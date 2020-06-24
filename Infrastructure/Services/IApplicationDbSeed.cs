using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IApplicationDbSeed
    {
        Task SeedAsync(ApplicationDbContext catalogContext,
            ILoggerFactory loggerFactory);
    }
}
