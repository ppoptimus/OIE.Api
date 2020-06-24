using ApplicationCore.Interfaces;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using ApplicationCore.Specifications;
using ApplicationCore.ViewModels;
using System.Linq;


namespace ApplicationCore.Services
{
    public class SelfAssessmentService : ISelfAssessmentService
    {
        private readonly IAppLogger<SelfAssessmentService> _logger;

        private readonly IAsyncRepository<Company> _companyRepository;
        private readonly IAsyncRepository<Assessment> _assessmentRepository;
        private readonly IAsyncRepository<AssessmentData> _assessmentDataRepository;
        private readonly IAsyncRepository<AssessmentBase> _assessmentBaseRepository;
        private readonly IQueryRepository _queryRepository;

        private IUnitOfWork _uow;

        public SelfAssessmentService(
            IUnitOfWork uow,
            IAsyncRepository<Company> companyRepository,
            IAsyncRepository<Assessment> assessmentRepository,
            IAsyncRepository<AssessmentData> assessmentDataRepository,
            IAsyncRepository<AssessmentBase> assessmentBaseRepository,
            IQueryRepository queryRepository,
            IAppLogger<SelfAssessmentService> logger)
        {
            _uow = uow;
            _companyRepository = companyRepository;
            _assessmentRepository = assessmentRepository;
            _assessmentDataRepository = assessmentDataRepository;
            _assessmentBaseRepository = assessmentBaseRepository;
            _queryRepository = queryRepository;
            _logger = logger;
        }

        //public async Task<IList<AssessmentBase>> GetAssessmentBaseByFilter(AssessmentBaseFilterModel filter)
        //{
        //    return await _assessmentBaseRepository.ListAsync(new AssessmentBaseFilterSpecification(filter));
        //}

        public async Task<SelfAssessmentModel> GetSelfAssessmentByUserIdAndYear(int userId, int Year)
        {
            // - - - - - Assessment - - - - -
            Assessment assessment = (await _assessmentRepository.ListAsync(new AssessmentFilterSpecification(Year, userId))).FirstOrDefault();

            // - - - - - Company - - - - -
            Company company = (await _companyRepository.ListAsync(new CompanyFilterSpecification(new Company() { CreateUser = userId }))).FirstOrDefault();

            if (assessment == null)
            {
                return new SelfAssessmentModel()
                {
                    company = company,
                    assessment = new Assessment(),
                    assessmentData = new List<AssessmentData>()
                };
            }

            // - - - - - Assessment Data - - - - -
            IList<AssessmentData> assessmentData = await _assessmentDataRepository.ListAsync(new AssessmentDataFilterSpecification(assessment.Id));

            return new SelfAssessmentModel()
            {
                company = company,
                assessment = assessment,
                assessmentData = assessmentData
            };

        }

        public async Task SaveAsync(SelfAssessmentModel selfAssessmentModel, int userId)
        {
            try
            {
                _uow.BeginTransaction();

                //###################### Delete Data ######################
                //---------- Search Assessment ----------
                Assessment assessmentToDelete = (await _assessmentRepository.ListAsync(new AssessmentFilterSpecification(selfAssessmentModel.assessment.Year, userId))).FirstOrDefault();

                //###################### Insert Data ######################
          
                // Update
                selfAssessmentModel.company.ModifyUser = userId;
                await _companyRepository.UpdateAsync(selfAssessmentModel.company);

                //---------- Insert Assessment ----------
                selfAssessmentModel.assessment.ModifyUser = userId;
                selfAssessmentModel.assessment.CompanyId = selfAssessmentModel.company.Id;
                await _assessmentRepository.AddAsync(selfAssessmentModel.assessment);

                //---------- Insert Assessment Data ----------
                string xmlAssessmentData = "<ArrayOfAssessmentData>";
                foreach (AssessmentData item in selfAssessmentModel.assessmentData)
                {
                    if (item.AssNo != 0)
                    {
                        xmlAssessmentData += $@"
<AssessmentData>
    <AssessmentId>{ selfAssessmentModel.assessment.Id}</AssessmentId>
    <AssNo>{ item.AssNo }</AssNo>
    <Score>{ item.Score }</Score>
</AssessmentData>";
                    }
                }
                xmlAssessmentData += "</ArrayOfAssessmentData>";

                await _queryRepository.SaveSelfAssessment(xmlAssessmentData, userId, assessmentToDelete == null ? 0 : assessmentToDelete.Id);
                
                _uow.CommitTransaction();
            }
            catch
            {
                _uow.RollbackTransaction();
                throw;
            }
        }

    }
}
