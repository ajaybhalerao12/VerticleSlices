using Microsoft.EntityFrameworkCore;
using Newsletter.Api.Database;

namespace Newsletter.Api.Extensions
{   
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNSwagServices(this IServiceCollection services)
        {
            services.AddOpenApiDocument();
            return services;
        }
        public static IServiceCollection AddDbContext(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration
                .GetConnectionString("DefaultConnection")));

            return services;
        }
        public static IHealthChecksBuilder AddPostgreSQLHealthCheck(this IHealthChecksBuilder services,
            IConfiguration configuration)
        {
            services.AddNpgSql(configuration.GetConnectionString("DefaultConnection"));
            return services;
        }
    }
}
