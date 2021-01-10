using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WEBCOPY1.Models
{
    /// The RoleEdit class is used to represent the Role and the details of the Users in the Identity System.
    public class RoleEdit
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<User> Members { get; set; }
        public IEnumerable<User> NonMembers { get; set; }
    }
}