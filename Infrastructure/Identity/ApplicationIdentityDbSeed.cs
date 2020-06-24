using IdentityModel;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using Infrastructure.Identity;

namespace Infrastructure.Data
{
    public class ApplicationIdentityDbSeed : IIdentityDbSeed
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public ApplicationIdentityDbSeed(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager
            )
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task SeedAsync(ApplicationIdentityDbContext context)
        {
            //var defaultUser = new ApplicationUser { UserName = "demouser@microsoft.com", Email = "demouser@microsoft.com" };
            //await userManager.CreateAsync(defaultUser, "Pass@word1");

            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return; // Db has been seeded.
            }

            string[] roles = new string[] { ApplicationRole.Administrator, ApplicationRole.OIEOfficer, ApplicationRole.RegisteredUser };

            // Creates Roles.
            foreach (var role in roles)
            {
                await _roleManager.CreateAsync(new ApplicationRole(role));

                // Seeds user.
                var user = new ApplicationUser
                {
                    Firstname = role,
                    Lastname = string.Empty,
                    AccessFailedCount = 0,
                    Email = role + "@oie.com",
                    EmailConfirmed = false,
                    LockoutEnabled = true,
                    NormalizedEmail = role + "@oie.com",
                    NormalizedUserName = role,
                    TwoFactorEnabled = false,
                    UserName = role + "@oie.com"
                };

                var result = await _userManager.CreateAsync(user, role + "01*");

                if (result.Succeeded)
                {
                    var adminUser = await _userManager.FindByNameAsync(user.UserName);
                    // Assigns the user to role.
                    await _userManager.AddToRoleAsync(adminUser, role);
                    // Assigns claims.
                    var claims = new List<Claim> {
                        new Claim(type: JwtClaimTypes.Email, value: user.Email),
                        new Claim(type: JwtClaimTypes.GivenName, value: user.Firstname),
                        new Claim(type: JwtClaimTypes.FamilyName, value: user.Lastname),
                    };
                    await _userManager.AddClaimsAsync(adminUser, claims);
                }
            }
        }
    }
}
