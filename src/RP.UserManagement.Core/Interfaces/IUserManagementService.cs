using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RP.UserManagement.Core.Entities;

namespace RP.UserManagement.Core.Interfaces
{
    public interface IUserManagementService
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetById(int id);

        Task<string> Authenticate(string email, string password);
        Task<User> AddUser(User user);
    }
}
