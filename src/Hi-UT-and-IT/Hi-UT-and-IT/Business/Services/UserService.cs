using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MakeCodeBetter.Business
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel> GetAsync(int id)
        {
            try
            {
                var userEntity = await _userRepository.GetAsync(id);
                var birthday = Convert(userEntity.Birthday);
                var userModel = new UserModel()
                {
                    Id = userEntity.Id,
                    Birthday = birthday,
                    Age = GetAge(birthday)
                };
                return userModel;
            }
            catch (Exception e)
            {
                if (e is DatabaseConnectionNotAvailableException databaseConnectionNotAvailableException)
                {
                    Console.WriteLine("log useful exception information here");
                    throw new UserServiceNotAvailableException();
                }
                else
                {
                    Console.WriteLine("log useful exception information here");
                    throw e;
                }
            }
        }

        private DateTime Convert(string birthdayString)
        {
            if (DateTime.TryParse(birthdayString, out var birthday))
            {
                return birthday;
            }
            return DateTime.MinValue;
        }

        private int GetAge(DateTime birthday)
        {
            if (birthday == DateTime.MinValue) return -1;
            var today = DateTime.Today;
            var age = today.Year - birthday.Year;
            if (birthday.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
