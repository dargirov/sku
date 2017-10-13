using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Infrastructure.Database.DbConfig.Migrations
{
    public partial class add_module_privileges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModulePrivilegeId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ModulePrivileges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryDelete = table.Column<bool>(type: "bit", nullable: false),
                    CategoryRead = table.Column<bool>(type: "bit", nullable: false),
                    CategoryWrite = table.Column<bool>(type: "bit", nullable: false),
                    ClientDelete = table.Column<bool>(type: "bit", nullable: false),
                    ClientRead = table.Column<bool>(type: "bit", nullable: false),
                    ClientWrite = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IncomeDelete = table.Column<bool>(type: "bit", nullable: false),
                    IncomeRead = table.Column<bool>(type: "bit", nullable: false),
                    IncomeWrite = table.Column<bool>(type: "bit", nullable: false),
                    InvoiceDelete = table.Column<bool>(type: "bit", nullable: false),
                    InvoiceRead = table.Column<bool>(type: "bit", nullable: false),
                    InvoiceWrite = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ManufacturerDelete = table.Column<bool>(type: "bit", nullable: false),
                    ManufacturerRead = table.Column<bool>(type: "bit", nullable: false),
                    ManufacturerWrite = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductDelete = table.Column<bool>(type: "bit", nullable: false),
                    ProductRead = table.Column<bool>(type: "bit", nullable: false),
                    ProductWrite = table.Column<bool>(type: "bit", nullable: false),
                    SaleDelete = table.Column<bool>(type: "bit", nullable: false),
                    SaleRead = table.Column<bool>(type: "bit", nullable: false),
                    SaleWrite = table.Column<bool>(type: "bit", nullable: false),
                    StoreDelete = table.Column<bool>(type: "bit", nullable: false),
                    StoreRead = table.Column<bool>(type: "bit", nullable: false),
                    StoreWrite = table.Column<bool>(type: "bit", nullable: false),
                    SupplierDelete = table.Column<bool>(type: "bit", nullable: false),
                    SupplierRead = table.Column<bool>(type: "bit", nullable: false),
                    SupplierWrite = table.Column<bool>(type: "bit", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModulePrivileges", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ModulePrivilegeId",
                table: "Users",
                column: "ModulePrivilegeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ModulePrivileges_ModulePrivilegeId",
                table: "Users",
                column: "ModulePrivilegeId",
                principalTable: "ModulePrivileges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ModulePrivileges_ModulePrivilegeId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "ModulePrivileges");

            migrationBuilder.DropIndex(
                name: "IX_Users_ModulePrivilegeId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModulePrivilegeId",
                table: "Users");
        }
    }
}
