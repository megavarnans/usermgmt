using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RP.UserManagement.Core.Entities;
using RP.UserManagement.Core.Interfaces;

namespace RP.UserManagement.Core.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUserRepository _userRepository;

        public UserManagementService(IUserRepository userRepo)
        {
            _userRepository = userRepo;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<User> GetById(int id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<string> Authenticate(string email, string password)
        {
            var user = await _userRepository.Authenticate(email, password);
            user.Password = string.Empty;

            // authentication successful so generate jwt token
            var token = GenerateJwtToken(user);

            return token;
        }

        public async Task<User> AddUser(User user)
        {
            return await _userRepository.AddUser(user);
        }

        private string GenerateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            // TODO: Move this to a common access path.
            var secret = "This is a secret string used for encoding";

            var usrJson = JsonConvert.SerializeObject(user);

            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {new Claim("id", user.Id.ToString()), new Claim("user", usrJson)}),
                Expires = DateTime.UtcNow.AddMinutes(60), // Need to be fetched from the config or Database
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}