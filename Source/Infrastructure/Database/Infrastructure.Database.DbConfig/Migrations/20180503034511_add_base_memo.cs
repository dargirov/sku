using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Infrastructure.Database.DbConfig.Migrations
{
    public partial class add_base_memo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BaseMemoId",
                table: "Memos",
                type: "int",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
