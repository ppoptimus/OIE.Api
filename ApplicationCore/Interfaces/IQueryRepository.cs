using ApplicationCore.Dtos;
using ApplicationCore.Entities;
using ApplicationCore.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using static ApplicationCore.ViewModels.PMIModel;

namespace ApplicationCore.Interfaces
{

    public interface IQueryRepository : IRepository<Document>, IAsyncRepository<Document>
    {
        Task<IList<IndexScoreModel>> GetRadarReportAsync(int year, int industry, int country);
        Task<IList<AssessmentScoreModel>> GetSelfAssessmentReport(int userId, int year);
        Task<IList<AssessmentScoreModel>> GetSelfAssessmentReport4(int userId, int year);
        Task<IList<PMIScoreModel>> GetPMIReport(int year, int month, string resultType);
        Task<IList<CountryCompareIndicatorScoreModel>> GetCountryCompareIndicatorScore(int year, int industry, int country);
        Task<IList<PillarScoreByIndustryAndCountry>> GetPillarScoreGroupByIndustryAndCountry(int year, int pillar);

        Task<IList<IndexScoreModel>> GetFactorsDataAsync(int year, int industry, int country);
        Task<IList<IndustryReviewModel>> GetListIndustryReview();
        Task<IList<IndustryIndicatorModel>> GetIndustryIndicatorList();
        Task<IndustryReviewModel> GetIndustryReview(int year, int industryId, int countryId);
        //Master
        Task<IList<V_M_Industry>> Get_V_M_Industry();
        Task<IList<V_M_Country>> Get_V_M_Country();
        Task<IList<V_M_Company>> Get_V_M_Company();
        Task<IList<V_M_Index>> Get_V_M_Index();
        Task<IList<V_M_Indicator>> Get_V_M_Indicator();
        Task<IList<V_M_COMPETITIVENESS_INDEX>> Get_V_M_COMPETITIVENESS_INDEX();
        Task<IList<V_M_Dimension_Indicator>> Get_V_M_Dimension_Indicator();
        Task<IList<V_M_Pmi_Indicator>> Get_V_M_Pmi_Indicator();
        Task<IList<V_M_Pmi_Industry>> Get_V_M_Pmi_Industry();
        Task<IList<V_M_Professional_Weight_4>> Get_V_M_Professional_Weight_4();
        Task<IList<V_M_MasterWeight_4>> Get_V_M_MasterWeight_4();
        //Data  
        Task<IList<V_D_SURVEY>> Get_V_D_SURVEY(int year);
        Task<IList<V_D_PRIMARY>> Get_V_D_PRIMARY(int year);
        Task<IList<V_D_SECONDARY>> Get_V_D_SECONDARY(int year);
        Task<IList<V_D_ASSESSEMENT>> Get_V_D_ASSESSEMENT(int year);
        Task<IList<V_D_SURVEY_RECOMEND>> Get_V_D_SURVEY_RECOMEND(int year);
        Task<IList<V_D_SECONDARY_THONLY>> Get_V_D_SECONDARY_THONLY(int year);
        Task<IList<V_D_SECONDARY_THONLY>> Get_V_D_SECONDARY_THONLY_N(int year);
        Task<IList<V_D_PMI_PART1>> Get_V_D_PMI_PART1(int year,int month);
        Task<IList<V_D_PMI_PART2>> Get_V_D_PMI_PART2(int year, int month);
        Task<IList<V_D_PRIMARY_DATA_4>> Get_V_D_PRIMARY_DATA_4(int year);
        //Result
        Task<IList<V_R_INDUSTRY_INDICATOR>> Get_V_R_INDUSTRY_INDICATOR(int year, int version);
        Task<IList<V_R_ASSESSMENT_BASE>> Get_V_R_ASSESSMENT_BASE(int year, int version);
        Task<IList<V_R_TH_5SW>> Get_V_R_TH_5SW(int year, int version);
        Task<IList<V_R_COMPETITIVENESS_INDEX>> Get_V_R_COMPETITIVENESS_INDEX(int year, int version);
        Task<IList<V_R_RESULT_4>> Get_V_R_RESULT_4(int year, int version);
        Task<IList<V_R_PMI_RESULT>> Get_V_R_PMI_RESULT(int year, int month);

