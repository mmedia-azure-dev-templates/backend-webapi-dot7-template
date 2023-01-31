using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations;


public partial class Users : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "dbo");
        migrationBuilder.AddColumn<string>(
            name: "FirstName",
            table: "AspNetUsers",
            type: "nvarchar(200)",
            maxLength: 200,
            nullable: false,
            schema: "dbo"
        );

        migrationBuilder.AddColumn<string>(
            name: "LastName",
            table: "AspNetUsers",
            type: "nvarchar(200)",
            schema: "dbo"
        );

        migrationBuilder.AddColumn<DateTime>(
            name: "LastLogin",
            table: "AspNetUsers",
            schema: "dbo"
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "FirstName",
            table: "AspNetUsers");
        migrationBuilder.DropColumn(
            name: "LastName",
            table: "AspNetUsers");
        migrationBuilder.DropColumn(
            name: "LastLogin",
            table: "AspNetUsers");
    }
}
