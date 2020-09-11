using System.ComponentModel.DataAnnotations;

namespace RealPage.UserManagement.WebApi
{
    public class AuthRequestModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
