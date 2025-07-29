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
        public DbSet<GlassesInfo> GlassesInfo { get; set; }
        public DbSet<Glasses> Glasses { get; set; }
        public DbSet<UserInteraction> UserInteractions { get; set; }
        public DbSet<SurveyMultiChoice> SurveyMultiChoices { get; set; }
        public DbSet<GlassesFeature> GlassesFeatures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Glasses price precision
            modelBuilder.Entity<Glasses>()
                .Property(g => g.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Glasses>()
                .HasOne(g => g.GlassesInfo)
                .WithMany()
                .HasForeignKey(g => g.GlassesInfoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_GLS_INFO");
            modelBuilder.Entity<Glasses>()
                .Property(g => g.InStock)
                .HasConversion(new BoolToZeroOneConverter<int?>())
                .HasColumnType("NUMBER(1)");
            modelBuilder.Entity<Glasses>()
                .Property(g => g.IsActive)
                .HasConversion(new BoolToZeroOneConverter<int>())
                .HasColumnType("NUMBER(1)");
            modelBuilder.Entity<Glasses>()
                .Property(g => g.PopularityScore)
                .HasPrecision(5, 2);

            modelBuilder.Entity<GlassesInfo>()
                .Property(g => g.HasAntiScratchCoating)
                .HasConversion(new BoolToZeroOneConverter<int?>())
                .HasColumnType("NUMBER(1)");
            modelBuilder.Entity<GlassesInfo>()
                .Property(g => g.LensWidth)
                .HasPrecision(5, 2);
            modelBuilder.Entity<GlassesInfo>()
                .Property(g => g.BridgeWidth)
                .HasPrecision(5, 2);
            modelBuilder.Entity<GlassesInfo>()
                .Property(g => g.TempleLength)
                .HasPrecision(5, 2);
            modelBuilder.Entity<GlassesInfo>()
                .Property(g => g.WeightGrams)
                .HasPrecision(5, 2);
            modelBuilder.Entity<UserInteraction>()
                .HasOne(ui => ui.Glass)
                .WithMany()
                .HasForeignKey(ui => ui.GlassId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_UI_GLS");

            modelBuilder.Entity<UserInteraction>()
                .HasOne(ui => ui.User)
                .WithMany()
                .HasForeignKey(ui => ui.UserId)
                .HasConstraintName("FK_UI_USR");

            // SurveyAnswer foreign key constraint name (short)
            modelBuilder.Entity<SurveyAnswer>()
                .HasOne(s => s.User)
                .WithMany(u => u.Surveys)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SA_USER");

            modelBuilder.Entity<SurveyAnswer>()
                .Property(s => s.Prescription)
                .HasConversion(new BoolToZeroOneConverter<int?>())
                .HasColumnType("NUMBER(1)");

            modelBuilder.Entity<SurveyMultiChoice>()
                .HasOne(mc => mc.Survey)
                .WithMany(s => s.MultiChoices)
                .HasForeignKey(mc => mc.SurveyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_SM_SURVEY");

            modelBuilder.Entity<GlassesFeature>()
                .HasOne(f => f.GlassesInfo)
                .WithMany(g => g.FeaturesList)
                .HasForeignKey(f => f.GlassesInfoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_GF_INFO");
            // Convert boolean to number for Oracle
            modelBuilder.Entity<SurveyAnswer>()
                .Property(s => s.Prescription)
                .HasConversion(new BoolToZeroOneConverter<int?>())
                .HasColumnType("NUMBER(1)");

            // Uppercase all table and column names
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Table
                entity.SetTableName(entity.GetTableName().ToUpper());

                // Columns
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.GetColumnName().ToUpper());
                }

                // Keys
                foreach (var key in entity.GetKeys())
                {
                    key.SetName(key.GetName().ToUpper());
                }

                // Foreign keys
                foreach (var fk in entity.GetForeignKeys())
                {
                    fk.SetConstraintName(fk.GetConstraintName().ToUpper());
                }

                // Indexes
                foreach (var index in entity.GetIndexes())
                {
                    index.SetDatabaseName(index.GetDatabaseName().ToUpper());
                }
            }
            modelBuilder.Entity<Glasses>().ToTable("GLASSES", schema: "DBS311_252NAA12");
            modelBuilder.Entity<GlassesInfo>().ToTable("GLASSESINFO", schema: "DBS311_252NAA12");
            modelBuilder.Entity<User>().ToTable("USERS", schema: "DBS311_252NAA12");
            modelBuilder.Entity<UserInteraction>().ToTable("USERINTERACTIONS", schema: "DBS311_252NAA12");
            modelBuilder.Entity<SurveyAnswer>().ToTable("SURVEYANSWER", schema: "DBS311_252NAA12");
            modelBuilder.Entity<SurveyMultiChoice>().ToTable("SURVEY_MULTI_CHOICES", schema: "DBS311_252NAA12");
            modelBuilder.Entity<GlassesFeature>().ToTable("GLASSES_FEATURES", schema: "DBS311_252NAA12");

        }
    }
}
