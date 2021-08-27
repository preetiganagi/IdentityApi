using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IdentityApi.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the IdentityApiUser class
    public class IdentityApiUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [PersonalData]
        //[DataType(DataType.Date)]
        [Required]
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string LirstName { get; set; }

        public static implicit operator UserManager<object>(IdentityApiUser v)
        {
            throw new NotImplementedException();
        }
    }

    public class IdentityApiUserRole : IdentityApiUser
    {
        public string RoleId { get; set; }
    }
}
