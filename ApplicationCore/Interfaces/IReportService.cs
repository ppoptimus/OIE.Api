using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.ViewModels;

namespace ApplicationCore.Interfaces
{
    public interface IReportService
    {
        Task<ChartDataModel> GetRadarAsync(int year, int industry, int country);
        Task<AppSettingViewModel> GetAppSettingAsync();
        Task<ChartDataModel> GetStrengths(int year, int industry, int country);
        Task<ChartDataModel> GetWeaknesses(int year, int industry, int country);
        Task<ChartDataModel> GetSelfAssessmentReport(int userId, int year);
        Task<ChartDataModel> GetSelfAssessmentReport4(int userId, int year);
        Task<IndexViewModel> GetCountryCompareIndicatorScore(int year, int industry, int country);
        Task<IList<IndustryGroupModel>> GetPillarScoreGroupByIndustryAndCountry(int year, int pillar);
        //Task<ChartDataModel> GetTransIndicatorGraph(int year, int industry, int country);
        Task<PMIReportModel> GetPMIReport(int year, int month, string resultType);
        Task<ChartDataModel> GetRadar40Async(int year, int industry);
        Task<Industry40IndicatorScoreViewModel> GetIndustry40IndicatorScore(int year, int industry);        
    }
}