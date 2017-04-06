using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EuroJobsCrm.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int? ContragentId { get; set; }
        public string FullName { get; set; }
        public string LanguageCode { get; set; }
        public bool Deleted { get; set; }
    }
}
