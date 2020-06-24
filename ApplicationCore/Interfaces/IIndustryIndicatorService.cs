using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.ViewModels;

namespace ApplicationCore.Interfaces
{
    public interface IIndustryIndicatorService
    {
        Task<IList<IndustryIndicatorModel>> GetIndustryIndicator(int year);
        Task<IList<IndustryIndicatorModel>> GetIndustryIndicator4(int year);
        Task<IList<IndustryIndicatorDataForSetupPrimary>> GetIndustryIndicatorForPrimary(int year, int industryId, string employeeNo);
        Task<IList<IndustryIndicatorDataForSetupPrimary>> GetIndustryIndicatorForPrimary4(int year, int industryId, string employeeNo);
        Task<IList<IndustryIndicatorDataForSetupSecondary>> GetIndustryIndicatorForSecondary(int year , int industryId , int countryId);
        Task<IndustryIndicatorDataCountModel> GetIndustryIndicatorDataCount(int year);
        Task<IndustryIndicatorDataCountModel> GetIndustryIndicatorDataCount4(int year);
        Task IndustryIndicatorCalculate(int year ,int userid);
        Task IndustryIndicatorCalculate4(int year, int userid);
        Task IndustryIndicatorUseSave(int year ,int iiid);
        Task IndustryIndicatorUseSave4(int year, int iiid);
        Task IndustryIndicatorDelete(int year ,int iiid);
        Task IndustryIndicatorDelete4(int year, int iiid);
        Task IndustryIndicatorForPrimarySave(IList<IndustryIndicatorDataForSetupPrimary> model, int userId, int selectYear, int industry, string employeeNo);
        Task IndustryIndicatorForPrimarySave4(IList<IndustryIndicatorDataForSetupPrimary> model, int userId, int selectYear, int industry, string employeeNo);
        Task IndustryIndicatorForSecondarySave(IList<IndustryIndicatorDataForSetupSecondary> model, int userId , int selectYear, int industry , int country);
        Task<IList<IndustryIndicator40CoreDimensionData>> GetIndustry40Data(int year, int industryId, int companyId);
        Task<IList<IndustryIndicator40DimensionData>> GetDimensionData(int year, int industryId, int companyId);


    }
}