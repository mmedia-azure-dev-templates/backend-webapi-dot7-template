using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations;

/// <inheritdoc />
public partial class OrderItems : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "web");

        migrationBuilder.CreateTable(
            name: "OrderItems",
            schema: "web",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                DataKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Quantity = table.Column<int>(type: "int", nullable: true),
                Price = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: true),
                Total = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: true),
                DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OrderItems", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "OrderItems",
            schema: "web");

    }
}
