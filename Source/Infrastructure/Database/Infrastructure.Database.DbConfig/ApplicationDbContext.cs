using Administration.Entities;
using Client.Entities;
using Infrastructure.Services.Multitenancy;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Database.DbConfig
{
    public class ApplicationDbContext : DbContext
    {
        private readonly Guid _tenantId;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantProvider tenantProvider)
            : base(options)
        {
            _tenantId = tenantProvider.GetTenantId();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<ModulePrivilege> ModulePrivileges { get; set; }
        public DbSet<ConfigOption> ConfigOptions { get; set; }

        public DbSet<Store.Entities.Store> Stores { get; set; }
        public DbSet<Store.Entities.StorePrivilege> StorePrivileges { get; set; }

        public DbSet<Supplier.Entities.Supplier> Suppliers { get; set; }

        public DbSet<Manufacturer.Entities.Manufacturer> Manufacturers { get; set; }

        public DbSet<NaturalClient> NaturalClients { get; set; }
        public DbSet<LegalClient> LegalClients { get; set; }

        public DbSet<Product.Entities.Product> Products { get; set; }
        public DbSet<Product.Entities.ProductCategory> ProductCategories { get; set; }
        public DbSet<Product.Entities.ProductPicture> ProductPictures { get; set; }
        public DbSet<Product.Entities.ProductStock> ProductStocks { get; set; }
        public DbSet<Product.Entities.ProductVariant> ProductVariants { get; set; }
        public DbSet<Product.Entities.ProductPriority> ProductPriorities { get; set; }

        public DbSet<Request.Entities.Request> Requests { get; set; }
        public DbSet<Request.Entities.StockRequest> StockRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Administration
            modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == _tenantId);
            modelBuilder.Entity<Organization>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == _tenantId);
            modelBuilder.Entity<City>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Country>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<File>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<ModulePrivilege>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == _tenantId);
            modelBuilder.Entity<ConfigOption>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == _tenantId);

            // Store
            modelBuilder.Entity<Store.Entities.Store>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == _tenantId);
            modelBuilder.Entity<Store.Entities.StorePrivilege>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == _tenantId);

            // Supplier
            modelBuilder.Entity<Supplier.Entities.Supplier>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == _tenantId);

            // Manufacturer
            modelBuilder.Entity<Manufacturer.Entities.Manufacturer>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == _tenantId);

            // Client
            modelBuilder.Entity<NaturalClient>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == _tenantId);
            modelBuilder.Entity<LegalClient>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == _tenantId);

            // Product
            modelBuilder.Entity<Product.Entities.Product>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == _tenantId);
            modelBuilder.Entity<Product.Entities.ProductCategory>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == _tenantId);
            modelBuilder.Entity<Product.Entities.ProductPicture>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == _tenantId).HasOne(x => x.FullSize).WithOne().OnDelete(DeleteBehavior.Restrict); // delete restrict only on one relational property
            modelBuilder.Entity<Product.Entities.ProductStock>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == _tenantId);
            modelBuilder.Entity<Product.Entities.ProductVariant>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == _tenantId);
            modelBuilder.Entity<Product.Entities.ProductPriority>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == _tenantId);

            // Request
            modelBuilder.Entity<Request.Entities.Request>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == _tenantId);
            modelBuilder.Entity<Request.Entities.StockRequest>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == _tenantId);
        }
    }
}
