using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityApi.Areas.Identity.Data;
using IdentityApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

namespace IdentityApi.Areas.Identity.Pages.Account
{
    [Authorize]

    public class ListUsersModel : PageModel
    {

        private readonly UserManager<IdentityApiUser> _userManager;

        public ListUsersModel(UserManager<IdentityApiUser> userManager)
        {
            _userManager = userManager;
        }

        [TempData]
        public string StatusMessage { get; set; }
        public IList<IdentityApiUser> User { get; set; }

        public async Task OnGetAsync()
        {
            User = await _userManager.Users.ToListAsync();
        }
    }
}
