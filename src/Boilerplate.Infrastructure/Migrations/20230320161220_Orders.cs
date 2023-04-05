using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Orders : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Orders",
            schema: "web",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                DataKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Locked = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                OrderStatusType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                OrderNumber = table.Column<long>(type: "bigint", nullable: false),
                UserGenerated = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserAssigned = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                SubTotal = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false, defaultValueSql: "0.00"),
                Total = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false, defaultValueSql: "0.00"),
                Notes = table.Column<string>(type: "nvarchar(max)", maxLength: 150, nullable: true),
                DocumentUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Documentation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Orders", x => x.Id);
            });

        migrationBuilder.CreateIndex(
                name: "DataKeyOrderNumberIndex",
                schema: "web",
                table: "Orders",
                unique: true,
                columns: new[] { "DataKey", "OrderNumber" }
            );
        migrationBuilder.CreateIndex(
                name: "DataKeyLockedIndex",
                schema: "web",
                table: "Orders",
                unique: false,
                columns: new[] { "DataKey", "Locked" }
            );
        migrationBuilder.CreateIndex(
                name: "DataKeyOrderStatusTypeIndex",
                schema: "web",
                table: "Orders",
                unique: false,
                columns: new[] { "DataKey", "OrderStatusType" }
            );

        migrationBuilder.CreateIndex(
                name: "DataKeyDateCreatedIndex",
                schema: "web",
                table: "Orders",
                unique: false,
                columns: new[] { "DataKey", "DateCreated" }
            );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Orders",
            schema: "web");
    }
}