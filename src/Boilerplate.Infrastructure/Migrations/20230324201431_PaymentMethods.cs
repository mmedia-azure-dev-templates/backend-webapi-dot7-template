using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations;

/// <inheritdoc />
public partial class PaymentMethods : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
        name: "PaymentMethods",
        schema: "web",
        columns: table => new
        {
            Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
            DataKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
            PaymentMethodsType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
            DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
            DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_PaymentMethods", x => x.Id);
        });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
        name: "PaymentMethods",
        schema: "web");
    }
}
