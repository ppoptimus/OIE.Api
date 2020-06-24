using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.ViewModels;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OIE.Api.Extensions;

namespace OIE.Api.Controllers
{
    [Route("api/IndustryIndicator")]
    public class IndustryIndicatorController : Controller
    {
        private readonly IIndustryIndicatorService _iIndustryIndicator;
        public IndustryIndicatorController(IIndustryIndicatorService iIndustryIndicator)
        {
            _iIndustryIndicator = iIndustryIndicator;

        }

        [HttpGet("get/{year}")]
        public async Task<IList<IndustryIndicatorModel>> GetIndustryIndicator(int year)
        {
            return await _iIndustryIndicator.GetIndustryIndicator(year);
        }
        [HttpGet("get4/{year}")]
        public async Task<IList<IndustryIndicatorModel>> GetIndustryIndicator4(int year)
        {
            return await _iIndustryIndicator.GetIndustryIndicator4(year);
        }
        [HttpGet("{year}")]
        public async Task<IndustryIndicatorDataCountModel> GetIndustryIndicatorDataCount(int year)
        {
            return await _iIndustryIndicator.GetIndustryIndicatorDataCount(year);
        }

        [HttpGet("getDataCount4/{year}")]
        public async Task<IndustryIndicatorDataCountModel> GetIndustryIndicatorDataCount4(int year)
        {
            return await _iIndustryIndicator.GetIndustryIndicatorDataCount4(year);
        }

        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.AdminPolicy)]
        [HttpPost("{year}")]
        public async Task GetIndustryIndicatorCalculate(int year)
        {
            await _iIndustryIndicator.IndustryIndicatorCalculate(year, User.GetId());
        }

        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.AdminPolicy)]
        [HttpPost("saveCalculate4/{year}")]
        public async Task GetIndustryIndicatorCalculate4(int year)
        {
            await _iIndustryIndicator.IndustryIndicatorCalculate4(year, User.GetId());
        }

        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.AdminPolicy)]
        [HttpPut("{year}/{industryId}")]
        public async Task IndustryIndicatorSave(int year, int industryId)
        {
            await _iIndustryIndicator.IndustryIndicatorUseSave(year, industryId);
        }

        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.AdminPolicy)]
        [HttpPut("saveUseData4/{year}/{iiid}")]
        public async Task IndustryIndicatorSaveUseData4(int year, int iiid)
        {
            await _iIndustryIndicator.IndustryIndicatorUseSave4(year, iiid);
        }

        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.AdminPolicy)]
        [HttpDelete("{year}/{iiid}")]
        public async Task IndustryIndicatorDelete(int year, int iiid)
        {
            await _iIndustryIndicator.IndustryIndicatorDelete(year, iiid);
        }

        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.AdminPolicy)]
        [HttpDelete("delete4/{year}/{iiid}")]
        public async Task IndustryIndicatorDelete4(int year, int iiid)
        {
            await _iIndustryIndicator.IndustryIndicatorDelete4(year, iiid);
        }

        [HttpGet("primary/{year}/{industryId}/{employeeNo}")]
        public async Task<IList<IndustryIndicatorDataForSetupPrimary>> GetIndustryIndicatorForPrimary(int year, int industryId , string employeeNo)
        {
            return await _iIndustryIndicator.GetIndustryIndicatorForPrimary(year, industryId , employeeNo);
        }
        [HttpGet("primary4/{year}/{industryId}/{employeeNo}")]
        public async Task<IList<IndustryIndicatorDataForSetupPrimary>> GetIndustryIndicatorForPrimary4(int year, int industryId, string employeeNo)
        {
            return await _iIndustryIndicator.GetIndustryIndicatorForPrimary4(year, industryId, employeeNo);
        }
        [HttpGet("secondary/{year}/{industryId}/{countryId}")]
        public async Task<IList<IndustryIndicatorDataForSetupSecondary>> GetIndustryIndicatorForSecondary(int year , int industryId, int countryId)
        {
            return await _iIndustryIndicator.GetIndustryIndicatorForSecondary(year , industryId , countryId);
        }

        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.AdminPolicy)]
        [HttpPost("primary/{selectYear}/{industry}/{employeeNo}")]
        public async Task IndustryIndicatorForPrimarySave([FromBody]IList<IndustryIndicatorDataForSetupPrimary> model, int selectYear, int industry , string employeeNo)
        {
            await _iIndustryIndicator.IndustryIndicatorForPrimarySave(model, User.GetId(), selectYear, industry , employeeNo);
        }

        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.AdminPolicy)]
        [HttpPost("primary4/{selectYear}/{industry}/{employeeNo}")]
        public async Task IndustryIndicatorForPrimarySave4([FromBody]IList<IndustryIndicatorDataForSetupPrimary> model, int selectYear, int industry, string employeeNo)
        {
            await _iIndustryIndicator.IndustryIndicatorForPrimarySave4(model, User.GetId(), selectYear, industry, employeeNo);
        }


        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.AdminPolicy)]
        [HttpPost("secondary/{selectYear}/{industry}/{country}")]
        public async Task IndustryIndicatorForSecondarySave([FromBody]IList<IndustryIndicatorDataForSetupSecondary> model , int selectYear, int industry , int country)
        {
            await _iIndustryIndicator.IndustryIndicatorForSecondarySave(model, User.GetId() , selectYear, industry , country);
        }

        [HttpGet("GetIndustry40Data/{year}/{industryId}/{companyId}")]
        public async Task<IList<IndustryIndicator40CoreDimensionData>> GetIndustry40Data(int year, int industryId, int companyId)
        {
            return await _iIndustryIndicator.GetIndustry40Data(year, industryId , companyId);
        }

        [HttpGet("getDimension/{year}/{industryId}/{companyId}")]
        public async Task<IList<IndustryIndicator40DimensionData>> GetDimensionData(int year, int industryId, int companyId)
        {
            return await _iIndustryIndicator.GetDimensionData(year, industryId, companyId);
        }
    }
}