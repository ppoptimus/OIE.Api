using System.Collections.Generic;
using ApplicationCore.Entities;

namespace OIE.Api.Models.AccountViewModels
{
    /// <summary>
    /// Class required to create a new user.
    /// </summary>
    public class UserViewModel
    {
        public string id { get; set; }
        public string username { get; set; }
        public string currentPassword { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string phonenumber { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }

        public bool IsAdministrator { get; set; }
        public bool IsOIEOfficer { get; set; }
        public bool IsRegisteredUser { get; set; }

        public Company Company { get; set; }
    }

}
