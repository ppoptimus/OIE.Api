using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Data
{
    public class TestDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        //public ApplicationDbContext Create()
        //{
        //    var environmentName =
        //                Environment.GetEnvironmentVariable(
        //                    "Hosting:Environment");

        //    var basePath = AppContext.BaseDirectory;

        //    return Create(basePath, environmentName);
        //}

        //public ApplicationDbContext Create(DbContextFactoryOptions options)
        //{
        //    return Create(Directory.GetCurrentDirectory(), Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
        //}

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfigurationRoot configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json")
             .AddJsonFile($"appsettings.{environmentName}.json", true)
             .Build();


            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new ApplicationDbContext(builder.Options);
        }

        //private ApplicationDbContext Create(string basePath, string environmentName)
        //{
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(basePath)
        //        .AddJsonFile("appsettings.json")
        //        .AddJsonFile($"appsettings.{environmentName}.json", true)
        //        .AddEnvironmentVariables();

        //    var config = builder.Build();

        //    var connstr = config.GetConnectionString("DefaultConnection");

        //    if (String.IsNullOrWhiteSpace(connstr) == true)
        //    {
        //        throw new InvalidOperationException(
        //            "Could not find a connection string named 'DefaultConnection'.");
        //    }
        //    else
        //    {
        //        return Create(connstr);
        //    }
        //}

        //private ApplicationDbContext Create(string connectionString)
        //{
        //    if (string.IsNullOrEmpty(connectionString))
        //        throw new ArgumentException(
        //            $"{nameof(connectionString)} is null or empty.",
        //            nameof(connectionString));

        //    var optionsBuilder =
        //        new DbContextOptionsBuilder<ApplicationDbContext>();

        //    optionsBuilder.UseSqlServer(connectionString);
        //    //optionsBuilder.UseMySQL(connectionString);

        //    return new ApplicationDbContext(optionsBuilder.Options);
        //}
    }
}

