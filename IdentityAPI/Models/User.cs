using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityApi.Models
{
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int IsActive { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LirstName { get; set; }

    }
}

