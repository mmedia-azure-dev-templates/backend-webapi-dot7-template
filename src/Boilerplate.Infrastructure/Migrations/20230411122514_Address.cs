using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Address : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "web");

        migrationBuilder.CreateTable(
            name: "Address",
            schema: "web",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                DataKey = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PrimaryStreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                SecondaryStreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Numeration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Provincia = table.Column<int>(type: "int", nullable: true),
                Canton = table.Column<int>(type: "int", nullable: true),
                Parroquia = table.Column<int>(type: "int", nullable: true),
                Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Address", x => x.Id);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Address_PersonId",
            schema: "web",
            table: "Address",
            column: "PersonId"
        );

    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "web");

        migrationBuilder.DropTable(
            name: "Address",
            schema: "web");
    }
}
