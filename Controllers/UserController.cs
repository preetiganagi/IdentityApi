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
        private readonly IdentityApiContext _userContext;
        private readonly UserManager<IdentityApiUser> _userManager;

        public UserController(ILogger<UserController> logger, IdentityApiContext userManagerContext, UserManager<IdentityApiUser> userManager)
        {
            _logger = logger;
            _userContext = userManagerContext;
            _userManager = userManager;
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
            var user = _userContext.Users.Where(s => s.Id == Id).FirstOrDefault();
            ViewBag.Roles = _userContext.Roles.ToList();

            var AdminRole = _userContext.Roles.Where(s => s.NormalizedName == "admin").FirstOrDefault();
            ViewBag.AdminRoleUser = AdminRole.Id;
            TempData["adminrole"] = AdminRole.Id;
            var LoginUserRole = _userContext.Roles.Join(_userContext.UserRoles,
                  role => role.Id,
                  userrole => userrole.RoleId,

                  (role, userrole) => new AdminRoleAttributes
                  {
                      RoleId = role.Id,
                      UserId = userrole.UserId,
                      RoleName = role.Name
                  }
              ).Where(c => c.UserId == Id).FirstOrDefault();
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
                IdentityApiUser Edituser = _userContext.Users.Where(s => s.Id == Euser.Id).FirstOrDefault();

                Edituser.LirstName = Euser.LirstName;
                Edituser.FirstName = Euser.FirstName;
                Edituser.PhoneNumber = Euser.PhoneNumber;
                _userContext.Users.Update(Edituser);

                _userContext.SaveChanges();
                var roleid = Euser.RoleId;
                var userid = Edituser.Id;

                if (roleid != null)
                {
                    var addrole = _userContext.UserRoles.Where(s => s.UserId == Edituser.Id).FirstOrDefault();
                    _userContext.UserRoles.Remove(addrole);
                    _userContext.SaveChanges();

                    addrole.RoleId = roleid;
                    addrole.UserId = userid;
                    _userContext.UserRoles.Add(addrole);

                    _userContext.SaveChanges();

                }



                // await userManager.AddToRoleAsync(Edituser, Euser.RoleId);

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
                var UserRoleObj = _userContext.UserRoles.Where(s => s.UserId == Id).FirstOrDefault();
                var RoleObj = _userContext.Roles.Where(s => s.Id == UserRoleObj.RoleId).FirstOrDefault();
                return View(RoleObj);
            }

            return View();


        }

        [HttpGet]
        public IActionResult Delete(string Id)
        {

            var delrole = _userContext.UserRoles.Where(s => s.UserId == Id).FirstOrDefault();
            if (delrole != null)
            {
                _userContext.UserRoles.Remove(delrole);
                _userContext.SaveChanges();
            }
           

            var DelUser = _userContext.Users.Where(s => s.Id == Id).FirstOrDefault();
            _userContext.Users.Remove(DelUser);
            _userContext.SaveChanges();
            TempData["EditMessage"] = "User deleted Successfully";
            return View("Index");
        }

        [HttpGet]
        public IActionResult CreateAdmin(string Id)
        {
            IdentityApiUser user = new IdentityApiUser();
            user = _userContext.Users.Where(s => s.Id == Id).FirstOrDefault();
            var adminRole = _userContext.Roles.Where(s => s.Name == "Admin").FirstOrDefault();
            var userRole = _userContext.UserRoles.Single(r => r.UserId == user.Id);
            userRole.RoleId = adminRole.Id;
            _userContext.UserRoles.Update(userRole); 
            TempData["EditMessage"] = "Admin updated Successfully";
            return View("Index");
        }
    }

    public class AdminRoleAttributes
    {
        public string RoleId;
        public string UserId;
        public string RoleName;
        
    }
}