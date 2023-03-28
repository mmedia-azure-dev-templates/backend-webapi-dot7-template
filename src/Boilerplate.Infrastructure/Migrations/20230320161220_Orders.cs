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
                DataKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                PaymentMethodsType = table.Column<int>(type: "int", nullable: false),
                OrderStatusType = table.Column<int>(type: "int", nullable: false),
                OrderNumber = table.Column<long>(type: "bigint", nullable: false),
                Credit = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: true),
                UserGenerated = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserAssigned = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserPaid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CashAdvance = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false, defaultValueSql: "0.00"),
                SubTotal = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false, defaultValueSql: "0.00"),
                Iva = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false, defaultValueSql: "0.00"),
                Total = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false, defaultValueSql: "0.00"),
                Balance = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false, defaultValueSql: "0.00"),
                Term = table.Column<int>(type: "int", nullable: true),
                Observations = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                Notes = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                DocumentUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Documentation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PaidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                PaidState = table.Column<bool>(type: "bit", nullable: true,defaultValueSql: "0"),
                Dispatch = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                Extras = table.Column<string>(type: "nvarchar(max)", nullable: true),
                DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Orders", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Orders",
            schema: "web");
    }
}