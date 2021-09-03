using IdentityApi.Controllers;
using IdentityApi.Data;
using IdentityApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IdentityApi.Tests
{
    public class RoleControllerTest
    {
        private RoleController _roleController;

        RoleControllerTest()
        {

            var optionsBuilder = new DbContextOptionsBuilder<IdentityApiContext>();  
            var context = new IdentityApiContext(optionsBuilder.Options);
            _roleController = new RoleController(context);
        }

        [Fact]
        public void IndexViewTests()
        {
            // Act
            var result = _roleController.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);

        }

        [Fact]
        public void CreateRoleTests()
        {
            //Arrange
            Role role = new Role
            {
                Name = "testRole",
                NormalizedName = "testRole",
                ConcurrencyStamp = "testRole"
            };
            // Act
            var result = _roleController.Create(role) as ViewResult;

            // Assert
            Assert.NotNull(result);

        }

    }
}
