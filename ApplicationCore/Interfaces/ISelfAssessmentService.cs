using ApplicationCore.Entities;
using ApplicationCore.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ISelfAssessmentService
    {
        Task SaveAsync(SelfAssessmentModel category, int userId);
        //Task<IList<AssessmentBase>> GetAssessmentBaseByFilter(AssessmentBaseFilterModel filter);
        Task<SelfAssessmentModel> GetSelfAssessmentByUserIdAndYear(int userId, int Year);
    }
}
