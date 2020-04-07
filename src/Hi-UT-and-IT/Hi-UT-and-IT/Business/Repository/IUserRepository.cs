using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MakeCodeBetter
{
    public interface IUserRepository
    {
        Task<UserEntity> GetAsync(int id);
    }
}
