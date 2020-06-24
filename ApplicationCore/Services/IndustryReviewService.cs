using ApplicationCore.Interfaces;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using ApplicationCore.Specifications;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationCore.Services
{
    public class IndustryReviewService : IIndustryReviewService
    {
        private readonly IAppLogger<IndustryReviewService> _logger;

        private readonly IAsyncRepository<IndustryReview> _industryReviewRepository;
        private readonly IQueryRepository _queryRepository;

        private IUnitOfWork _uow;

        public IndustryReviewService(
            IUnitOfWork uow,
            IAsyncRepository<IndustryReview> industryReviewRepository,
            IQueryRepository queryRepository,
            IAppLogger<IndustryReviewService> logger)
        {
            _uow = uow;
            _industryReviewRepository = industryReviewRepository;
            _logger = logger;
            _queryRepository = queryRepository;
        }

        public async Task<IndustryReviewModel> GetIndustryReview(int year, int industryId, int countryId)
        {
            return await _queryRepository.GetIndustryReview(year, industryId, countryId);
        }

        public async Task SaveIndustryReview(IndustryReview industryReviewModel)
        {
            try
            {
                _uow.BeginTransaction();
                if(industryReviewModel.Id == 0)
                {
                    //Create
                    await _industryReviewRepository.AddAsync(industryReviewModel);
                }
                else
                {
                    //Update
                    await _industryReviewRepository.UpdateAsync(industryReviewModel);
                }
                
                _uow.CommitTransaction();
            }
            catch (System.Exception e)
            {
                _uow.RollbackTransaction();
                throw;
            }
        }

        public async Task<IndustryReview> GetByIdAsync(int Id)
        {
            return await _industryReviewRepository.GetByIdAsync(Id);
        }

        public async Task<IList<IndustryReviewModel>> GetListIndustryReview()
        {
            return await _queryRepository.GetListIndustryReview();
        }

        public async Task DeleteIndustryReview(int id)
        {
            try
            {
                _uow.BeginTransaction();

                await _industryReviewRepository.DeleteAsync(await _industryReviewRepository.GetByIdAsync(id));

                _uow.CommitTransaction();
            }
            catch (System.Exception e)
            {
                _uow.RollbackTransaction();
                throw;
            }
        }
    }
}
