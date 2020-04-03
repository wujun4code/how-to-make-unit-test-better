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

        public async Task<UserModel> Get(int id)
        {
            var userEntity = await _userRepository.Get(id);
            var birthday = Convert(userEntity.Birthday);
            var userModel = new UserModel()
            {
                Id = userEntity.Id,
                Birthday = birthday,
                Age = GetAge(birthday)
            };
            return userModel;
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
            var today = DateTime.Today;
            var age = today.Year - birthday.Year;
            if (birthday.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
