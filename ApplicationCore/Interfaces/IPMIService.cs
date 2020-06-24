using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.ViewModels;
using static ApplicationCore.ViewModels.PMIModel;

namespace ApplicationCore.Interfaces
{
    public interface IPMIService
    {
        Task SavePMI(PMIModel pmi, int userId);
        Task<PMIModel> GetDataPMI(int month,int year,int userId);
        Task<IList<PMIIndicator>> GetPMIIndicator();
        Task<IList<PMIMonthYear>> GetPMIMonthYear();
        Task<IList<PMIListForCal>> GetPMIListForCal(int month, int year, string industrySize);
        Task PMIForCalSave(IList<PMIForCalSave> model, int userId, int selectedMonth, int selectedYear , string selectedIndustrySize);
        Task<IList<PMIVersionIndicator>> GetPMIVersionIndicator();
        Task PMIVersionUpdateFlag(int version , int userId);
        Task PMIIndicatorSetupSave(IList<PMIIndicatorForSave> model, int userId);
        Task PMICalculateSave(int month , int year , int version, int userId);
        Task<IList<PMIMonthYear>> GetPMICheckLatestMonthYear();
        Task<IList<PMIResult>> GetPMIResultList(int year);
        Task SavePMIDataUseFlag(int idToUse);
        Task SavePMIReportIndicatorSummary(PMIReportIndicatorSummaryDataModel summaryData, int userId);


        //Task GetResultSummary(int year, int month, int indicatorId, int industryId, string resultType);
        //Task SaveResultSummary(PMIResultSummary summary, int userId);


    }
}