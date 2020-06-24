using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.Entities
{

    public class ApplicationRole : IdentityRole<int>
    {
        public static string Administrator = "Administrator";
        public static string OIEOfficer = "OIE Officer";
        public static string RegisteredUser= "Registered User";

        public ApplicationRole() : base() { }
        public ApplicationRole(string roleName) : base(roleName) { }
    }

}
