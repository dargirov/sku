using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Infrastructure.Database.DbConfig.Migrations
{
    public partial class add_request_privs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RequestDelete",
                table: "ModulePrivileges",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequestRead",
                table: "ModulePrivileges",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequestWrite",
                table: "ModulePrivileges",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestDelete",
                table: "ModulePrivileges");

            migrationBuilder.DropColumn(
                name: "RequestRead",
                table: "ModulePrivileges");

            migrationBuilder.DropColumn(
                name: "RequestWrite",
                table: "ModulePrivileges");
        }
    }
}
