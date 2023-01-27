using Boilerplate.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Products : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        //migrationBuilder.Sql(Utils.GetRawSql("20230123185853_ProductsUp.sql"));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        //migrationBuilder.Sql(Utils.GetRawSql("20230123185853_ProductsDown.sql"));
    }
}
