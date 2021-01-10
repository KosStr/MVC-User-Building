using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WEBCOPY1.Models
{
    public class BuildingUser
    {
        [Key]
        public int Building_id { set; get; }
        public int User_id { set; get; }
    }
}
