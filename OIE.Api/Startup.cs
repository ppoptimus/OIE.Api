
using ApplicationCore.Interfaces;
using IdentityServer4.AccessTokenValidation;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Linq;
using System.Reflection;
using Infrastructure.Identity;
using ApplicationCore;
using Infrastructure.Logging;
using ApplicationCore.Entities;
using AutoMapper;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using ApplicationCore.Services;
using IdentityServer4.Services;
using OIE.Api.IdentityServer;
using IdentityServer4.EntityFramework.DbContexts;
using System.Collections.Generic;

namespace OIE.Api
{

    public class Startup
    {
        private const string KeyFilePath = "KeyFilePath";
        private const string KeyFilePassword = "KeyFilePassword";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var migrationsAssembly = typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Infrastructure")));

            services.AddDbContext<ApplicationIdentityDbContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Infrastructure")));

            services.AddDbContext<PersistedGrantDbContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Infrastructure")));

            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<LocalizedIdentityErrorDescriber>();


            // Identity options.
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                options.User.RequireUniqueEmail = true;

                // Lockout settings.
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 30;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
            });

            // Role based Authorization: policy based role checks.
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Config.Authenticated, policy => policy.RequireRole(ApplicationRole.Administrator, ApplicationRole.OIEOfficer, ApplicationRole.RegisteredUser));
                options.AddPolicy(Config.SelfAssessmentPolicy, policy => policy.RequireRole(ApplicationRole.Administrator, ApplicationRole.OIEOfficer, ApplicationRole.RegisteredUser));
                options.AddPolicy(Config.PMIPolicy, policy => policy.RequireRole(ApplicationRole.Administrator, ApplicationRole.OIEOfficer, ApplicationRole.RegisteredUser));
                options.AddPolicy(Config.SurveyPolicy, policy => policy.RequireRole(ApplicationRole.Administrator, ApplicationRole.OIEOfficer, ApplicationRole.RegisteredUser));
                options.AddPolicy(Config.AdminPolicy, policy => policy.RequireRole(ApplicationRole.Administrator));

            });


            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

            //services.AddMemoryCache();
            //services.Configure<CatalogSettings>(Configuration);
            //services.AddSingleton<IUriComposer>(new UriComposer(Configuration.Get<CatalogSettings>()));

            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // Adds application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IIdentityDbSeed, ApplicationIdentityDbSeed>();
            services.AddTransient<IApplicationDbSeed, ApplicationDbSeed>();
            services.AddTransient<IQueryRepository, QueryRepository>();
            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<ISelfAssessmentService, SelfAssessmentService>();
            services.AddTransient<IIndustryReviewService, IndustryReviewService>();
            services.AddTransient<IDocumentService, DocumentService>();
            services.AddTransient<IIndustryIndicatorService, IndustryIndicatorService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IPMIService, PMIService>();
            services.AddTransient<ISurveyService, SurveyService>();

            // services.AddTransient<IHtmlReporitory, HtmlReporitory>();

            //services.AddTransient<IContentRepository, ContentRepository>();


            var keyOptions = Configuration.GetSection("SigninKeyCredentials");
            var keyFilePath = keyOptions.GetValue<string>(KeyFilePath);
            var keyFilePassword = keyOptions.GetValue<string>(KeyFilePassword);


            // Adds IdentityServer.
            var builder = services.AddIdentityServer()
                // The AddDeveloperSigningCredential extension creates temporary key material for signing tokens.
                // This might be useful to get started, but needs to be replaced by some persistent key material for production scenarios.
                // See the http://docs.identityserver.io/en/release/topics/crypto.html#refcrypto for more information.

                .AddDeveloperSigningCredential()
                //.AddSigningCredential(new X509Certificate2(keyFilePath, keyFilePassword))

                //.AddInMemoryPersistedGrants()

                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                })
                // To configure IdentityServer to use EntityFramework (EF) as the storage mechanism for configuration data (rather than using the in-memory implementations),
                // see https://identityserver4.readthedocs.io/en/release/quickstarts/8_entity_framework.html
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddAspNetIdentity<ApplicationUser>();

            //builder.Services.AddTransient<IProfileService, ProfileService>();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = ServerUtils.ConfigInfo.APIServer;
                    options.RequireHttpsMetadata = false;

                    options.ApiName = "OieApi";
                });


            // Registers the Swagger generator, defining one or more Swagger documents.
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "WebAPI", Version = "v1" });
                c.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "password",
                    AuthorizationUrl = $"{ServerUtils.ConfigInfo.APIServer}/connect/authorize",
                    TokenUrl = $"{ServerUtils.ConfigInfo.APIServer}/connect/token",
                    Scopes = new Dictionary<string, string>
                        {
                            { "OieApi","" },
                            { "offline_access","" },
                            { "openid","" },
                            { "profile","" },
                            { "roles","" }
                        }
                });
                // c.DocumentFilter<SecurityRequirementsDocumentFilter>(); // c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> // { //     { "oauth2", new string[] { } } // });
                // c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                // {
                //     In = "header",
                //     Description = "Please insert JWT with Bearer into field",
                //     Name = "Authorization",
                //     Type = "apiKey"
                // });
                // c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                // {
                //     { "Bearer", new string[] { } }
                // });

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "oauth2", new string[] { } }
                });

            });

            services.AddMvc();

            services.AddAutoMapper();

            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                                                                   .AllowAnyMethod()
                                                                    .AllowAnyHeader()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Router on the server must match the router on the client (see app.routing.module.ts) to use PathLocationStrategy.
            //var appRoutes = new[] {
            //     "/home",
            //     "/account/signin",
            //     "/account/signup",
            //     "/resources",
            //     "/dashboard"
            // };

            //app.Use(async (context, next) =>
            //{
            //    if (context.Request.Path.HasValue && appRoutes.Contains(context.Request.Path.Value))
            //    {
            //        context.Request.Path = new PathString("/");
            //    }

            //    await next();
            //});

            app.UseIdentityServer();

            app.UseAuthentication();

            app.UseCors("AllowAll");

            app.UseMvc();

            // Microsoft.AspNetCore.StaticFiles: API for starting the application from wwwroot.
            // Uses default files as index.html.
            app.UseDefaultFiles();
            // Uses static file for the current path.
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), $"{ServerUtils.ConfigInfo.MediaFolder}")),
                RequestPath = new PathString($"/{ServerUtils.ConfigInfo.MediaFolder}")
            });

            if (env.IsDevelopment())
            {
                // Enables middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enables middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1");
                    c.OAuthClientId("SWAGGER");
                    c.OAuthAppName("SWAGGER");
                    c.OAuthScopeSeparator(" ");
                    c.OAuthClientSecret("");
                    c.OAuthUseBasicAuthenticationWithAccessCodeGrant();

                });
            }
        }
    }
}
