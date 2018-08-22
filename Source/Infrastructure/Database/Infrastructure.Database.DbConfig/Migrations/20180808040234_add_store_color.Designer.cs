﻿// <auto-generated />
using Administration.Entities;
using Infrastructure.Database.DbConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Product.Entities;
using Request.Entities;
using System;

namespace Infrastructure.Database.DbConfig.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180808040234_add_store_color")]
    partial class add_store_color
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Administration.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("Highlight");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("PostCode");

                    b.Property<int>("Type");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Administration.Entities.ConfigOption", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Category");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Entity")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("EntityId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<Guid>("TenantId");

                    b.Property<int>("Type");

                    b.Property<string>("Value")
                        .IsRequired();

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.ToTable("ConfigOptions");
                });

            modelBuilder.Entity("Administration.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("Highlight");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("NameEn")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Administration.Entities.File", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<Guid>("Guid")
                        .HasMaxLength(100);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Path");

                    b.Property<int>("StorageType");

                    b.Property<Guid>("TenantId");

                    b.Property<int>("UserId");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Administration.Entities.Memo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BaseEntityId");

                    b.Property<string>("BaseEntityName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime?>("ChangedOn");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<int>("EntityId");

                    b.Property<string>("EntityName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("NewValue");

                    b.Property<string>("OldValue");

                    b.Property<string>("Property");

                    b.Property<Guid>("TenantId");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.ToTable("Memos");
                });

            modelBuilder.Entity("Administration.Entities.ModulePrivilege", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("CategoryDelete");

                    b.Property<bool>("CategoryRead");

                    b.Property<bool>("CategoryWrite");

                    b.Property<bool>("ClientDelete");

                    b.Property<bool>("ClientRead");

                    b.Property<bool>("ClientWrite");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IncomeDelete");

                    b.Property<bool>("IncomeRead");

                    b.Property<bool>("IncomeWrite");

                    b.Property<bool>("InvoiceDelete");

                    b.Property<bool>("InvoiceRead");

                    b.Property<bool>("InvoiceWrite");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("ManufacturerDelete");

                    b.Property<bool>("ManufacturerRead");

                    b.Property<bool>("ManufacturerWrite");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<bool>("ProductDelete");

                    b.Property<bool>("ProductImport");

                    b.Property<bool>("ProductRead");

                    b.Property<bool>("ProductWrite");

                    b.Property<bool>("RequestDelete");

                    b.Property<bool>("RequestRead");

                    b.Property<bool>("RequestWrite");

                    b.Property<bool>("SaleDelete");

                    b.Property<bool>("SaleRead");

                    b.Property<bool>("SaleWrite");

                    b.Property<bool>("StoreDelete");

                    b.Property<bool>("StoreRead");

                    b.Property<bool>("StoreWrite");

                    b.Property<bool>("SupplierDelete");

                    b.Property<bool>("SupplierRead");

                    b.Property<bool>("SupplierWrite");

                    b.Property<Guid>("TenantId");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.ToTable("ModulePrivileges");
                });

            modelBuilder.Entity("Administration.Entities.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("HashId")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Mol")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<Guid>("TenantId");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("Administration.Entities.PageData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("GridId")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<int>("PageSize");

                    b.Property<Guid>("TenantId");

                    b.Property<int>("UserId");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PageData");
                });

            modelBuilder.Entity("Administration.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(100);

                    b.Property<string>("Comment")
                        .HasMaxLength(1000);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("IsAdmin");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastActivity");

                    b.Property<DateTime>("LastLogIn");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<int>("ModulePrivilegeId");

                    b.Property<int>("OrganizationId");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<string>("Phone")
                        .HasMaxLength(20);

                    b.Property<Guid>("TenantId");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.HasIndex("ModulePrivilegeId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Client.Entities.LegalClient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(200);

                    b.Property<int>("CityId");

                    b.Property<string>("Comment")
                        .HasMaxLength(1000);

                    b.Property<int>("CountryId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Eik")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Email")
                        .HasMaxLength(50);

                    b.Property<string>("FirmName")
                        .HasMaxLength(100);

                    b.Property<bool>("HasDds");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Mol")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Phone")
                        .HasMaxLength(50);

                    b.Property<Guid>("TenantId");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("CountryId");

                    b.ToTable("LegalClients");
                });

            modelBuilder.Entity("Client.Entities.NaturalClient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(200);

                    b.Property<int>("CityId");

                    b.Property<string>("Comment")
                        .HasMaxLength(1000);

                    b.Property<int>("CountryId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Email")
                        .HasMaxLength(50);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<string>("PersonalNo")
                        .HasMaxLength(20);

                    b.Property<string>("Phone")
                        .HasMaxLength(50);

                    b.Property<Guid>("TenantId");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("CountryId");

                    b.ToTable("NaturalClients");
                });

            modelBuilder.Entity("Manufacturer.Entities.Manufacturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment")
                        .HasMaxLength(1000);

                    b.Property<int>("CountryId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Email")
                        .HasMaxLength(50);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<Guid>("TenantId");

                    b.Property<string>("Url")
                        .HasMaxLength(100);

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("Product.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("ManufacturerId");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<int?>("SupplierId");

                    b.Property<Guid>("TenantId");

                    b.Property<int>("Version");

                    b.Property<string>("Warranty")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ManufacturerId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Product.Entities.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<Guid>("TenantId");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("Product.Entities.ProductPicture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<int>("FullSizeId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<int>("ProductId");

                    b.Property<Guid>("TenantId");

                    b.Property<int>("ThumbId");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.HasIndex("FullSizeId")
                        .IsUnique();

                    b.HasIndex("ProductId");

                    b.HasIndex("ThumbId");

                    b.ToTable("ProductPictures");
                });

            modelBuilder.Entity("Product.Entities.ProductPriority", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<int>("Priority");

                    b.Property<int>("ProductId");

                    b.Property<Guid>("TenantId");

                    b.Property<int>("UserId");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("ProductPriorities");
                });

            modelBuilder.Entity("Product.Entities.ProductStock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LowQuantity");

                    b.Property<int>("LowQuantityMeasureType");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<int>("Quantity");

                    b.Property<int>("QuantityMeasureType");

                    b.Property<int>("StoreId");

                    b.Property<Guid>("TenantId");

                    b.Property<int>("VariantId");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.HasIndex("StoreId");

                    b.HasIndex("VariantId");

                    b.ToTable("ProductStocks");
                });

            modelBuilder.Entity("Product.Entities.ProductVariant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<decimal>("PriceCustomer");

                    b.Property<int>("PriceCustomerType");

                    b.Property<decimal>("PriceNet");

                    b.Property<int>("PriceNetType");

                    b.Property<int>("ProductId");

                    b.Property<Guid>("TenantId");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductVariants");
                });

            modelBuilder.Entity("Request.Entities.Request", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<int>("Status");

                    b.Property<Guid>("TenantId");

                    b.Property<string>("Text");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("Request.Entities.StockRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<int>("FromStoreId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<int>("Priority");

                    b.Property<int>("Quantity");

                    b.Property<int?>("RequestId");

                    b.Property<int>("StockId");

                    b.Property<Guid>("TenantId");

                    b.Property<int>("ToStoreId");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.HasIndex("FromStoreId");

                    b.HasIndex("RequestId");

                    b.HasIndex("StockId");

                    b.HasIndex("ToStoreId");

                    b.ToTable("StockRequests");
                });

            modelBuilder.Entity("Store.Entities.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Bank")
                        .HasMaxLength(50);

                    b.Property<int>("CityId");

                    b.Property<string>("Color")
                        .HasMaxLength(20);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<string>("HashId")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Iban")
                        .HasMaxLength(30);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<Guid>("TenantId");

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("Store.Entities.StorePrivilege", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("Delete");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<bool>("Read");

                    b.Property<int>("StoreId");

                    b.Property<Guid>("TenantId");

                    b.Property<int>("UserId");

                    b.Property<int>("Version");

                    b.Property<bool>("Write");

                    b.HasKey("Id");

                    b.HasIndex("StoreId");

                    b.HasIndex("UserId");

                    b.ToTable("StorePrivileges");
                });

            modelBuilder.Entity("Supplier.Entities.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(200);

                    b.Property<string>("Comment")
                        .HasMaxLength(1000);

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<string>("Email")
                        .HasMaxLength(50);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Mol")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Phone")
                        .HasMaxLength(50);

                    b.Property<Guid>("TenantId");

                    b.Property<string>("Url")
                        .HasMaxLength(100);

                    b.Property<int>("Version");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("Administration.Entities.File", b =>
                {
                    b.HasOne("Administration.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Administration.Entities.PageData", b =>
                {
                    b.HasOne("Administration.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Administration.Entities.User", b =>
                {
                    b.HasOne("Administration.Entities.ModulePrivilege", "ModulePrivilege")
                        .WithMany()
                        .HasForeignKey("ModulePrivilegeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Administration.Entities.Organization", "Organization")
                        .WithMany("Users")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Client.Entities.LegalClient", b =>
                {
                    b.HasOne("Administration.Entities.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Administration.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Client.Entities.NaturalClient", b =>
                {
                    b.HasOne("Administration.Entities.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Administration.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Manufacturer.Entities.Manufacturer", b =>
                {
                    b.HasOne("Administration.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Product.Entities.Product", b =>
                {
                    b.HasOne("Product.Entities.ProductCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Manufacturer.Entities.Manufacturer", "Manufacturer")
                        .WithMany()
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Supplier.Entities.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId");
                });

            modelBuilder.Entity("Product.Entities.ProductPicture", b =>
                {
                    b.HasOne("Administration.Entities.File", "FullSize")
                        .WithOne()
                        .HasForeignKey("Product.Entities.ProductPicture", "FullSizeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Product.Entities.Product", "Product")
                        .WithMany("Pictures")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Administration.Entities.File", "Thumb")
                        .WithMany()
                        .HasForeignKey("ThumbId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Product.Entities.ProductPriority", b =>
                {
                    b.HasOne("Product.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Administration.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Product.Entities.ProductStock", b =>
                {
                    b.HasOne("Store.Entities.Store", "Store")
                        .WithMany()
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Product.Entities.ProductVariant", "Variant")
                        .WithMany("Stocks")
                        .HasForeignKey("VariantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Product.Entities.ProductVariant", b =>
                {
                    b.HasOne("Product.Entities.Product", "Product")
                        .WithMany("Variants")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Request.Entities.StockRequest", b =>
                {
                    b.HasOne("Store.Entities.Store", "FromStore")
                        .WithMany()
                        .HasForeignKey("FromStoreId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Request.Entities.Request", "Request")
                        .WithMany("StockRequests")
                        .HasForeignKey("RequestId");

                    b.HasOne("Product.Entities.ProductStock", "Stock")
                        .WithMany()
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Store.Entities.Store", "ToStore")
                        .WithMany()
                        .HasForeignKey("ToStoreId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Store.Entities.Store", b =>
                {
                    b.HasOne("Administration.Entities.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Store.Entities.StorePrivilege", b =>
                {
                    b.HasOne("Store.Entities.Store", "Store")
                        .WithMany()
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Administration.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
