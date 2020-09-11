using Microsoft.VisualStudio.TestTools.UnitTesting;
using RP.UserManagement.Core.Services;
using RP.UserManagement.Infrastructure.Data;

namespace RealPage.UserManagement.Core.UnitTests
{
    [TestClass]
    public class UserManagementUnitTest
    {
        [TestMethod]
        public void GetUserById_ReturnsValidUser()
        {
            var userRepo = new UserRepository();

            //Arrange
            var usrManagementService = new UserManagementService(userRepo);

            // Act
            var user = usrManagementService.GetById(1).Result;

            // Assert
            Assert.AreEqual(user.Email, "megavarnan@gmail.com");
        }
    }
}