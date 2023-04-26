using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations;

/// <inheritdoc />
public partial class MigrationPaymentMethods : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
        name: "PaymentMethodsTmp",
        schema: "web",
        columns: table => new
        {
            Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
            DataKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
            PaymentMethodsType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            Display = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            Priority = table.Column<int>(type: "int", nullable: false),
            Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
            Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
            DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
            DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
        },

        constraints: table =>
        {
            table.PrimaryKey("PK_PaymentMethodsTmp", x => x.Id);
        });

        migrationBuilder.Sql(@"
            INSERT INTO web.PaymentMethodsTmp
            (
                Id,
                DataKey,
                PaymentMethodsType,
                Display,
                Priority,
                Active,
                Icon,
                DateCreated,
                DateUpdated
            )
            SELECT
                Id,
                DataKey,
                PaymentMethodsType,
                Display,
                1,
                Active,
                Icon,
                DateCreated,
                DateUpdated
            FROM web.PaymentMethods");

        migrationBuilder.DropTable(
            name: "PaymentMethods",
                       schema: "web");

        //migrationBuilder.RenameTable(
        //    name: "PaymentMethodsTmp",
        //               schema: "web",
        //                          newName: "PaymentMethods",
        //                                     newSchema: "web");

        migrationBuilder.CreateTable(
        name: "PaymentMethods",
        schema: "web",
        columns: table => new
        {
            Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
            DataKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
            PaymentMethodsType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            Display = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            Priority = table.Column<int>(type: "int", nullable: false),
            Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
            Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
            DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
            DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
        },

        constraints: table =>
        {
            table.PrimaryKey("PK_PaymentMethods", x => x.Id);
        });


        migrationBuilder.Sql(@"
            INSERT INTO web.PaymentMethods
            (
                Id,
                DataKey,
                PaymentMethodsType,
                Display,
                Priority,
                Active,
                Icon,
                DateCreated,
                DateUpdated
            )
            SELECT
                Id,
                DataKey,
                PaymentMethodsType,
                Display,
                Priority,
                Active,
                Icon,
                DateCreated,
                DateUpdated
            FROM web.PaymentMethodsTmp");

        migrationBuilder.Sql(@"create Sequence Sq as int minvalue 1 cycle;");
        migrationBuilder.Sql(@"update [web].[PaymentMethods] set Priority=NEXT VALUE FOR Sq");
        migrationBuilder.Sql(@"DROP SEQUENCE Sq");
        migrationBuilder.DropTable(
            name: "PaymentMethodsTmp",
                       schema: "web");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
        name: "PaymentMethodsTmp",
        schema: "web",
        columns: table => new
        {
            Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
            DataKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
            PaymentMethodsType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            Display = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            Priority = table.Column<int>(type: "int", nullable: false),
            Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
            Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
            DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
            DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
        },
        constraints: table =>
        {
            table.PrimaryKey("PK_PaymentMethodsTmp", x => x.Id);
        });

        migrationBuilder.Sql(@"
            INSERT INTO web.PaymentMethodsTmp
            (
                Id,
                DataKey,
                PaymentMethodsType,
                Display,
                Priority,
                Active,
                Icon,
                DateCreated,
                DateUpdated
            )
            SELECT
                Id,
                DataKey,
                PaymentMethodsType,
                Display,
                Priority,
                Active,
                Icon,
                DateCreated,
                DateUpdated
            FROM web.PaymentMethods");

        migrationBuilder.DropTable(
                   name: "PaymentMethods",
                          schema: "web");

        migrationBuilder.CreateTable(
        name: "PaymentMethods",
        schema: "web",
        columns: table => new
        {
            Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
            DataKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
            PaymentMethodsType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            Display = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
            Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
            Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
            DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
            DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
        },

        constraints: table =>
        {
            table.PrimaryKey("PK_PaymentMethods", x => x.Id);
        });

        migrationBuilder.Sql(@"
            INSERT INTO web.PaymentMethods
            (
                Id,
                DataKey,
                PaymentMethodsType,
                Display,
                Active,
                Icon,
                DateCreated,
                DateUpdated
            )
            SELECT
                Id,
                DataKey,
                PaymentMethodsType,
                Display,
                Active,
                Icon,
                DateCreated,
                DateUpdated
            FROM web.PaymentMethodsTmp");

        migrationBuilder.DropTable(
                   name: "PaymentMethodsTmp",
                          schema: "web");

    }
}
