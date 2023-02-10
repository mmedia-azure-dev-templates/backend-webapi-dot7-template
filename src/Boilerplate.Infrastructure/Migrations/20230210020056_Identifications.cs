using Boilerplate.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Identifications : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
        schema: "web",
        name: "UserInformations",
        columns: table => new
        {
            Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
            UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            TypeDocument = table.Column<string>(type: "nvarchar(50)", nullable: false),
            Nacionality = table.Column<string>(type: "nvarchar(50)", nullable: false),
            Ndocument = table.Column<string>(type: "nvarchar(30)", nullable: false),
            Gender = table.Column<string>(type: "nvarchar(450)", nullable: false),
            CivilStatus = table.Column<string>(type: "nvarchar(450)", nullable: false),
            BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            EntryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            DepartureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
            Hired = table.Column<bool>(type: "bit", nullable: false),
            ImgUrl = table.Column<string>(type: "nvarchar(200)", nullable: false),
            CurriculumUrl = table.Column<string>(type: "nvarchar(200)", nullable: true),
            Mobile = table.Column<string>(type: "nvarchar(50)", nullable: true),
            Phone = table.Column<string>(type: "nvarchar(50)", nullable: true),
            PrimaryStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
            SecondaryStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
            Numeration = table.Column<string>(type: "nvarchar(max)", nullable: false),
            Reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
            Provincia = table.Column<int>(type: "int", nullable: false),
            Canton = table.Column<int>(type: "int", nullable: false),
            Parroquia = table.Column<int>(type: "int", nullable: false),
            Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_UserInformations", x => x.Id);
        });

        migrationBuilder.Sql(Utils.GetRawSql("20230122165751_IdentificationsUp.sql"));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            schema: "web",
            name: "UserInformations");
    }
}
