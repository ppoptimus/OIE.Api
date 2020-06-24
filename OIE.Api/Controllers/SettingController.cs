using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ApplicationCore.Dtos;
using Ardalis.GuardClauses;
using ApplicationCore.ViewModels;
using System.IO;
using OIE.Api.Models;
using ApplicationCore;
using System;
using Microsoft.AspNetCore.Authorization;
using IdentityServer4.AccessTokenValidation;

namespace OIE.Api.Controllers
{
    [Route("api/[controller]")]
    public class SettingController : Controller
    {
        private readonly IReportService _repository;
        private readonly IQueryRepository _queryRepository;
        public SettingController(IReportService repository, IQueryRepository queryRepository)
        {
            _repository = repository;
            _queryRepository = queryRepository;
        }

        [HttpGet("app-setting")]
        public async Task<AppSettingViewModel> GetFilter()
        {
            var result = await _repository.GetAppSettingAsync();
            return result;
        }


        [HttpGet("page/{pageName}")]
        public async Task<IActionResult> Get(string pageName)
        {
            try
            {
                using (StreamReader sr = new StreamReader(ServerUtils.ConfigInfo.MediaFolder + $"/{pageName}.html", System.Text.Encoding.UTF8))
                {
                    string line = await sr.ReadToEndAsync();
                    return Ok(new PageViewModel() { Html = line, PageName = pageName });
                }
            }
            catch { }
            return Ok(new PageViewModel() { PageName = pageName });
        }


        [HttpPost("page")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.AdminPolicy)]
        public async Task<IActionResult> Post([FromBody]PageViewModel page)
        {
            try
            {
                using (StreamWriter outputFile = new StreamWriter(ServerUtils.ConfigInfo.MediaFolder + $"/{page.PageName}.html"))
                {
                    outputFile.WriteLine(page.Html);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SaveCounter")]
        public async Task<IActionResult> SaveCounter([FromBody]WebCounterViewModel page)
        {
            try
            {
                //await _queryRepository.SaveCounter(page.url , page.ipaddress, page.sessionid);
                return Ok(await _queryRepository.SaveCounter(page.url, page.ipaddress, page.sessionid , page.date));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}