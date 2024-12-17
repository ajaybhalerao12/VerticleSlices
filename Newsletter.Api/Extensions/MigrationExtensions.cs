using Microsoft.EntityFrameworkCore;
using Newsletter.Api.Database;

namespace Newsletter.Api.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this WebApplication application)
        {
            using var scope = application.Services.CreateScope();
            var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            applicationDbContext.Database.Migrate();
        }
    }
}
