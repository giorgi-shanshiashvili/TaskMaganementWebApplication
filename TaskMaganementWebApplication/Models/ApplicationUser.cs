using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskMaganementWebApplication.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string User  { get; set; }
        public string Password { get; set; }
        public UserRole UserType { get; set; }
    }

    public enum UserRole
    {
        User,
        Support
    }
}
