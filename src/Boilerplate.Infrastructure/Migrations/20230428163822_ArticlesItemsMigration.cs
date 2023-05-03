using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations;

/// <inheritdoc />
public partial class ArticlesItemsMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "web");

        migrationBuilder.CreateTable(
            name: "ArticlesItems",
            schema: "web",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                DataKey = table.Column<string>(type: "varchar(450)", unicode: false, maxLength: 250, nullable: false),
                ArticleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PaymentMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Price = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false),
                DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                Dateupdated = table.Column<DateTime>(type: "datetime2", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ArticlesItems", x => x.Id);
            });

        migrationBuilder.AddForeignKey(
                       name: "FK_ArticlesItems_Articles_ArticleId",
                       schema: "web",
                       table: "ArticlesItems",
                       column: "ArticleId",
                       principalSchema: "web",
                       principalTable: "Articles",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
                        name: "FK_ArticlesItems_PaymentMethods_PaymentMethodId",
                        schema: "web",
                        table: "ArticlesItems",
                        column: "PaymentMethodId",
                        principalSchema: "web",
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
                        name: "FK_ArticlesItems_Articles_ArticleId",
                        schema: "web",
                        table: "ArticlesItems");
        migrationBuilder.DropForeignKey(
                        name: "FK_ArticlesItems_PaymentMethods_PaymentMethodId",
                        schema: "web",
                        table: "ArticlesItems");
        migrationBuilder.DropTable(
                        schema: "web",
                        name: "ArticlesItems");
    }
}
