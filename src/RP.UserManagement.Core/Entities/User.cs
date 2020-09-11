using System;
using System.Collections.Generic;
using System.Text;

namespace RP.UserManagement.Core.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public List<Tuple<Company, UserRole>> UserCompanyRole { get; set; }

    }
}
