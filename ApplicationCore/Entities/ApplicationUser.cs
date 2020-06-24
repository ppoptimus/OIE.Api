using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
