using System;
using Xunit;
using IdentityApi.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using IdentityApi.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;

namespace IdentityApi.Tests
{
    public class HomeControllerTest
    {
        private HomeController _homeController; 

        private ILogger<HomeController> _logger;
        private SignInManager<IdentityApiUser> _signInManager;

        

        public HomeControllerTest()
        {
            //Arrange
            _homeController = new HomeController(_signInManager, _logger);
        }

        [Fact]
        public void IndexViewTests()
        {
            // Act
            var result = _homeController.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);

        }

        [Fact]
        public void PrivacyViewTests()
        {
            // Act
            var result = _homeController.Privacy() as ViewResult;

            // Assert
            Assert.NotNull(result);

        }

        [Fact]
        public async void LogOutViewTests()
        {
            // Act
            var result = await _homeController.LogOut() as ViewResult;

            // Assert
            Assert.NotNull(result);

        }
    }
}
