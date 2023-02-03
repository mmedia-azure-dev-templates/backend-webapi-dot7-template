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
        migrationBuilder.EnsureSchema(
            name: "dbo");
        
        migrationBuilder.AddColumn<int>(
            name: "LegacyId",
            table: "AspNetUsers",
            type: "int",
            nullable: false,
            schema: "dbo"
        ).Annotation("SqlServer:Identity", "1, 1");

        migrationBuilder.Sql("SET IDENTITY_INSERT [dbo].[AspNetUsers] ON");

        migrationBuilder.AddColumn<string>(
            name: "FirstName",
            table: "AspNetUsers",
            type: "nvarchar(200)",
            maxLength: 200,
            nullable: false,
            schema: "dbo"
        );

        migrationBuilder.AddColumn<string>(
            name: "LastName",
            table: "AspNetUsers",
            type: "nvarchar(200)",
            schema: "dbo"
        );

        migrationBuilder.AddColumn<DateTime>(
            name: "LastLogin",
            table: "AspNetUsers",
            schema: "dbo",
            nullable: true
        );

        migrationBuilder.Sql(Utils.GetRawSql("20230122144334_UsersUp.sql"));

        migrationBuilder.Sql("SET IDENTITY_INSERT [dbo].[AspNetUsers] OFF");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "LegacyId",
            table: "AspNetUsers");
        
        migrationBuilder.DropColumn(
            name: "FirstName",
            table: "AspNetUsers");
        
        migrationBuilder.DropColumn(
            name: "LastName",
            table: "AspNetUsers");
        
        migrationBuilder.DropColumn(
            name: "LastLogin",
            table: "AspNetUsers");
        
        migrationBuilder.Sql(Utils.GetRawSql("20230122144334_UsersDown.sql"));
    }
}
