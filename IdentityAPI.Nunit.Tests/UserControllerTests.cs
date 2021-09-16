using IdentityApi.Controllers;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using IdentityApi.Data;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Moq;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using IdentityApi.Areas.Identity.Data;
using System.Threading.Tasks;

namespace IdentityAPI.Nunit.Tests
{
    class UserControllerTests
    {

        private UserController _userController;

        private Mock<UserManager<IdentityApiUser>> _mockUserManager;

        private DbContextOptions<IdentityApiContext> dbContextOptions = new DbContextOptionsBuilder<IdentityApiContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options;

        public UserControllerTests()
        {

            var userStoreMock = new Mock<IUserStore<IdentityApiUser>>();

            _mockUserManager = new Mock<UserManager<IdentityApiUser>>(
                               userStoreMock.Object,
                               new Mock<IOptions<IdentityOptions>>().Object,
                               new Mock<IPasswordHasher<IdentityApiUser>>().Object,
                               new IUserValidator<IdentityApiUser>[0],
                               new IPasswordValidator<IdentityApiUser>[0],
                               new Mock<ILookupNormalizer>().Object,
                               new Mock<IdentityErrorDescriber>().Object,
                               new Mock<IServiceProvider>().Object,
                               new Mock<ILogger<UserManager<IdentityApiUser>>>().Object
                               );


            var serviceProvider = new ServiceCollection()
                                   .AddLogging()
                                   .BuildServiceProvider();
            var loggerServiceProvider = serviceProvider.GetService<ILoggerFactory>();
            var logger = loggerServiceProvider.CreateLogger<UserController>();

            var context = new IdentityApiContext(dbContextOptions);
            List<IdentityApiUser> users = new List<IdentityApiUser>
            {

                new IdentityApiUser { UserName = "User", Email = "user@test.com"},
                new IdentityApiUser { UserName = "Admin", Email = "admin@test.com"}
            };

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole { Name = "Admin", NormalizedName = "Admin"},
                new IdentityRole { Name = "admin", NormalizedName = "admin"},
                new IdentityRole { Name = "User", NormalizedName = "User"}
            };

            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string> { RoleId = roles[0].Id, UserId = users[0].Id, }
            };

            context.AddRange(users);
            context.AddRange(roles);
            context.AddRange(userRoles);
            context.SaveChanges();


            _userController = new UserController(logger, context , _mockUserManager.Object);
        }

        [Test]
        public void IndexTest()
        {
            var result = _userController.Index() as IActionResult;
            Assert.NotNull(result);
        }

        [Test]
        public void EditRoleTest()
        {
            var result = _userController.Edit(_userController.getId()) as IActionResult;
            Assert.NotNull(result);
        }

        [Test]
        public void EditUserRoleTest()
        {
            IdentityApiUserRole user = new IdentityApiUserRole()
            {
                Id = _userController.getId(),
                FirstName = "test",
                LirstName = "test"
            };
            var result = _userController.Edit(user) as Task<ActionResult>;
            Assert.NotNull(result);
        }

        [Test]
        public void AddRoleTest()
        {
            var result = _userController.AddRole(_userController.getId()) as IActionResult;
            Assert.NotNull(result);
        }

        [Test]
        public void DeleteTest()
        {
            var result = _userController.Delete(_userController.getId()) as IActionResult;
            Assert.NotNull(result);
        }

        [Test]
        public void CreateAdminTest()
        {
            var result = _userController.CreateAdmin(_userController.getId()) as IActionResult;
            Assert.NotNull(result);
        }
    }
}
