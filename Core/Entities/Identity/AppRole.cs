using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    public class AppRole : IdentityRole
    {
        public ICollection<AppUserRole> UserRole { get; set; }
    }
}