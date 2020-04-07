using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MakeCodeBetter
{
    public class UserRepository : IUserRepository
    {
        public Task<UserEntity> GetAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
