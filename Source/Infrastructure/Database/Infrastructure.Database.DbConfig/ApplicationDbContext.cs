﻿using Administration.Entities;
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

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<File> Files { get; set; }

        public virtual DbSet<Store.Entities.Store> Stores { get; set; }

        public virtual DbSet<Supplier.Entities.Supplier> Suppliers { get; set; }

        public virtual DbSet<Manufacturer.Entities.Manufacturer> Manufacturers { get; set; }

        public virtual DbSet<NaturalClient> NaturalClients { get; set; }
        public virtual DbSet<LegalClient> LegalClients { get; set; }

        public virtual DbSet<Product.Entities.Product> Products { get; set; }
        public virtual DbSet<Product.Entities.ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<Product.Entities.ProductPicture> ProductPictures { get; set; }
        public virtual DbSet<Product.Entities.ProductStock> ProductStocks { get; set; }
        public virtual DbSet<Product.Entities.ProductVariant> ProductVariants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Administration
            modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == _tenantId);
            modelBuilder.Entity<Organization>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == _tenantId);
            modelBuilder.Entity<City>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Country>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<File>().HasQueryFilter(x => !x.IsDeleted);

            // Store
            modelBuilder.Entity<Store.Entities.Store>().HasQueryFilter(x => !x.IsDeleted && x.TenantId == _tenantId);

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
        }
    }
}
