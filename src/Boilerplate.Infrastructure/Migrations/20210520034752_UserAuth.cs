using System;
using Microsoft.EntityFrameworkCore.Migrations;
using BC = BCrypt.Net.BCrypt;

namespace Boilerplate.Infrastructure.Migrations;

public partial class UserAuth : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<long>(type: "serial", nullable: false),
                Email = table.Column<string>(type: "varchar(254)", maxLength: 254, nullable: false),
                Password = table.Column<string>(type: "text", nullable: false),
                Role = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
            });

        _ = migrationBuilder.InsertData(
            table: "Users",
            columns: new[] { "Id", "Email", "Password", "Role" },
            values: new object[] { 1, "admin@boilerplate.com", BC.HashPassword("adminpassword"), "Admin" });

        migrationBuilder.InsertData(
            table: "Users",
            columns: new[] { "Id", "Email", "Password", "Role" },
            values: new object[] { 2, "user@boilerplate.com", BC.HashPassword("userpassword"), "User" });

        migrationBuilder.CreateIndex(
            name: "IX_Users_Email",
            table: "Users",
            column: "Email",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Users");
    }
}