        Task<IList<TransIndiGraphModel>> GetTransIndicatorGraph(int industry);
        Task<IList<ListViewModel>> GetPillarIndex();
        Task<IList<ListViewModel>> GetCountries();
        Task<IList<ListViewModel>> GetIndustries();
        Task<IList<ListViewModel>> GetPmiIndustries();
        Task<IList<IndustryCountryListViewModel>> GetIndustryCountryList();
        Task<IList<IndustryIndicatorModel>> GetIndustryIndicatorModelList(int year);
        Task<IList<IndustryIndicatorModel>> GetIndustryIndicatorModelList4(int year);
        Task<IList<IndustryIndicatorDataForSetupPrimary>> GetIndustryIndicatorSetupPrimary(int year, int industryId, string employeeNo);
        Task<IList<IndustryIndicatorDataForSetupPrimary>> GetIndustryIndicatorSetupPrimary4(int year, int industryId, string employeeNo);
        Task<IList<IndustryIndicatorDataForSetupSecondary>> GetIndustryIndicatorSetupSecondary(int year, int industryId, int countryId);
        Task<IndustryIndicatorDataCountModel> GetIndustryIndicatorDataCount(int year);
        Task<IndustryIndicatorDataCountModel> GetIndustryIndicatorDataCount4(int year);
        Task<IndustryIndicatorModel> GetIndustryIndicatorCalculate(int year, int userId);
        Task<IndustryIndicatorModel> GetIndustryIndicatorCalculate4(int year, int userId);
        Task<IndustryIndicatorModel> IndustryIndicatorUseSave(int year, int industry_Id);
        Task<IndustryIndicatorModel> IndustryIndicatorUseSave4(int year, int iiid);
        Task IndustryIndicatorDelete(int year, int iiid);
        Task IndustryIndicatorDelete4(int year, int iiid);
        Task IndustryIndicatorSetupPrimarySave(string xml, int selectYear, int industry, string employeeNo, int userId);
        Task IndustryIndicatorSetupPrimarySave4(string xml, int selectYear, int industry, string employeeNo, int userId);
        Task IndustryIndicatorSetupSecondarySave(string xml, int selectYear, int industry, int country , int userId);
        Task<IList<AssessmentBase>> GetAssessmentBase(int industry, int year);
        Task SaveXMLPrimaryData(string XMLPrimaryData);
        Task DeletePrimaryData(int PrimaryHId);
        Task SaveSelfAssessment(string xml, int userId, int oldSelfAssessmentId);
        //Task DeleteAssessmentData(int AssessmentId);

        Task<IList<int>> GetSelfAssessmentYear(int userId);
        Task<IList<int>> GetSelfAssessmentYear4(int userId);
        Task<PMIHEAD> GETPMI(int month,int year,int userId);
        Task<IList<PMIDATA>> GETPMIDATA(int month,int year,int userId);
        Task<IList<IndustryCountryViewModel>> GetIndustryCountry();
        Task<IList<YearVersionViewModel>> GetYearVersion();
        Task<IList<string>> SaveCounter(string url, string ipaddress, string sessionid , string date);
        Task<IList<PMIIndicator>> GetPMIIndicator();
        Task<IList<PMIMonthYear>> GetPMIMonthYear();
        Task<IList<PMIListForCal>> GetPMIListForCal(int month, int year , string industrySize);
        Task PMIForCalSave(string xml, int selectedMonth, int selectedYear, string selectedIndustrySize , int userId );
        Task<IList<PMIVersionIndicator>> GetPMIVersionIndicator();
        Task PMIVersionUpdateFlag(int version , int userId);
        Task PMIIndicatorSetupSave(string xml, int userId);
        Task PMICalculateSave(int month , int year , int version , int userId);
        Task<IList<PMIMonthYear>> GetPMICheckLatestMonthYear();
        Task<IList<IndustryIndicator40CoreDimensionData>> GetIndustry40Data(int year, int indicatorId, int companyId);
        Task<IList<IndustryIndicator40DimensionData>> GetDimensionData(int year, int indicatorId, int companyId);
        Task SaveXMLIndustry40CoreDimension(string xmlIndustry40CoreDimension);
        Task SaveXMLIndustry40Dimension(string xmlIndustry40Dimension , string xmlIndustry40CoreDimension);
        Task DeleteIndustry40Data(int primaryHID);
        Task<IList<PMIResult>> GetPMIResultList(int year);
        Task SavePMIDataUseFlag(int idToUse);
        Task SavePMIReportIndicatorSummary(PMIReportIndicatorSummaryDataModel summaryData, int usreId);
        Task<IList<DimensionIndicator40ScoreModel>> GetRadar40ReportAsync(int year, int industry);

        Task<IList<ListViewModel>> GetDimensionIndicator40Index();

        Task<IList<Industry40IndicatorScoreModel>> GetIndustry40IndicatorScore(int year, int industry);
    }
}