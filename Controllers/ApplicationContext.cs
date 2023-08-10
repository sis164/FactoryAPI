using Microsoft.EntityFrameworkCore;
using FactoryAPI.Models;

namespace FactoryAPI.Controllers
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Factory> Factory { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=db_enterprise;Username=postgres;Password=ssddss123");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }
}
