using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealPage.UserManagement.WebApi.Models;
using RP.UserManagement.Core.Entities;
using RP.UserManagement.Core.Interfaces;

namespace RealPage.UserManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class UserManagementController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;

        public UserManagementController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        // GET: api/UserManagement
        [HttpGet]
        [Helpers.Authorize]
        public async Task<IEnumerable<User>> Get()
        {
            try
            {
                return await _userManagementService.GetAllUsers();
            }
            catch (Exception e)
            {
                // Send the exception details to the logger
                Console.WriteLine(e);
            }

            return null;
        }

        // Post: api/UserManagement
        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<string> Authenticate([FromBody] AuthRequestModel authRequest)
        {
            try
            {
                return await _userManagementService.Authenticate(authRequest.Email, authRequest.Password);
            }
            catch (Exception e)
            {
                // send the exception details to the logger
                Console.WriteLine(e);
            }

            return "Unable to generate the Bearer token!";
        }

        // POST: api/UserManagement
        [HttpPost("adduser")]
        [Helpers.Authorize]
        public async Task<IActionResult> AddUser([FromBody] UserRequestModel usm)
        {
            try
            {
                var user = (User) HttpContext.Items["User"];

                if (!IsAuthorizedUser(usm, user))
                    return Unauthorized("The logged in user is not authorized to add the user ");

                var usrCompanyRole = new Tuple<Company, UserRole>(usm.Company, usm.Role);
                var usrList = new List<Tuple<Company, UserRole>> {usrCompanyRole};

                // TODO: Potentially use Automapper for the ViewModels to DomainModels and vice-versa.
                await _userManagementService.AddUser(new User
                {
                    Id = usm.Id,
                    FirstName = usm.FirstName,
                    LastName = usm.LastName,
                    Email = usm.Email,
                    Password = usm.Password,
                    UserCompanyRole = usrList
                });

                return Ok($"Successfully added the user - {usm.FirstName} {usm.LastName}");
            }
            catch (Exception e)
            {
                // Log to the centralized logger
                Debug.Print(e.Message);
                return BadRequest($"Error: Unable to add user - {usm.FirstName} {usm.LastName}");
            }
        }

        private bool IsAuthorizedUser(UserRequestModel usrToAdd, User loggedinUser)
        {
            var authorizedUsrCompanyRole =
                new Tuple<Company, UserRole>(usrToAdd.Company, new UserRole {Id = 1, Name = "superadmin"});

            if (loggedinUser.UserCompanyRole.Any(x => x.Item1.Id ==
                                                      authorizedUsrCompanyRole.Item1.Id &&
                                                      x.Item2.Id == authorizedUsrCompanyRole.Item2.Id))
                return true;
            return false;
        }

        // GET: api/UserManagement/5
        [HttpGet("{id}", Name = "Get")]
        [Helpers.Authorize]
        public async Task<User> Get(int id)
        {
            try
            {
                return await _userManagementService.GetById(id);
            }
            catch (Exception e)
            {
                // send the exception details to the logger
                Console.WriteLine(e);
            }

            return null;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [Helpers.Authorize]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}