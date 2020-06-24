using ApplicationCore.Entities;
using ApplicationCore.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ICompanyService
    {
        Task<Company> GetCompanyByUserId(int UserId);

        Task AddAsync(Company company);

        Task UpdateAsync(Company company);
    }
}
