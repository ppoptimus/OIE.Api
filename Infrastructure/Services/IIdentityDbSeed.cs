using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IIdentityDbSeed
    {
        Task SeedAsync(ApplicationIdentityDbContext context);
    }
}
