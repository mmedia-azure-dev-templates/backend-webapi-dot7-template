using Boilerplate.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations;


public partial class Users : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(Utils.GetRawSql("20230122144334_UsersUp.sql"));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(Utils.GetRawSql("20230122144334_UsersDown.sql"));
    }
}
