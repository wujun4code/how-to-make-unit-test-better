using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MakeCodeBetter
{
    public interface IUserService
    {
        Task<UserModel> GetAsync(int id);
    }
}
