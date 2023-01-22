using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Heroes : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable("Heroes",
        table => new
        {
            Id = table.Column<long>(type: "serial", nullable: false),
            Name = table.Column<string>("text", nullable: false),
            Nickname = table.Column<string>("text", nullable: true),
            Individuality = table.Column<string>("text", nullable: true),
            Age = table.Column<int>("int", nullable: true),
            HeroType = table.Column<int>("int", nullable: false),
            Team = table.Column<string>("text", nullable: true)
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_Heroes", x => x.Id);
        },
        schema: "web"
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable("Heroes", "web");
    }
}
