using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IdentityServer4.AccessTokenValidation;
using System.Globalization;

namespace OIE.Api.Controllers
{
    [Route("api/export-data")]
    [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme, Policy = Config.AdminPolicy)]
    public class DataExportController : Controller
    {
        private readonly IQueryRepository _repository;
        public DataExportController(IQueryRepository repository)
        {
            //CultureInfo.CurrentCulture = new CultureInfo("th-TH", false);
            _repository = repository;
        }

        //MASTRER
        [HttpGet("V_M_Industry")]
        public async Task<IActionResult> GetFilter()
        {
            return Ok(await _repository.Get_V_M_Industry());
        }

        [HttpGet("V_M_Country")]
        public async Task<IActionResult> Get_V_M_Country()
        {
            return Ok(await _repository.Get_V_M_Country());
        }

        [HttpGet("V_M_Company")]
        public async Task<IActionResult> Get_V_M_Company()
        {
            return Ok(await _repository.Get_V_M_Company());
        }

        [HttpGet("V_M_Index")]
        public async Task<IActionResult> Get_V_M_Index()
        {
            return Ok(await _repository.Get_V_M_Index());
        }

        [HttpGet("V_M_Indicator")]
        public async Task<IActionResult> Get_V_M_Indicator()
        {
            return Ok(await _repository.Get_V_M_Indicator());
        }
        [HttpGet("V_M_COMPETITIVENESS_INDEX")]
        public async Task<IActionResult> Get_V_M_COMPETITIVENESS_INDEX()
        {
            return Ok(await _repository.Get_V_M_COMPETITIVENESS_INDEX());
        }
        [HttpGet("V_M_Dimension_Indicator")]
        public async Task<IActionResult> Get_V_M_Dimension_Indicator()
        {
            return Ok(await _repository.Get_V_M_Dimension_Indicator());
        }
        [HttpGet("V_M_Pmi_Indicator")]
        public async Task<IActionResult> Get_V_M_Pmi_Indicator()
        {
            return Ok(await _repository.Get_V_M_Pmi_Indicator());
        }
        [HttpGet("V_M_Pmi_Industry")]
        public async Task<IActionResult> Get_V_M_Pmi_Industry()
        {
            return Ok(await _repository.Get_V_M_Pmi_Industry());
        }
        [HttpGet("V_M_Professional_Weight_4")]
        public async Task<IActionResult> Get_V_M_Professional_Weight_4()
        {
            return Ok(await _repository.Get_V_M_Professional_Weight_4());
        }
        [HttpGet("V_M_MasterWeight_4")]
        public async Task<IActionResult> Get_V_M_MasterWeight_4()
        {
            return Ok(await _repository.Get_V_M_MasterWeight_4());
        }
        //DATA
        [HttpGet("V_D_SURVEY/{year}")]
        public async Task<IActionResult> Get_V_D_SURVEY(int year)
        {
            return Ok(await _repository.Get_V_D_SURVEY(year));
        }
        [HttpGet("V_D_PRIMARY/{year}")]
        public async Task<IActionResult> Get_V_D_PRIMARY(int year)
        {
            return Ok(await _repository.Get_V_D_PRIMARY(year));
        }
        [HttpGet("V_D_SECONDARY/{year}")]
        public async Task<IActionResult> Get_V_D_SECONDARY(int year)
        {
            return Ok(await _repository.Get_V_D_SECONDARY(year));
        }
        [HttpGet("V_D_ASSESSEMENT/{year}")]
        public async Task<IActionResult> Get_V_D_ASSESSEMENT(int year)
        {
            return Ok(await _repository.Get_V_D_ASSESSEMENT(year));
        }
        [HttpGet("V_D_SURVEY_RECOMEND/{year}")]
        public async Task<IActionResult> Get_V_D_SURVEY_RECOMEND(int year)
        {
            return Ok(await _repository.Get_V_D_SURVEY_RECOMEND(year));
        }
        [HttpGet("V_D_SECONDARY_THONLY/{year}")]
        public async Task<IActionResult> Get_V_D_SECONDARY_THONLY(int year)
        {
            return Ok(await _repository.Get_V_D_SECONDARY_THONLY(year));
        }
        [HttpGet("V_D_SECONDARY_THONLY_N/{year}")]
        public async Task<IActionResult> Get_V_D_SECONDARY_THONLY_N(int year)
        {
            return Ok(await _repository.Get_V_D_SECONDARY_THONLY_N(year));
        }
        [HttpGet("V_D_PMI_PART1/{year}/{month}")]
        public async Task<IActionResult> Get_V_D_PMI_PART1(int year,int month)
        {
            return Ok(await _repository.Get_V_D_PMI_PART1(year, month));
        }
        [HttpGet("V_D_PMI_PART2/{year}/{month}")]
        public async Task<IActionResult> Get_V_D_PMI_PART2(int year , int month)
        {
            return Ok(await _repository.Get_V_D_PMI_PART2(year, month));
        }
        [HttpGet("V_D_PRIMARY_DATA_4/{year}")]
        public async Task<IActionResult> Get_V_D_PRIMARY_DATA_4(int year)
        {
            return Ok(await _repository.Get_V_D_PRIMARY_DATA_4(year));
        }

        //RESULT
        [HttpGet("V_R_INDUSTRY_INDICATOR/{year}/{version}")]
        public async Task<IActionResult> Get_V_R_INDUSTRY_INDICATOR(int year, int version)
        {
            return Ok(await _repository.Get_V_R_INDUSTRY_INDICATOR(year, version));
        }
        [HttpGet("V_R_ASSESSMENT_BASE/{year}/{version}")]
        public async Task<IActionResult> Get_V_R_ASSESSMENT_BASE(int year, int version)
        {
            return Ok(await _repository.Get_V_R_ASSESSMENT_BASE(year, version));
        }
        [HttpGet("V_R_TH_5SW/{year}/{version}")]
        public async Task<IActionResult> Get_V_R_TH_5SW(int year, int version)
        {
            return Ok(await _repository.Get_V_R_TH_5SW(year, version));
        }
        [HttpGet("V_R_COMPETITIVENESS_INDEX/{year}/{version}")]
        public async Task<IActionResult> Get_V_R_COMPETITIVENESS_INDEX(int year, int version)
        {
            return Ok(await _repository.Get_V_R_COMPETITIVENESS_INDEX(year, version));
        }

        [HttpGet("V_R_RESULT_4/{year}/{version}")]
        public async Task<IActionResult> Get_V_R_RESULT_4(int year, int version)
        {
            return Ok(await _repository.Get_V_R_RESULT_4(year, version));
        }
        [HttpGet("V_R_PMI_RESULT/{year}/{month}")]
        public async Task<IActionResult> Get_V_R_PMI_RESULT(int year, int month)
        {
            return Ok(await _repository.Get_V_R_PMI_RESULT(year, month));
        }
        //[HttpGet("strengths/{year}/{industry}/{country}")]
        //public async Task<ChartDataModel> GetStrengths(int year, int industry, int country)
        //{
        //    var result = await _repository.GetStrengths(year, industry, country);
        //    return result;
        //}
    }
}