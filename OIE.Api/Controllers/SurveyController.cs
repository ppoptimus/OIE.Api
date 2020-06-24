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
using OIE.Api;

namespace OIE.Api.Controllers
{
    /// <summary>
    /// Resources Web API controller.
    /// </summary>
    [Route("api/[controller]")]
    // Authorization policy for this API.
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.SurveyPolicy)]
    public class SurveyController : Controller
    {
        private readonly ISurveyService _surveyService;
        private readonly ILogger _logger;

        public SurveyController(
           ISurveyService surveyService,
           ILogger<IdentityController> logger)
        {
            _surveyService = surveyService;
            _logger = logger;
        }

        [HttpGet("{year}")]
        public async Task<IActionResult> Get(int year)
        {
            try
            {
                return Ok(await _surveyService.GetSurveyByUserIdAndYear(User.GetId(), year));
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

        [HttpGet("yearlist")]
        public async Task<IActionResult> YearList()
        {
            try
            {
                return Ok(await _surveyService.GetYearList(User.GetId()));
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
        public async Task<IActionResult> SaveSurvey([FromBody]SurveyViewModel Survey)
        {
            try
            {
                await _surveyService.SaveAsync(Survey, User.GetId());
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
