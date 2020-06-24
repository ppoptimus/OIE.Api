using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Serilog;
using Serilog.Events;
using ApplicationCore;
using IdentityServer4.EntityFramework.DbContexts;

namespace OIE.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var host = BuildWebHost(args);

            // Initializes db.
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var applicationDbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
                    var applicationDbSeed = serviceProvider.GetRequiredService<IApplicationDbSeed>();
                    var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
                    applicationDbContext.Database.Migrate();

                    applicationDbSeed.SeedAsync(applicationDbContext, loggerFactory);


                    var applicationIdentityDbContext = serviceProvider.GetRequiredService<ApplicationIdentityDbContext>();

                    var persistedGrantDbContext = serviceProvider.GetRequiredService<PersistedGrantDbContext>();

                    var identityDbSeed = serviceProvider.GetRequiredService<IIdentityDbSeed>();
                    applicationIdentityDbContext.Database.Migrate();
                    persistedGrantDbContext.Database.Migrate();

                    identityDbSeed.SeedAsync(applicationIdentityDbContext).GetAwaiter().GetResult();

                }
                catch (Exception ex)
                {
                    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseKestrel()
            .UseIISIntegration()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseStartup<Startup>()
            .UseUrls(ServerUtils.ConfigInfo.APIServer)
            .ConfigureLogging(builder =>
            {
                 builder.ClearProviders();
                 builder.AddSerilog();
            })
            .Build();
    }
}
