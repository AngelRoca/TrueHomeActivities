using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoriesPostgreEF
{
    public class TrueHomeDataContext : DbContext
    {
        public TrueHomeDataContext(DbContextOptions<TrueHomeDataContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();

            modelBuilder.Entity<Activity>()
                .ToTable("Activity");

            modelBuilder.Entity<Property>()
                .ToTable("Property");
        }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<Property> Properties { get; set; }
    }
}
