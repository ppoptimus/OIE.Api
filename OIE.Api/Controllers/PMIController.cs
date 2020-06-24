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
using static ApplicationCore.ViewModels.PMIModel;

namespace OIE.Api.Controllers
{
    /// <summary>
    /// Resources Web API controller.
    /// </summary>
    [Route("api/[controller]")]
    // Authorization policy for this API.
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.PMIPolicy)]
    public class PMIController : Controller
    {
        private readonly IPMIService _PMIService;
        private readonly ICompanyService _companyService;
        private readonly ILogger _logger;
        private readonly IReportService _reportService;

        public PMIController(
           IPMIService PMIService,
           ICompanyService companyService,
           IReportService reportService,
           ILogger<IdentityController> logger)
        {
            _PMIService = PMIService;
            _companyService = companyService;
            _reportService = reportService;
            _logger = logger;
        }




        [HttpPost]
        public async Task<IActionResult> SavePMI([FromBody]PMIModel PMI)
        {
            try
            {
                await _PMIService.SavePMI(PMI, User.GetId());
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

        [HttpGet("{month}/{year}")]
        public async Task<IActionResult> GetDataPMI(int month, int year)
        {
            try
            {
                var data = await _PMIService.GetDataPMI(month, year, User.GetId());

                if (data.PMIHEAD == null)
                {
                    Company company = new Company();
                    var CurrentYear = DateTime.Now.Year;
                    var CurrentMonth = DateTime.Now.Month;
                    if ((year == CurrentYear) && (month == CurrentMonth))
                    {
                        company = await _companyService.GetCompanyByUserId(User.GetId());
                    } else {
                        company = null;
                    }
                    data.PMIHEAD = new PMIHEAD() {
                        InformantFName = company.ContactName,
                        InformantLName = company.ContactSurname,
                        InformantCompany = company.CompanyNameTh,
                        InformantPosition = company.ContactPosition,
                        Email = company.ContactEmail,
                        Phone = company.ContactNo
                };
                    return NotFound(data);
                }

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

        [HttpGet("GetPMIIndicator")]
        public async Task<IActionResult> GetPMIIndicator()
        {
            return Ok(await _PMIService.GetPMIIndicator());
        }

        [HttpGet("GetPMIMonthYear")]
        public async Task<IActionResult> GetPMIMonthYear()
        {
            return Ok(await _PMIService.GetPMIMonthYear());
        }

        [HttpGet("GetPMIListForCal/{month}/{year}/{industrySize}")]
        public async Task<IActionResult> GetPMIListForCal(int month, int year, string industrySize)
        {
            return Ok(await _PMIService.GetPMIListForCal(month,year, industrySize));
        }

        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.AdminPolicy)]
        [HttpPost("PMIForCalSave/{selectedMonth}/{selectedYear}/{selectedIndustrySize}")]
        public async Task PMIForCalSave([FromBody]IList<PMIForCalSave> model, int selectedMonth, int selectedYear , string selectedIndustrySize)
        {
            await _PMIService.PMIForCalSave(model, User.GetId(), selectedMonth, selectedYear , selectedIndustrySize);
        }

        [HttpGet("GetPMIVersionIndicator")]
        public async Task<IActionResult> GetPMIVersionIndicator()
        {
            return Ok(await _PMIService.GetPMIVersionIndicator());
        }

        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.AdminPolicy)]
        [HttpPost("PMIVersionUpdateFlag/{version}")]
        public async Task PMIVersionUpdateFlag(int version)
        {
            await _PMIService.PMIVersionUpdateFlag(version , User.GetId());
        }
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.AdminPolicy)]
        [HttpPost("PMIIndicatorSetupSave")]
        public async Task PMIIndicatorSetupSave([FromBody]IList<PMIIndicatorForSave> model)
        {
            await _PMIService.PMIIndicatorSetupSave(model, User.GetId());
        }


        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.AdminPolicy)]
        [HttpPost("PMICalculateSave/{month}/{year}/{version}")]
        public async Task PMICalculateSave(int month, int year , int version)
        {
            await _PMIService.PMICalculateSave(month , year ,  version , User.GetId());
        }

        [HttpGet("GetPMICheckLatestMonthYear")]
        public async Task<IActionResult> GetPMICheckLatestMonthYear()
        {
            return Ok(await _PMIService.GetPMICheckLatestMonthYear());
        }


        [HttpGet("GetPMIResultList/{year}")]
        public async Task<IActionResult> GetPMIResultList(int year)
        {
            return Ok(await _PMIService.GetPMIResultList(year));
        }


        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.AdminPolicy)]
        [HttpPost("SavePMIDataUseFlag/{idToUse}")]
        public async Task SavePMIDataUseFlag(int idToUse)
        {
            await _PMIService.SavePMIDataUseFlag(idToUse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// overall:
        /// byindicator:[]
        /// </returns>
        [HttpGet("report/{year}/{month}/{resultType}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetReport(int year, int month, string resultType)
        {
            try
            {
                var data = await _reportService.GetPMIReport(year, month, resultType);
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

        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.AdminPolicy)]
        [HttpPost("report/indicator/summary")]
        public async Task SavePMIReportIndicatorSummary([FromBody]PMIReportIndicatorSummaryDataModel summaryData)
        {
            await _PMIService.SavePMIReportIndicatorSummary(summaryData, User.GetId());
        }

        
    }
}
