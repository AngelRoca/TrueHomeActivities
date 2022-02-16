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
        }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<Property> Properties { get; set; }
    }
}
