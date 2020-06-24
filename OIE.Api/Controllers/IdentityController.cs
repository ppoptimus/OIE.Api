using IdentityModel;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using OIE.Api.Models.AccountViewModels;
using ApplicationCore.Entities;
using OIE.Api.Extensions;
using System;

namespace OIE.Api.Controllers
{
    /// <summary>
    /// Identity Web API controller.
    /// </summary>
    [Route("api/[controller]")]
    // Authorization policy for this API.
    public class IdentityController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICompanyService _companyService;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;

        public IdentityController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            ICompanyService companyService,
            IEmailSender emailSender,
            ILogger<IdentityController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _companyService = companyService;
            _emailSender = emailSender;
            _logger = logger;
        }
        /// <summary>
        /// Gets all the users.
        /// </summary>
        /// <returns>Returns all the users</returns>
        // GET api/identity/GetAll
        [HttpGet("GetAll")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.AdminPolicy)]
        public async Task<IActionResult> GetAll(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            var users = await _userManager.GetUsersInRoleAsync(role.Name);

            return new JsonResult(users);
        }


        [HttpGet("Get")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.Authenticated)]
        public async Task<IActionResult> Get(string Id)
        {
            Company company = new Company();
            if (Id == "profile")
            {
                Id = User.GetId().ToString();
                company = await _companyService.GetCompanyByUserId(User.GetId());
            }
            var user = await _userManager.FindByIdAsync(Id);
            var roles = await _userManager.GetRolesAsync(user);
            var userModel = new UserViewModel()
            {
                id = user.Id.ToString(),
                firstname = user.Firstname,
                lastname = user.Lastname,
                username = user.UserName,
                email = user.Email,
                Company = company
            };

            userModel.IsAdministrator = roles.Contains(ApplicationRole.Administrator);
            userModel.IsOIEOfficer = roles.Contains(ApplicationRole.OIEOfficer);
            userModel.IsRegisteredUser = roles.Contains(ApplicationRole.RegisteredUser);

            return new JsonResult(userModel);
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <returns>IdentityResult</returns>
        // POST: api/identity/Create
        [HttpPost("Create")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody]UserViewModel model)
        {
            if (!model.IsAdministrator && !model.IsRegisteredUser && !model.IsOIEOfficer)
                model.IsRegisteredUser = true;

            if (model.id == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    Firstname = model.firstname,
                    Lastname = model.lastname,
                    AccessFailedCount = 0,
                    Email = model.email,
                    PhoneNumber = model.phonenumber,
                    EmailConfirmed = false,
                    LockoutEnabled = true,
                    NormalizedEmail = model.username.ToUpper(),
                    NormalizedUserName = model.username.ToUpper(),
                    TwoFactorEnabled = false,
                    UserName = model.username
                };

                if (model.password == null) model.password = "oie";
                var result = await _userManager.CreateAsync(user, model.password);

                if (result.Succeeded)
                {
                    if (model.IsAdministrator)
                        await addToRole(model.username, ApplicationRole.Administrator);
                    if (model.IsRegisteredUser)
                        await addToRole(model.username, ApplicationRole.RegisteredUser);
                    if (model.IsOIEOfficer)
                        await addToRole(model.username, ApplicationRole.OIEOfficer);

                    await addClaims(model.username);

                    if (model.Company == null)
                    {
                        var company = new Company();
                        company.ModifyUser = user.Id;
                        company.ContactName = user.Firstname;
                        company.ContactSurname = user.Lastname;
                        company.ContactNo = user.PhoneNumber;
                        company.ContactEmail = user.Email;
                        company.IndustryId = 99;
                        await _companyService.AddAsync(company);
                    }
                    else
                    {
                        model.Company.ModifyUser = user.Id;
                        model.Company.ContactName = user.Firstname;
                        model.Company.ContactSurname = user.Lastname;
                        model.Company.ContactNo = user.PhoneNumber;
                        model.Company.ContactEmail = user.Email;
                        model.Company.IndustryId = 99;
                        await _companyService.AddAsync(model.Company);
                    }

                }
                return new JsonResult(result);
            }
            else
            {
                var user = await _userManager.FindByIdAsync(model.id.ToString());
                user.Firstname = model.firstname;
                user.Lastname = model.lastname;
                user.Email = model.email;
                user.PhoneNumber = model.phonenumber;
                user.NormalizedEmail = model.username.ToUpper();
                var result = await _userManager.UpdateAsync(user);

                if (model.password != null)
                {
                    await _userManager.RemovePasswordAsync(user);
                    await _userManager.AddPasswordAsync(user, model.password);
                }
                if (result.Succeeded)
                {

                    if (model.IsAdministrator)
                        await _userManager.AddToRoleAsync(user, ApplicationRole.Administrator);
                    else
                        await _userManager.RemoveFromRoleAsync(user, ApplicationRole.Administrator);


                    if (model.IsOIEOfficer)
                        await _userManager.AddToRoleAsync(user, ApplicationRole.OIEOfficer);
                    else
                        await _userManager.RemoveFromRoleAsync(user, ApplicationRole.OIEOfficer);


                    if (model.IsRegisteredUser)
                        await _userManager.AddToRoleAsync(user, ApplicationRole.RegisteredUser);
                    else
                        await _userManager.RemoveFromRoleAsync(user, ApplicationRole.RegisteredUser);

                    await addClaims(model.username);


                    var company = await _companyService.GetCompanyByUserId(Convert.ToInt16(model.id));

                    company.ModifyUser = user.Id;
                    company.ContactName = model.firstname;
                    company.ContactSurname = model.lastname;
                    company.ContactNo = model.phonenumber;
                    company.ContactEmail = model.email;

                    await _companyService.UpdateAsync(company);
                }
                return new JsonResult(result);
            }
        }


        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <returns>IdentityResult</returns>
        // POST: api/identity/Update
        [HttpPost("Update")]
        [AllowAnonymous]
        public async Task<IActionResult> Update([FromBody]UserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.id.ToString());
            user.Firstname = model.firstname;
            user.Lastname = model.lastname;
            user.Email = model.email;
            user.PhoneNumber = model.phonenumber;
            user.NormalizedEmail = model.username.ToUpper();
            var result = await _userManager.UpdateAsync(user);


            if (model.password != null)
            {
                var ret = await _userManager.ChangePasswordAsync(user, model.currentPassword, model.password);
                if (!ret.Succeeded)
                {
                    return new JsonResult(ret);
            }
            }
            if (result.Succeeded)
            {

                await addClaims(model.username);


                var company = await _companyService.GetCompanyByUserId(Convert.ToInt16(model.id));

                company.ModifyUser = user.Id;
                company.ContactName = model.firstname;
                company.ContactSurname = model.lastname;
                company.ContactNo = model.phonenumber;
                company.ContactEmail = model.email;

                company.CompanyNameTh = model.Company.CompanyNameTh;
                company.CompanyName = model.Company.CompanyName;
                company.CompanyCard = model.Company.CompanyCard;
                company.AddressNo = model.Company.AddressNo;
                company.Building = model.Company.Building;
                company.Floor = model.Company.Floor;
                company.Soi = model.Company.Soi;
                company.Street = model.Company.Street;
                company.Tumbon = model.Company.Tumbon;
                company.Amphur = model.Company.Amphur;
                company.Province = model.Company.Province;
                company.PostCode = model.Company.PostCode;
                company.Phone = model.Company.Phone;
                company.Fax = model.Company.Fax;
                company.Website = model.Company.Website;

                await _companyService.UpdateAsync(company);
            }
            return new JsonResult(result);
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <returns>IdentityResult</returns>
            // POST: api/identity/Delete
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.AdminPolicy)]
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody]string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            var result = await _userManager.DeleteAsync(user);

            return new JsonResult(result);
        }

        private async Task addToRole(string userName, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            await _userManager.AddToRoleAsync(user, roleName);
        }

        private async Task addClaims(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var claims = new List<Claim> {
                new Claim(type: JwtClaimTypes.GivenName, value: user.Firstname),
                new Claim(type: JwtClaimTypes.FamilyName, value: user.Lastname),
            };
            var claimsToDelete = await _userManager.GetClaimsAsync(user);

            await _userManager.RemoveClaimsAsync(user, claimsToDelete);

            await _userManager.AddClaimsAsync(user, claims);
        }
    }
}