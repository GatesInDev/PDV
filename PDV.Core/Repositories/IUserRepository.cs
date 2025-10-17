using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PDV.Core.Entities;

namespace PDV.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string username);
    }
}
