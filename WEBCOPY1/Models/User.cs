using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WEBCOPY1.Models
{
    //Class for User
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        //new
        public bool IsActive { get; set; }

        //public List<Building> UserBuildings { get; set; }

        public User() 
        {
            IsActive = true;
        }
    }
}
