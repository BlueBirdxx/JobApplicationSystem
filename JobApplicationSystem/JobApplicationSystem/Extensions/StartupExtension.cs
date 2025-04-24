using Data.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using Service.Services;

namespace JobApplicationSystem.Extensions
{
    public static class StartupExtension
    {
        public static void ConfigDbContext(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("JobApplicationSystem"));
        }

        public static void BusinessServices(this IServiceCollection services)
        {
            services.AddScoped<ICandidateService, CandidateService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IJobApplicationService, JobApplicationService>();
            services.AddScoped<IJobPostService, JobPostService>();
        }

        public static void BusinessRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();
            services.AddScoped<IJobPostRepository, JobPostRepository>();
        }

        public static void ConfigCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                      builder =>
                      {
                          builder
                                 .AllowAnyHeader()
                                 .SetIsOriginAllowed((host) => true)
                                 .AllowAnyMethod()
                                 .AllowCredentials();
                      });
            });
        }
    }
}
