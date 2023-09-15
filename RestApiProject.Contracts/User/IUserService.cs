using RestApiProject.Contracts.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiProject.Contracts.User
{
    public interface IUserService : IBaseService
    {
        Task<bool> IsUserExist(string username);
    }
}
