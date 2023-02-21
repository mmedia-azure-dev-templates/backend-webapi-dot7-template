using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Teams : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
        schema: "web",
        name: "Teams",
        columns: table => new
        {
            Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
            UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
            //HyerarchyId = table.Column<HierarchyId>(type: "hierarchyid(50)", nullable: false),
            //OldHyerarchyId = table.Column<string>(type: "nvarchar(50)", nullable: false),
            DataKey = table.Column<string>(type: "nvarchar(450)", nullable: true),
            DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
            Dateupdated = table.Column<DateTime>(type: "datetime2", nullable: true),
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_UserInformations", x => x.Id);
        });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        
    }
}
