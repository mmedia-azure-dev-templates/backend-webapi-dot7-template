using System;
using Boilerplate.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore.Migrations;
using BC = BCrypt.Net.BCrypt;

namespace Boilerplate.Infrastructure.Migrations;

public partial class UserAuth : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(Utils.GetRawSql("20210520034752_AddTableUsers.sql"));
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Users","web");
    }
}
