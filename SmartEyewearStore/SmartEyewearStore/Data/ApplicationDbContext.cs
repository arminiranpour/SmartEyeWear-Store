using Microsoft.EntityFrameworkCore;
using SmartEyewearStore.Models;

namespace SmartEyewearStore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
        public DbSet<Glasses> Glasses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Glasses table
            modelBuilder.Entity<Glasses>(entity =>
            {
                entity.ToTable("GLASSES");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Name).HasColumnName("NAME");
                entity.Property(e => e.FrameShape).HasColumnName("FRAME_SHAPE");
                entity.Property(e => e.Color).HasColumnName("COLOR");
                entity.Property(e => e.Style).HasColumnName("STYLE");
                entity.Property(e => e.Usage).HasColumnName("USAGE");
                entity.Property(e => e.Price).HasColumnName("PRICE").HasPrecision(10, 2);
                entity.Property(e => e.ImageUrl).HasColumnName("IMAGE_URL");
            });

            // User table
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USERS");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Username).HasColumnName("USERNAME");
                entity.Property(e => e.Email).HasColumnName("EMAIL");
                entity.Property(e => e.Password).HasColumnName("PASSWORD");
            });

            // SurveyAnswer table
            modelBuilder.Entity<SurveyAnswer>(entity =>
            {
                entity.ToTable("SURVEY_ANSWERS");
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.GlassType).HasColumnName("GLASS_TYPE");
                entity.Property(e => e.Material).HasColumnName("MATERIAL");
                entity.Property(e => e.Gender).HasColumnName("GENDER");
                entity.Property(e => e.Tone).HasColumnName("TONE");
                entity.Property(e => e.FaceShape).HasColumnName("FACE_SHAPE");
                entity.Property(e => e.SkinTone).HasColumnName("SKIN_TONE");
                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.HasOne(e => e.User)
                    .WithMany(u => u.Surveys)
                    .HasForeignKey(e => e.UserId)
                    .HasConstraintName("FK_SA_USERS_UID"); 
            });

        }
    }
}
