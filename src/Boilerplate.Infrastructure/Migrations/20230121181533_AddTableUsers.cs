using BCrypt.Net;
using Boilerplate.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddTableUsers : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        var baseDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts");
        var path = Path.Combine(baseDirectory, "20230121181533_AddTableUsers.sql");
        migrationBuilder.Sql(File.ReadAllText(path));

    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {

    }
}
