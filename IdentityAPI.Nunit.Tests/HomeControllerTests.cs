using IdentityApi.Areas.Identity.Data;
using IdentityApi.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IdentityAPI.Nunit.Tests
{
    public class HomeControllerTests
    {
        private HomeController _homeController;

        private Mock<UserManager<IdentityApiUser>> _mockUserManager;
        private Mock<SignInManager<IdentityApiUser>> _mockSignInManager;

        public HomeControllerTests()
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

            _mockSignInManager = new Mock<SignInManager<IdentityApiUser>>(
                                 _mockUserManager.Object,
                                 new Mock<IHttpContextAccessor>().Object,
                                 new Mock<IUserClaimsPrincipalFactory<IdentityApiUser>>().Object,
                                 new Mock<IOptions<IdentityOptions>>().Object,
                                 new Mock<ILogger<SignInManager<IdentityApiUser>>>().Object,
                                 new Mock<IAuthenticationSchemeProvider>().Object,
                                 new Mock<IUserConfirmation<IdentityApiUser>>().Object
                                 );


            var serviceProvider = new ServiceCollection()
            .AddLogging()
            .BuildServiceProvider();
            var loggerServiceProvider = serviceProvider.GetService<ILoggerFactory>();

            var logger = loggerServiceProvider.CreateLogger<HomeController>();
            _homeController = new HomeController(_mockSignInManager.Object, logger);
        }

        [Test]
        public void IndexTest()
        {
            var result = _homeController.Index() as IActionResult;
            Assert.NotNull(result);
        }

        [Test]
        public void PrivacyTest()
        {
            var result = _homeController.Privacy() as IActionResult;
            Assert.NotNull(result);
        }

        [Test]
        public void LogOutTest()
        {
            var result = _homeController.LogOut() as RedirectToActionResult;
            Assert.NotNull(result);
        }
    }
}
