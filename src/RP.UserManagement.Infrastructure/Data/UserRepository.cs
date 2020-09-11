using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RP.UserManagement.Core.Entities;
using RP.UserManagement.Core.Interfaces;

// TODO: Implement caching here

namespace RP.UserManagement.Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _userList;

        public UserRepository()
        {
            // Ideally this will come from the database using the DbContext
            _userList = LoadHardcodedUserInfo();
        }

        public async Task<List<User>> GetAllUsers()
        {
            // return the hard-coded values
            // Additionally, we can use caching to retrieve the users and
            // invalidate the cache whenever a new user is added or an existing user is updated.
            return await Task.Run(() => _userList);
        }

        public async Task<User> GetById(int id)
        {
            // return the first item from hard-coded user values
            return await Task.Run(() => _userList.FirstOrDefault(x => x.Id == id));
        }

        public async Task<User> Authenticate(string email, string password)
        {
            return await Task.Run(() => _userList.FirstOrDefault(x => x.Email == email && x.Password == password));
        }

        public async Task<User> AddUser(User user)
        {
            await Task.Run(() => _userList.Add(user));
            return _userList.FirstOrDefault(x => x.Email == user.Email);
        }

        private List<User> LoadHardcodedUserInfo()
        {
            // Hard-coded for simple application. This needs to come from Database.
            // User1: megavarnan@gmail.com from CompanyA superadmin, but for CompanyB, he is just a regular employee
            var usrCompanyRole = new Tuple<Company, UserRole>(new Company {Id = 1, Name = "CompanyA"},
                new UserRole {Id = 1, Name = "superadmin"});
            var usrCompanyRole2 = new Tuple<Company, UserRole>(new Company {Id = 2, Name = "CompanyB"},
                new UserRole {Id = 2, Name = "employee"});
            var usrList = new List<Tuple<Company, UserRole>> {usrCompanyRole, usrCompanyRole2};

            var users = new List<User>(new[]
            {
                new User
                {
                    FirstName = "Mega",
                    LastName = "Selvaraj",
                    Id = 1,
                    Email = "megavarnan@gmail.com",
                    Password = "test",
                    UserCompanyRole = usrList
                }
            });


            // User2:emp2@2.com from CompanyA, just a regular employee
            var usrCompanyRole3 = new Tuple<Company, UserRole>(new Company {Id = 1, Name = "CompanyA"},
                new UserRole {Id = 2, Name = "employee"});
            var usrList2 = new List<Tuple<Company, UserRole>> {usrCompanyRole3};

            users.Add(new User
            {
                FirstName = "FN2",
                LastName = "LN2",
                Id = 2,
                Email = "emp2@2.com",
                Password = "test",
                UserCompanyRole = usrList2
            });


            return users;
        }
    }
}