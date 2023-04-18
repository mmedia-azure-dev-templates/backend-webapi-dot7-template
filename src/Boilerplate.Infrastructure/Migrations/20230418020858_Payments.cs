using Boilerplate.Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Payments : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "web");

        migrationBuilder.CreateTable(
            name: "Payments",
            schema: "web",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                DataKey = table.Column<string>(type: "varchar(450)", unicode: false, maxLength: 250, nullable: false),
                OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PaymentMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Amount = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false, defaultValue: 0),
                Notes = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                Dateupdated = table.Column<DateTime>(type: "datetime2", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Payments", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Payments",
            schema: "web");
    }
}
