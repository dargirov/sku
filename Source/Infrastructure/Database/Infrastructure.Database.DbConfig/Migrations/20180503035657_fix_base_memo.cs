using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Infrastructure.Database.DbConfig.Migrations
{
    public partial class fix_base_memo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memos_Memos_BaseMemoId",
                table: "Memos");

            migrationBuilder.DropIndex(
                name: "IX_Memos_BaseMemoId",
                table: "Memos");

            migrationBuilder.DropColumn(
                name: "BaseMemoId",
                table: "Memos");

            migrationBuilder.AddColumn<string>(
                name: "BaseEntityName",
                table: "Memos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseEntityName",
                table: "Memos");

            migrationBuilder.AddColumn<int>(
                name: "BaseMemoId",
                table: "Memos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Memos_BaseMemoId",
                table: "Memos",
                column: "BaseMemoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Memos_Memos_BaseMemoId",
                table: "Memos",
                column: "BaseMemoId",
                principalTable: "Memos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
