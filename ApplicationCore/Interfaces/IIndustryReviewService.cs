using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.ViewModels;

namespace ApplicationCore.Interfaces
{
    public interface IIndustryReviewService
    {
        Task<IndustryReviewModel> GetIndustryReview(int year, int industryId, int countryId);
        Task SaveIndustryReview(IndustryReview industryReview);
        Task DeleteIndustryReview(int id);
        Task<IndustryReview> GetByIdAsync(int Id);
        Task<IList<IndustryReviewModel>> GetListIndustryReview();
    }
}