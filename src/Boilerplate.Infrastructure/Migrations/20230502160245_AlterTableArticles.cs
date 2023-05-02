using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AlterTableArticles : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(@"
            delete web.Articles where Sku = null;
            delete web.Articles where Sku = '';
            delete web.Articles where Display = null;
            delete web.Articles where Display = '';");

        migrationBuilder.DropColumn(
                    name: "Abrevia",
                    schema: "web",
                    table: "Articles");

        migrationBuilder.DropColumn(
                    name: "Cost",
                    schema: "web",
                    table: "Articles");

        migrationBuilder.AlterColumn<string>(
                    name: "Sku",
                    schema: "web",
                    table: "Articles",
                    maxLength: 450,
                    nullable: false,
                    oldNullable: true);

        migrationBuilder.AlterColumn<string>(
                    name: "Display",
                    schema: "web",
                    table: "Articles",
                    nullable: false,
                    oldNullable: true);

        migrationBuilder.CreateIndex(
                    name: "DataKeySkuIndex",
                    schema: "web",
                    table: "Articles",
                    unique: true,
                    columns: new[] { "DataKey", "Sku" });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Abrevia",
            schema: "web",
            table: "Articles",
            type: "nvarchar(150)",
            maxLength: 150,
            nullable: true);

        migrationBuilder.AddColumn<decimal>(
            name: "Cost",
            schema: "web",
            table: "Articles",
            type: "decimal(14,2)",
            defaultValue: 0,
            precision: 14,
            scale: 2,
            nullable: false);

        migrationBuilder.AlterColumn<string>(
            name: "Sku",
            schema: "web",
            table: "Articles",
            nullable: true,
            oldNullable: false);

        migrationBuilder.AlterColumn<string>(
            name: "Display",
            schema: "web",
            table: "Articles",
            nullable: true,
            oldNullable: false);
    }
}
