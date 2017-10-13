using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Infrastructure.Database.DbConfig.Migrations
{
    public partial class change_product_stock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductStocks_ProductVariants_ProductVariantId",
                table: "ProductStocks");

            migrationBuilder.DropIndex(
                name: "IX_ProductStocks_ProductVariantId",
                table: "ProductStocks");

            migrationBuilder.DropColumn(
                name: "ProductVariantId",
                table: "ProductStocks");

            migrationBuilder.AddColumn<int>(
                name: "VariantId",
                table: "ProductStocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductStocks_VariantId",
                table: "ProductStocks",
                column: "VariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStocks_ProductVariants_VariantId",
                table: "ProductStocks",
                column: "VariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductStocks_ProductVariants_VariantId",
                table: "ProductStocks");

            migrationBuilder.DropIndex(
                name: "IX_ProductStocks_VariantId",
                table: "ProductStocks");

            migrationBuilder.DropColumn(
                name: "VariantId",
                table: "ProductStocks");

            migrationBuilder.AddColumn<int>(
                name: "ProductVariantId",
                table: "ProductStocks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductStocks_ProductVariantId",
                table: "ProductStocks",
                column: "ProductVariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductStocks_ProductVariants_ProductVariantId",
                table: "ProductStocks",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
