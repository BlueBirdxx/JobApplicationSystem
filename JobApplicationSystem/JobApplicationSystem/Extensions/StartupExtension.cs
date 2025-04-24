using Data.DataAccessLayer;
using Microsoft.EntityFrameworkCore;

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
