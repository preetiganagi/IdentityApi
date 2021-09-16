using IdentityApi.Areas.Identity.Data;
using IdentityApi.Controllers;
using IdentityApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using IdentityApi.Data;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace IdentityAPI.Nunit.Tests
{
    public class RoleControllerTest
    {
        private DbContextOptions<IdentityApiContext> dbContextOptions = new DbContextOptionsBuilder<IdentityApiContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options;
        private RoleController _roleController;
        private IdentityRole newRole;
        private IdentityApiContext context;
        public RoleControllerTest()
        {
            newRole = new IdentityRole()
            {
               Name = "Admin",
               NormalizedName ="Admin"
            };

            context = new IdentityApiContext(dbContextOptions);
            List<IdentityRole> Roles = new List<IdentityRole>
            {
                new IdentityRole { Name = "SuperUser", NormalizedName = "SuperUser"},
                new IdentityRole { Name = "Admin", NormalizedName = "Admin"},
                new IdentityRole { Name = "User", NormalizedName = "User"}
            };

            context.AddRange(Roles);
            context.SaveChanges();
            _roleController = new RoleController(context);
        }

        [Test]
        public void IndexTest()
        {
            var result = _roleController.Index() as IActionResult;
            Assert.NotNull(result);
        }

        [Test]
        public void CreateTest()
        {
            Role newRole = new Role()
            {
                Name = "Tester",
                NormalizedName = "Tester"
            };
            var result = _roleController.Create(newRole) as IActionResult;
            Assert.NotNull(result);
        }

        [Test]
        public void EditTest()
        {
            var id = _roleController.GetId();
            IdentityRole newRole = new IdentityRole()
            {
                Id = id,
                Name = "Developer",
                NormalizedName = "Developer"
            };
            var result = _roleController.Edit(newRole) as IActionResult;
            Assert.NotNull(context.Roles.Find(id));
        }

        [Test]
        public void DeleteTest()
        {
            var result = _roleController.Delete(_roleController.GetId()) as IActionResult;
            Assert.NotNull(result);
        }
    }
}
