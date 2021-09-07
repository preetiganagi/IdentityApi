using System.Collections.Generic;
using System.Linq;
using IdentityApi.Data;
using IdentityApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApi.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly IdentityApiContext _db;

        public List<Role> Roles { get; set; }
        
        public RoleController(IdentityApiContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var roles = _db.Roles.ToList();
            ViewBag.roles = roles;

            return View();
        }

        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Role role)
        {
            _db.Roles.Add(new Microsoft.AspNetCore.Identity.IdentityRole()
            {
                Name = role.Name,
                NormalizedName = role.NormalizedName,
                ConcurrencyStamp = role.ConcurrencyStamp
            });

            _db.SaveChanges();
            //TempData["EditMessage"] = "Role Added Successfully";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                var currentRole = _db.Roles.Find(role.Id);

                currentRole.Name = role.Name;
                currentRole.NormalizedName = role.NormalizedName;
                currentRole.ConcurrencyStamp = role.ConcurrencyStamp;

                _db.Roles.Update(currentRole);
                _db.SaveChanges();
            }
            //TempData["EditMessage"] = "Role Updated Successfully";
            
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string Id)
        {

            var currentRole = _db.Roles.Find(Id);
            _db.Roles.Remove(currentRole);
            _db.SaveChanges();

            //TempData["EditMessage"] = "Role Removed Successfully";

            return RedirectToAction("Index");
        }

        public string GetId()
        {
            return _db.Roles.First().Id;
        }
    }
}
