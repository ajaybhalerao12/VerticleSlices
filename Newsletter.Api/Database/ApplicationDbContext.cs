using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newsletter.Api.Entities;

namespace Newsletter.Api.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>(builder =>
               builder.OwnsOne(a => a.Tags,
               tagsBuilder => tagsBuilder.ToJson()));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(
                warnings => warnings.Ignore(CoreEventId.AccidentalEntityType));
        }

        public DbSet<Article> Articles { get; set; }
    }
}
