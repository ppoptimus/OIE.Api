using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Threading;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Country> Country { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Assessment> Assessment { get; set; }
        public DbSet<AssessmentData> AssessmentData { get; set; }
        public DbSet<AssessmentBase> AssessmentBase { get; set; }
        public DbSet<IndustryReview> IndustryReview { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<PMIHEAD> PMI { get; set; }
        public DbSet<PMIDATA> PMIDATA { get; set; }
        public DbSet<PrimaryData> PrimaryData { get; set; }
        public DbSet<PrimaryDataHead> PrimaryDataHead { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<Basket>(ConfigureBasket);
            //builder.Entity<CatalogBrand>(ConfigureCatalogBrand)
        }


        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            AddTimestamps();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            //var currentUsername = !string.IsNullOrEmpty(System.Web.HttpContext.Current?.User?.Identity?.Name)
            //    ? HttpContext.Current.User.Identity.Name
            //    : "Anonymous";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreateDate = DateTime.UtcNow;
                    ((BaseEntity)entity.Entity).CreateUser = ((BaseEntity)entity.Entity).ModifyUser;
                }

                ((BaseEntity)entity.Entity).ModifyDate = DateTime.UtcNow;


                //if (((BaseEntity)entity.Entity).DeleteFlag)
                //{
                //    ((BaseEntity)entity.Entity).DeletedAt = DateTime.UtcNow;
                //    ((BaseEntity)entity.Entity).DeletedBy = ((BaseEntity)entity.Entity).ModifyUser;
                //}

            }
        }

        //private void ConfigureBasket(EntityTypeBuilder<Basket> builder)
        //{
        //    var navigation = builder.Metadata.FindNavigation(nameof(Basket.Items));

        //    navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        //}

        //private void ConfigureCatalogItem(EntityTypeBuilder<CatalogItem> builder)
        //{
        //    builder.ToTable("Catalog");

        //    builder.Property(ci => ci.Id)
        //        .ForSqlServerUseSequenceHiLo("catalog_hilo")
        //        .IsRequired();

        //    builder.Property(ci => ci.Name)
        //        .IsRequired(true)
        //        .HasMaxLength(50);

        //    builder.Property(ci => ci.Price)
        //        .IsRequired(true);

        //    builder.Property(ci => ci.PictureUri)
        //        .IsRequired(false);

        //    builder.HasOne(ci => ci.CatalogBrand)
        //        .WithMany()
        //        .HasForeignKey(ci => ci.CatalogBrandId);

        //    builder.HasOne(ci => ci.CatalogType)
        //        .WithMany()
        //        .HasForeignKey(ci => ci.CatalogTypeId);
        //}

        //private void ConfigureCatalogBrand(EntityTypeBuilder<CatalogBrand> builder)
        //{
        //    builder.ToTable("CatalogBrand");

        //    builder.HasKey(ci => ci.Id);

        //    builder.Property(ci => ci.Id)
        //       .ForSqlServerUseSequenceHiLo("catalog_brand_hilo")
        //       .IsRequired();

        //    builder.Property(cb => cb.Brand)
        //        .IsRequired()
        //        .HasMaxLength(100);
        //}

        //private void ConfigureCatalogType(EntityTypeBuilder<CatalogType> builder)
        //{
        //    builder.ToTable("CatalogType");

        //    builder.HasKey(ci => ci.Id);

        //    builder.Property(ci => ci.Id)
        //       .ForSqlServerUseSequenceHiLo("catalog_type_hilo")
        //       .IsRequired();

        //    builder.Property(cb => cb.Type)
        //        .IsRequired()
        //        .HasMaxLength(100);
        //}

        //private void ConfigureOrder(EntityTypeBuilder<Order> builder)
        //{
        //    var navigation = builder.Metadata.FindNavigation(nameof(Order.OrderItems));

        //    navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

        //    builder.OwnsOne(o => o.ShipToAddress);
        //}

        //private void ConfigureOrderItem(EntityTypeBuilder<OrderItem> builder)
        //{
        //    builder.OwnsOne(i => i.ItemOrdered);
        //}
    }
}
