using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AlterOrderItem : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "web");

        migrationBuilder.DropTable(
            name: "OrderItems",
            schema: "web");

        migrationBuilder.CreateTable(
            name: "OrderItems",
            schema: "web",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                DataKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Delivered = table.Column<bool>(type: "bit", defaultValue: false, nullable: false),
                Quantity = table.Column<int>(type: "int", nullable: false),
                Price = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                Total = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OrderItems", x => x.Id);
            });

        migrationBuilder.AddForeignKey(
            schema: "web",
            table: "OrderItems",
            column: "ArticleId",
            name: "FK_OrderItems_Articles_ArticleId",
            principalSchema: "web",
            principalTable: "Articles",
            principalColumn: "Id",
            onDelete: ReferentialAction.NoAction
        );

        migrationBuilder.AddForeignKey(
            schema: "web",
            table: "OrderItems",
            column: "OrderId",
            name: "FK_OrderItems_Orders_OrderId",
            principalSchema: "web",
            principalTable: "Orders",
            principalColumn: "Id",
            onDelete: ReferentialAction.NoAction
        );

    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "web");

        migrationBuilder.DropTable(
            name: "OrderItems",
            schema: "web");

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
                Price = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                Total = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OrderItems", x => x.Id);
            });

    }
}
