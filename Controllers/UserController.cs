using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityApi.Areas.Identity.Data;
using IdentityApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;

namespace IdentityApi.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IdentityApiContext _userManager;
        private readonly UserManager<IdentityApiUser> userManager;

        public UserController(ILogger<UserController> logger, IdentityApiContext userManagerContext, UserManager<IdentityApiUser> userManagerRole)
        {
            _logger = logger;
            _userManager = userManagerContext;
            userManager = userManagerRole;
        }

        [BindProperty]
        public IdentityApiUser User { get; set; }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(string Id)
        {
           

            var user = _userManager.Users.Where(s => s.Id == Id).FirstOrDefault();
            ViewBag.Roles =  _userManager.Roles.ToList();

           var AdminRole =  _userManager.Roles
                    .Join(
            _userManager.UserRoles,
                role => role.Id,
                userrole => userrole.RoleId,

                (role, userrole) => new AdminRoleAttributes
                {
                    RoleId = role.Id,
                    UserId = userrole.UserId,
                    RoleName = role.NormalizedName
                }
            ).Where(c => c.RoleName == "admin").FirstOrDefault();

            ViewBag.AdminRoleUser = AdminRole;

          var  LoginUserRole = _userManager.Roles.Join(_userManager.UserRoles,
                role => role.Id,
                userrole => userrole.RoleId,

                (role, userrole) => new AdminRoleAttributes
                {
                    RoleId = role.Id,
                    UserId = userrole.UserId,
                    RoleName = role.Name
                }
            ).Where(c => c.UserId == Id ).FirstOrDefault();
            ViewBag.LoginUserRole = LoginUserRole;
            return View(user);

        }

        [HttpPost]
        public async Task<ActionResult> Edit(IdentityApiUserRole Euser)
        {
            //update student in DB using EntityFramework in real-life application

            //update list by removing old student and adding updated student for demo purpose

            if (ModelState.IsValid)
            {
                IdentityApiUser Edituser = _userManager.Users.Where(s => s.Id == Euser.Id).FirstOrDefault();

                Edituser.LirstName = Euser.LirstName;
                Edituser.FirstName = Euser.FirstName;
                Edituser.PhoneNumber = Euser.PhoneNumber;
                _userManager.Users.Update(Edituser);

                _userManager.SaveChanges();

                await userManager.AddToRoleAsync(Edituser, Euser.RoleId);

            }
            TempData["EditMessage"] = "User Edited Successfully";
            //return View("./../../Areas/Identity/Pages/Account/ListUsers");
             return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddRole(string Id)
        {
            if (Id != null)
            {
                var UserRoleObj = _userManager.UserRoles.Where(s => s.UserId == Id).FirstOrDefault();
                var RoleObj = _userManager.Roles.Where(s => s.Id == UserRoleObj.RoleId).FirstOrDefault();
                return View(RoleObj);
            }

            return View();
         

        }
    }

    public class AdminRoleAttributes
    {
        public string RoleId;
        public string UserId;
        public string RoleName;
        
    }
}