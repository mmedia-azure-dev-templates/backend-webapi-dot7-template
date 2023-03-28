using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Articles : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "web");

        migrationBuilder.CreateTable(
            name: "Articles",
            schema: "web",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                DataKey = table.Column<string>(type: "varchar(450)", unicode: false, maxLength: 250, nullable: false),
                Provider = table.Column<int>(type: "int", nullable: true),
                Sku = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                Abrevia = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                Display = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Cost = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                Brand = table.Column<int>(type: "int", nullable: true),
                Notes = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                Meta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Discontinued = table.Column<bool>(type: "bit", nullable: true),
                DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                Dateupdated = table.Column<DateTime>(type: "datetime2", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Articles", x => x.Id);
            });

    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            schema: "web",
            name: "Articles");
    }
}
