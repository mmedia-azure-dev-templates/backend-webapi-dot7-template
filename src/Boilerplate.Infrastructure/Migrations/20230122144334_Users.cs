using Azure.Storage.Blobs.Models;
using Boilerplate.Infrastructure.Configuration;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Graph;
using StackExchange.Redis;
using System;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Users : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                SurName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                RememberToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                IsActive = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastLogin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LastLoginIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                CreatedAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UpdatedAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                DeletedAt = table.Column<string>(type: "nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUsers", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Users");
    }
}
