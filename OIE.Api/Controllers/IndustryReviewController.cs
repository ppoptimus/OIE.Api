using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Exceptions;
using System;
using System.Linq;
using ApplicationCore.ViewModels;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Authorization;
using IdentityServer4.AccessTokenValidation;

namespace OIE.Api.Controllers
{
    [Route("api/[controller]")]
    public class IndustryReviewController : Controller
    {
        private readonly IIndustryReviewService _industryReviewService;
        public IndustryReviewController(IIndustryReviewService industryReviewService)
        {
            _industryReviewService = industryReviewService;
        }

        [HttpGet("{year}/{industryId}/{countryId}")]
        public async Task<IActionResult> GetIndustryReview(int year, int industryId, int countryId)
        {
            var ret = await _industryReviewService.GetIndustryReview(year, industryId, countryId);
            if (ret == null)
                return NotFound("");

            return Ok(ret);
        }

        [HttpGet("{Id}")]
        public async Task<IndustryReview> GetIndustryReviewById(int Id)
        {
            return await _industryReviewService.GetByIdAsync(Id);
        }

        [HttpGet]
        public async Task<IList<IndustryReviewModel>> GetListIndustryReview()
        {
            return await _industryReviewService.GetListIndustryReview();
        }

        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.AdminPolicy)]
        [HttpPost]
        public async Task<IActionResult> SaveIndustryReview([FromBody]IndustryReview IndustryReview)
        {
            try
            {
                await _industryReviewService.SaveIndustryReview(IndustryReview);
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

        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.AdminPolicy)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIndustryReview(int id)
        {
            try
            {
                await _industryReviewService.DeleteIndustryReview(id);
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