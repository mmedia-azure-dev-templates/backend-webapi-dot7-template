using Boilerplate.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Counters : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        //migrationBuilder.Sql(Utils.GetRawSql("20230123175129_CountersUp.sql"));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        //migrationBuilder.Sql(Utils.GetRawSql("20230123175129_CountersDown.sql"));
    }
}
