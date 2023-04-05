using System;
using Boilerplate.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Matchs : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "EmailIndex",
            table: "AspNetUsers"
        );

        migrationBuilder.DropIndex(
            name: "NormalizedEmailIndex",
            table: "AspNetUsers"
        );

        migrationBuilder.DropIndex(
            name: "FirstNameIndex",
            schema: "dbo",
            table: "AspNetUsers"
        );

        migrationBuilder.DropIndex(
            name: "LastNameIndex",
            schema: "dbo",
            table: "AspNetUsers"
        );

        // Foreign Orders.CustomerId to Customers.Id
        migrationBuilder.AddForeignKey(
            schema: "web",
            table: "Orders",
            column: "CustomerId",
            name: "FK_Orders_Customers_CustomerId",
            principalSchema: "web",
            principalTable: "Customers",
            principalColumn: "Id",
            onDelete: ReferentialAction.NoAction
        );

        // Foreignkey OrderItems.ArticleId to Articles.Id
        migrationBuilder.AddForeignKey(
            schema: "web",
            table: "OrderItems",
            column: "ArticleId",
            name: "FK_OrderItems_Articles_ArticleId",
            principalSchema: "web",
            principalTable: "Articles",
            principalColumn: "Id",
            onDelete: ReferentialAction.NoAction
        );

        // Foreignkey OrderItems.OrderId to Orders.Id
        migrationBuilder.AddForeignKey(
            schema: "web",
            table: "OrderItems",
            column: "OrderId",
            name: "FK_OrderItems_Orders_OrderId",
            principalSchema: "web",
            principalTable: "Orders",
            principalColumn: "Id",
            onDelete: ReferentialAction.NoAction
        );

        migrationBuilder.CreateIndex(
            name: "EmailIndex",
            table: "AspNetUsers",
            column: "Email"
        );

        migrationBuilder.CreateIndex(
            name: "NormalizedEmailIndex",
            table: "AspNetUsers",
            column: "NormalizedEmail"
        );

        migrationBuilder.CreateIndex(
            name: "FirstNameIndex",
            schema: "dbo",
            table: "AspNetUsers",
            column: "FirstName",
            filter: "[FirstName] IS NOT NULL"
        );

        migrationBuilder.CreateIndex(
            name: "LastNameIndex",
            schema: "dbo",
            table: "AspNetUsers",
            column: "LastName",
            filter: "[LastName] IS NOT NULL"
        );

        migrationBuilder.Sql(Utils.GetRawSql("20230328170146_Matchs.sql"));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        //migrationBuilder.DropForeignKey(
        //    name: "FK_OrderItems_Articles_ArticleId",
        //    table: "OrderItems",
        //    schema: "web"
        //);

        //migrationBuilder.DropForeignKey(
        //    name: "FK_Orders_Customers_CustomerId",
        //    table: "Orders",
        //    schema: "web"
        //);
    }
}
