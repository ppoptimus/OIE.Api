using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ApplicationCore.ViewModels;
using OIE.Api.Extensions;
using System;
using ApplicationCore.Exceptions;

namespace OIE.Api.Controllers
{
    [Route("api/report")]
    public class ReportController : Controller
    {
        private readonly IReportService _repository;
        private readonly IQueryRepository _queryRepository;
        public ReportController(IReportService repository, IQueryRepository queryRepository)
        {
            _repository = repository;
            _queryRepository = queryRepository;
        }

        [HttpGet("{year}/{industry}/{country}")]
        public async Task<ChartDataModel> GetALL(int year, int industry, int country)
        {
            var result = await _repository.GetRadarAsync(year, industry, country);
            return result;
        }

        [HttpGet("strengths/{year}/{industry}/{country}")]
        public async Task<ChartDataModel> GetStrengths(int year, int industry, int country)
        {
            var result = await _repository.GetStrengths(year, industry, country);
            return result;
        }
        [HttpGet("weaknesses/{year}/{industry}/{country}")]
        public async Task<ChartDataModel> GetWeaknesses(int year, int industry, int country)
        {
            var result = await _repository.GetWeaknesses(year, industry, country);
            return result;
        }

        [HttpGet("countrycompareindicatorscore/{year}/{industry}/{country}")]
        public async Task<IndexViewModel> GetCountryCompareIndicatorScore(int year, int industry, int country)
        {
            var result = await _repository.GetCountryCompareIndicatorScore(year, industry, country);
            return result;
        }

        [HttpGet("pillarscoregroupbyindustryandcountry/{year}/{pillar}")]
        public async Task<IList<IndustryGroupModel>> GetPillarScoreGroupByIndustryAndCountry(int year, int pillar)
        {
            var result = await _repository.GetPillarScoreGroupByIndustryAndCountry(year, pillar);
            return result;
        }

        [HttpGet("transindigraph/{chartNo}/{industry}")]
        public async Task<IActionResult> GetTransIndicatorGraph(int chartNo, int industry)
        {
            var data = await _queryRepository.GetTransIndicatorGraph(industry);
            if(data==null || data.Count==0)
                return NotFound($"No Transition Indicator for {industry.ToString()}");

            var IndicatorName = data.Where(d => d.GraphPos == chartNo).FirstOrDefault().Indicator_Name;
            var negative = new DataModel()
            {
                Data = (from d in data where d.GraphPos == chartNo select d.Score).ToList<double>(),
                Label = IndicatorName
            };

            return Ok(new ChartDataModel()
            {
                Data = new DataModel[] { negative },
                Labels = (from d in data where d.GraphPos == chartNo select d.Year.ToString()).ToArray()
            });
        }


        [HttpGet("4.0/{year}/{industry}")]
        public async Task<ChartDataModel> Get40(int year, int industry)
        {
            var result = await _repository.GetRadar40Async(year, industry);
            return result;
        }

        [HttpGet("industry40indicatorscore/{year}/{industry}")]
        public async Task<Industry40IndicatorScoreViewModel> GetIndustry40IndicatorScore(int year, int industry)
        {
            var result = await _repository.GetIndustry40IndicatorScore(year, industry);
            return result;
        }
    }
}