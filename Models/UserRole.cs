using System;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Identity;

namespace IdentityApi.Models
{
    public class UserRole
    {
        public UserRole(string userId, string roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }

        public string UserId { get; set; }
        public string RoleId { get; set; }
    }
}
