using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using WEBCOPY1.Models;

namespace WEBCOPY1.Areas.Identity.Pages.BuildingList
{
    public class BuildingListModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Street")]
            public string Street { get; set; }

            [Required]
            [Display(Name = "Number")]
            public int Number { get; set; }

            [Required]
            [Display(Name = "Square")]
            public int Square { get; set; }

            [Required]
            [Display(Name = "Price")]
            public int Price { get; set; }

            [Required]
            [Display(Name = "Year of creation")]
            public int YearOfCreation { get; set; }
        }

   
        
    }
}

