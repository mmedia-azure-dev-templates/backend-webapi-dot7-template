using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Boilerplate.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Heroes",
                table => new
                {
                    Id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Heroes");
        }
    }
}
