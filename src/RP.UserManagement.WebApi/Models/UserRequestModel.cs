using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RP.UserManagement.Core.Entities;

namespace RealPage.UserManagement.WebApi.Models
{
    public class UserRequestModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public Company Company { get; set; }

        public UserRole Role { get; set; }
        
    }
}
