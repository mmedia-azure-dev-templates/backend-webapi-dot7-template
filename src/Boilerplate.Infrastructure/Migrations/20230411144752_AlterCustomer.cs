using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AlterCustomer : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(name: "web");

        migrationBuilder.DropForeignKey(
            name: "FK_Orders_Customers_CustomerId",
            table: "Orders",
            schema: "web"
        );

        migrationBuilder.DropTable(name: "Customers", schema: "web");

        migrationBuilder.CreateTable(
            name: "Customers",
            schema: "web",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                DataKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                DocumentType = table.Column<string>(type: "nvarchar(50)", nullable: false),
                Ndocument = table.Column<string>(type: "nvarchar(30)", nullable: false),
                BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                GenderType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                CivilStatusType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                FirstName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                LastName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                Mobile = table.Column<string>(type: "nvarchar(50)", nullable: false),
                Phone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                Dateupdated = table.Column<DateTime>(type: "datetime2", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Customers", x => x.Id);
            });

    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(name: "web");

        migrationBuilder.CreateTable(
            name: "Customers",
            schema: "web",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                DataKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                DocumentType = table.Column<string>(type: "nvarchar(50)", nullable: false),
                Ndocument = table.Column<string>(type: "nvarchar(30)", nullable: false),
                BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                GenderType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                CivilStatusType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                FirstName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                LastName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                Mobile = table.Column<string>(type: "nvarchar(50)", nullable: false),
                Phone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                Dateupdated = table.Column<DateTime>(type: "datetime2", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Customers", x => x.Id);
            });

        // Foreign Orders.CustomerId to Customers.Id
        migrationBuilder.AddForeignKey(
            schema: "web",
            table: "Orders",
            column: "CustomerId",
            name: "FK_Orders_Customers_CustomerId",
            principalSchema: "web",
            principalTable: "Customers",
            principalColumn: "Id",
            onDelete: ReferentialAction.NoAction
        );

        migrationBuilder.DropTable(name: "Customers", schema: "web");
    }
}
