using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RP.UserManagement.Core.Entities;

namespace RP.UserManagement.Core.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAllUsers();
        public Task<User> GetById(int id);
        public Task<User> Authenticate(string email, string password);
        Task<User> AddUser(User user);
    }
}
