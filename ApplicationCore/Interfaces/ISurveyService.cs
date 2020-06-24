using ApplicationCore.Entities;
using ApplicationCore.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ISurveyService
    {
        Task<SurveyViewModel> GetSurveyByUserIdAndYear(int userId, int Year);
        Task<List<int>> GetYearList(int userId);
        Task SaveAsync(SurveyViewModel surveyModel, int userId);
    }
}
