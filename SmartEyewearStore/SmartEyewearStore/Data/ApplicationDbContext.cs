
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartEyewearStore.Models;

namespace SmartEyewearStore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
        public DbSet<Glasses> Glasses { get; set; }

        // using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Glasses>()
                .Property(g => g.Price)
                .HasPrecision(10, 2);

           
        }

    }

}
