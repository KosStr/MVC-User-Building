using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WEBCOPY1.Models;

namespace WEBCOPY1.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Building> Building { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<BuildingUser> BuildingUser { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Building>().ToTable("Building");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<BuildingUser>().ToTable("BuildingUser");
        }
    }
}
