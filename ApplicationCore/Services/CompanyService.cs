using ApplicationCore.Interfaces;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Specifications;
using System.Linq;

namespace ApplicationCore.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IAppLogger<CompanyService> _logger;

        private readonly IAsyncRepository<Company> _companyRepository;

        private IUnitOfWork _uow;

        public CompanyService(
            IUnitOfWork uow,
            IAsyncRepository<Company> companyRepository,
            IAppLogger<CompanyService> logger)
        {
            _uow = uow;
            _companyRepository = companyRepository;
            _logger = logger;
        }
        public async Task<Company> GetCompanyByUserId(int UserId)
        {
            return (await _companyRepository.ListAsync(new CompanyFilterSpecification(new Company() { CreateUser = UserId }))).FirstOrDefault();
        }


        public async Task AddAsync(Company company)
        {
            await _companyRepository.AddAsync(company);
        }

        public async Task UpdateAsync(Company company)
        {
            await _companyRepository.UpdateAsync(company);
        }
    }
}
