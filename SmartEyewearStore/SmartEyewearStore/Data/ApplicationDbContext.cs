using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartEyewearStore.Models;
using SmartEyewearStore.Models.Catalog;

namespace SmartEyewearStore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
        public DbSet<UserInteraction> UserInteractions { get; set; }


        // Catalog tables
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Shape> Shapes { get; set; }
        public DbSet<RimStyle> RimStyles { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<FrameSpecs> FrameSpecs { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<VariantDimensions> VariantDimensions { get; set; }
        public DbSet<VariantImage> VariantImages { get; set; }
        public DbSet<VariantPrice> VariantPrices { get; set; }
        public DbSet<VariantInventory> VariantInventories { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<RatingSummary> RatingSummaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Glasses price precision
            modelBuilder.Entity<UserInteraction>()
                .HasOne(ui => ui.Variant)
                .WithMany()
                .HasForeignKey(ui => ui.VariantId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_UI_VARIANT");

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

            // === Catalog configuration ===

            modelBuilder.Entity<Brand>()
                .HasIndex(b => b.Name)
                .IsUnique();

            modelBuilder.Entity<Material>()
                .HasIndex(m => m.Name)
                .IsUnique();

            modelBuilder.Entity<Shape>()
                .HasIndex(s => s.Name)
                .IsUnique();

            modelBuilder.Entity<RimStyle>()
                .HasIndex(r => r.Name)
                .IsUnique();

            modelBuilder.Entity<Color>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Feature>()
                .HasIndex(f => f.Code)
                .IsUnique();

            modelBuilder.Entity<Tag>()
                .HasIndex(t => t.Name)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Slug)
                .IsUnique();
            modelBuilder.Entity<Product>()
                 .Property(p => p.IsActive)
                 .IsRequired()
                 .HasConversion(new BoolToZeroOneConverter<int?>())
                 .HasColumnType("NUMBER(1)")
                 .HasDefaultValueSql("1");
            modelBuilder.Entity<Product>()
                .Property(p => p.CreatedAt)
                .HasDefaultValueSql("SYSTIMESTAMP");
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId)
                .HasConstraintName("FK_PRODUCT_BRAND");
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.BrandId)
                .HasDatabaseName("IX_PRODUCT_BRAND_ID");

            modelBuilder.Entity<FrameSpecs>()
                .HasKey(f => f.ProductId);
            modelBuilder.Entity<FrameSpecs>()
                 .HasOne(f => f.Product)
                 .WithOne(p => p.FrameSpecs)
                 .HasForeignKey<FrameSpecs>(f => f.ProductId)
                 .OnDelete(DeleteBehavior.Cascade)
                 .HasConstraintName("FK_FRAME_SPECS_PRODUCT");
            modelBuilder.Entity<FrameSpecs>()
                .HasOne(f => f.Material)
                .WithMany(m => m.FrameSpecs)
                .HasForeignKey(f => f.MaterialId)
                .HasConstraintName("FK_FRAMESPECS_MATERIAL");
            modelBuilder.Entity<FrameSpecs>()
                .HasOne(f => f.Shape)
                .WithMany(s => s.FrameSpecs)
                .HasForeignKey(f => f.ShapeId)
                .HasConstraintName("FK_FRAMESPECS_SHAPE");
            modelBuilder.Entity<FrameSpecs>()
                .HasOne(f => f.RimStyle)
                .WithMany(r => r.FrameSpecs)
                .HasForeignKey(f => f.RimStyleId)
                .HasConstraintName("FK_FRAMESPECS_RIMSTYLE");
            modelBuilder.Entity<FrameSpecs>()
                .Property(f => f.WeightG)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ProductVariant>()
                 .HasKey(v => v.VariantId);
            modelBuilder.Entity<ProductVariant>()
                .Property(v => v.Fit)
                .HasColumnType("NUMBER(1)");
            modelBuilder.Entity<ProductVariant>()
            .Property(v => v.IsDefault)
               .IsRequired()
               .HasConversion(new BoolToZeroOneConverter<int?>())
               .HasColumnType("NUMBER(1)")
               .HasDefaultValueSql("0");
            modelBuilder.Entity<ProductVariant>()
                .HasOne(v => v.Product)
                .WithMany(p => p.Variants)
                .HasForeignKey(v => v.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PRODUCT_VARIANT_PRODUCT");
            modelBuilder.Entity<ProductVariant>()
                .HasOne(v => v.Color)
                .WithMany(c => c.Variants)
                .HasForeignKey(v => v.ColorId)
                .HasConstraintName("FK_PRODVAR_COLOR");
            modelBuilder.Entity<ProductVariant>()
                .HasIndex(v => v.ProductId);
            modelBuilder.Entity<ProductVariant>()
                .HasIndex(v => v.ColorId);
            modelBuilder.Entity<ProductVariant>()
                .HasIndex(v => v.ProductId);
            modelBuilder.Entity<ProductVariant>()
                .HasIndex(v => v.ColorId);

            modelBuilder.Entity<VariantDimensions>()
               .HasKey(d => d.VariantId);
            modelBuilder.Entity<VariantDimensions>()
                .HasOne(d => d.Variant)
                .WithOne(v => v.Dimensions)
                .HasForeignKey<VariantDimensions>(d => d.VariantId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_VARIANT_DIMENSIONS");
            modelBuilder.Entity<VariantDimensions>()
                .Property(d => d.LensWidthMm)
                .HasPrecision(18, 2);
            modelBuilder.Entity<VariantDimensions>()
                .Property(d => d.BridgeWidthMm)
                .HasPrecision(18, 2);
            modelBuilder.Entity<VariantDimensions>()
                .Property(d => d.TempleLengthMm)
                .HasPrecision(18, 2);
            modelBuilder.Entity<VariantDimensions>()
                .Property(d => d.LensHeightMm)
                .HasPrecision(18, 2);
            modelBuilder.Entity<VariantDimensions>()
                .Property(d => d.FrameWidthMm)
                .HasPrecision(18, 2);

            modelBuilder.Entity<VariantImage>()
                .HasKey(i => i.ImageId);
            modelBuilder.Entity<VariantImage>()
                .HasOne(i => i.Variant)
                .WithMany(v => v.Images)
                .HasForeignKey(i => i.VariantId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_VARIANT_IMAGE_VARIANT");
            modelBuilder.Entity<VariantImage>()
                .HasIndex(i => i.VariantId)
                .HasDatabaseName("IX_VARIMG_VARID");
            modelBuilder.Entity<VariantImage>()
                .HasIndex(i => new { i.VariantId, i.SortOrder })
                .IsUnique()
                .HasDatabaseName("IX_VARIMG_VARID_SORT");

            modelBuilder.Entity<VariantPrice>()
                .HasKey(p => p.PriceId);
            modelBuilder.Entity<VariantPrice>()
                .HasOne(p => p.Variant)
                .WithMany(v => v.Prices)
                .HasForeignKey(p => p.VariantId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_VARIANT_PRICE_VARIANT");
            modelBuilder.Entity<VariantPrice>()
                .HasIndex(p => p.VariantId);

            modelBuilder.Entity<VariantInventory>()
                .HasKey(i => i.VariantId);
            modelBuilder.Entity<VariantInventory>()
                .Property(i => i.Backorderable)
                .IsRequired()
                .HasConversion(new BoolToZeroOneConverter<int?>())
                .HasColumnType("NUMBER(1)")
                .HasDefaultValueSql("0");
            modelBuilder.Entity<VariantInventory>()
                .HasOne(i => i.Variant)
                .WithOne(v => v.Inventory)
                .HasForeignKey<VariantInventory>(i => i.VariantId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_VARIANT_INVENTORY_VARIANT");

            modelBuilder.Entity<ProductFeature>()
                .HasKey(pf => new { pf.ProductId, pf.FeatureId });
            modelBuilder.Entity<ProductFeature>()
                .HasOne(pf => pf.Product)
                .WithMany(p => p.ProductFeatures)
                .HasForeignKey(pf => pf.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PRODUCT_FEATURE_PRODUCT");
            modelBuilder.Entity<ProductFeature>()
                .HasOne(pf => pf.Feature)
                .WithMany(f => f.ProductFeatures)
                .HasForeignKey(pf => pf.FeatureId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PRODUCT_FEATURE_FEATURE");
            modelBuilder.Entity<ProductFeature>()
                .HasIndex(pf => pf.FeatureId);

            modelBuilder.Entity<ProductTag>()
                .HasKey(pt => new { pt.ProductId, pt.TagId });
            modelBuilder.Entity<ProductTag>()
                .HasOne(pt => pt.Product)
                .WithMany(p => p.ProductTags)
                .HasForeignKey(pt => pt.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PRODUCT_TAG_PRODUCT");
            modelBuilder.Entity<ProductTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.ProductTags)
                .HasForeignKey(pt => pt.TagId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PRODUCT_TAG_TAG");
            modelBuilder.Entity<ProductTag>()
                .HasIndex(pt => pt.TagId);

            modelBuilder.Entity<RatingSummary>()
                .HasKey(r => r.ProductId);
            modelBuilder.Entity<RatingSummary>()
                .HasOne(r => r.Product)
                .WithOne(p => p.RatingSummary)
                .HasForeignKey<RatingSummary>(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_RATING_SUMMARY_PRODUCT");
            modelBuilder.Entity<RatingSummary>()
                .Property(r => r.AvgRating)
                .HasPrecision(18, 2);

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
            const string schema = "DBS311_252NAA12";
            modelBuilder.Entity<User>().ToTable("USERS", schema: schema);
            modelBuilder.Entity<UserInteraction>().ToTable("USERINTERACTIONS", schema: schema);
            modelBuilder.Entity<SurveyAnswer>().ToTable("SURVEYANSWER", schema: schema);

            // Catalog tables
            modelBuilder.Entity<Brand>().ToTable("BRAND", schema: schema);
            modelBuilder.Entity<Material>().ToTable("MATERIAL", schema: schema);
            modelBuilder.Entity<Shape>().ToTable("SHAPE", schema: schema);
            modelBuilder.Entity<RimStyle>().ToTable("RIM_STYLE", schema: schema);
            modelBuilder.Entity<Color>().ToTable("COLOR", schema: schema);
            modelBuilder.Entity<Feature>().ToTable("FEATURE", schema: schema);
            modelBuilder.Entity<Product>().ToTable("PRODUCT", schema: schema);
            modelBuilder.Entity<FrameSpecs>().ToTable("FRAME_SPECS", schema: schema);
            modelBuilder.Entity<ProductVariant>().ToTable("PRODUCT_VARIANT", schema: schema);
            modelBuilder.Entity<VariantDimensions>().ToTable("VARIANT_DIMENSIONS", schema: schema);
            modelBuilder.Entity<VariantImage>().ToTable("VARIANT_IMAGE", schema: schema);
            modelBuilder.Entity<VariantPrice>().ToTable("VARIANT_PRICE", schema: schema);
            modelBuilder.Entity<VariantInventory>().ToTable("VARIANT_INVENTORY", schema: schema);
            modelBuilder.Entity<ProductFeature>().ToTable("PRODUCT_FEATURE", schema: schema);
            modelBuilder.Entity<Tag>().ToTable("TAG", schema: schema);
            modelBuilder.Entity<ProductTag>().ToTable("PRODUCT_TAG", schema: schema);
            modelBuilder.Entity<RatingSummary>().ToTable("RATING_SUMMARY", schema: schema);

        }
    }
}
