using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Matchs : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Foreign Orders.CustomerId to Customers.Id
        migrationBuilder.AddForeignKey(
            name: "FK_Orders_Customers_CustomerId",
            table: "Orders",
            column: "CustomerId",
            schema: "web",
            principalSchema: "web",
            principalTable: "Customers",
            principalColumn: "Id",
            onDelete: ReferentialAction.NoAction
        );

        // Foreignkey OrderItems.ArticleId to Articles.Id
        migrationBuilder.AddForeignKey(
            name: "FK_OrderItems_Articles_ArticleId",
            table: "OrderItems",
            column: "ArticleId",
            schema: "web",
            principalSchema: "web",
            principalTable: "Articles",
            principalColumn: "Id",
            onDelete: ReferentialAction.NoAction
        );

        // Foreignkey OrderItems.OrderId to Orders.Id
        migrationBuilder.AddForeignKey(
            name: "FK_OrderItems_Orders_OrderId",
            table: "OrderItems",
            column: "OrderId",
            schema: "web",
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
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_OrderItems_Articles_ArticleId",
            table: "OrderItems",
            schema: "web"
        );

        migrationBuilder.DropForeignKey(
            name: "FK_Orders_Customers_CustomerId",
            table: "Orders",
            schema: "web"
        );
    }
}
