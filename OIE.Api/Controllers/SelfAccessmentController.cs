using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OIE.Api.Extensions;
using ApplicationCore.ViewModels;

namespace OIE.Api.Controllers
{
    /// <summary>
    /// Resources Web API controller.
    /// </summary>
    [Route("api/[controller]")]
    // Authorization policy for this API.
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.SelfAssessmentPolicy)]
    public class SelfAssessmentController : Controller
    {
        private readonly ISelfAssessmentService _SelfAssessmentService;
        private readonly IReportService _repository;
        private readonly ILogger _logger;

        public SelfAssessmentController(
           ISelfAssessmentService SelfAssessmentService,
           IReportService repository,
           ILogger<IdentityController> logger)
        {
            _SelfAssessmentService = SelfAssessmentService;
            _repository = repository;
            _logger = logger;
        }
         
        [HttpGet("{year}")]
        public async Task<IActionResult> Get(int Year)
        {
            try
            {
                return Ok(await _SelfAssessmentService.GetSelfAssessmentByUserIdAndYear(User.GetId(), Year));
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


        [HttpGet("report/{year}")]
        public async Task<IActionResult> GetReport(int year)
        {
            try
            {
                var data = await _repository.GetSelfAssessmentReport(User.GetId(), year);
                return Ok(data);
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

        [HttpGet("report4/{year}")]
        public async Task<IActionResult> GetReport4(int year)
        {
            try
            {
                var data = await _repository.GetSelfAssessmentReport4(User.GetId(), year);
                return Ok(data);
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

        [HttpPost]
        public async Task<IActionResult> Save([FromBody]SelfAssessmentModel SelfAssessment)
        {
            try
            {
                await _SelfAssessmentService.SaveAsync(SelfAssessment, User.GetId());
                return Ok();
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
