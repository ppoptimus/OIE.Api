using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using OIE.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using IdentityServer4.AccessTokenValidation;

namespace OIE.Api.Controllers
{
    /// <summary>
    /// Resources Web API controller.
    /// </summary>
    [Route("api/[controller]")]
    // Authorization policy for this API.
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.Authenticated)]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly ILogger _logger;

        public CompanyController(
           ICompanyService companyService,
           ILogger<IdentityController> logger)
        {
            _companyService = companyService;
            _logger = logger;
        } 
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _companyService.GetCompanyByUserId(User.GetId()));
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
