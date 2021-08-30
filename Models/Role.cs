using System;
using Microsoft.AspNetCore.Identity;

namespace IdentityApi.Models
{
    public class Role : IdentityRole
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
