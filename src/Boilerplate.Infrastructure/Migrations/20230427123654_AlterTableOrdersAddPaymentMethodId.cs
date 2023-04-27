using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AlterTableOrdersAddPaymentMethodId : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "OrdersTmp",
            schema: "web",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                DataKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Locked = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                OrderStatusType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                OrderNumber = table.Column<long>(type: "bigint", nullable: false),
                PaymentMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                UserGenerated = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserAssigned = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                SubTotal = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false, defaultValueSql: "0.00"),
                Total = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false, defaultValueSql: "0.00"),
                Notes = table.Column<string>(type: "nvarchar(max)", maxLength: 150, nullable: true),
                DocumentUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Documentation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OrdersTmp", x => x.Id);
            });

        migrationBuilder.Sql(@"
            INSERT INTO web.OrdersTmp
            (
                Id,
                DataKey,
                Locked,
                OrderStatusType,
                OrderNumber,
                PaymentMethodId,
                UserGenerated,
                UserAssigned,
                CustomerId,
                SubTotal,
                Total,
                Notes,
                DocumentUrl,
                Documentation,
                DateCreated,
                DateUpdated
            )
            SELECT
                Id,
                DataKey,
                Locked,
                OrderStatusType,
                OrderNumber,
                null,
                UserGenerated,
                UserAssigned,
                CustomerId,
                SubTotal,
                Total,
                Notes,
                DocumentUrl,
                Documentation,
                DateCreated,
                DateUpdated
            FROM web.Orders");

        migrationBuilder.DropForeignKey(
            schema: "web",
            table: "Orders",
            name: "FK_Orders_Customers_CustomerId"
        );

        migrationBuilder.DropForeignKey(
            schema: "web",
            table: "OrderItems",
            name: "FK_OrderItems_Orders_OrderId"
        );

        migrationBuilder.DropTable(
            name: "Orders",
                       schema: "web");

        migrationBuilder.CreateTable(
            name: "Orders",
            schema: "web",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                DataKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Locked = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                OrderStatusType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                OrderNumber = table.Column<long>(type: "bigint", nullable: false),
                PaymentMethodId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                UserGenerated = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserAssigned = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                SubTotal = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false, defaultValueSql: "0.00"),
                Total = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false, defaultValueSql: "0.00"),
                Notes = table.Column<string>(type: "nvarchar(max)", maxLength: 150, nullable: true),
                DocumentUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Documentation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Orders", x => x.Id);
            });

        migrationBuilder.Sql(@"
            INSERT INTO web.Orders
            (
                Id,
                DataKey,
                Locked,
                OrderStatusType,
                OrderNumber,
                PaymentMethodId,
                UserGenerated,
                UserAssigned,
                CustomerId,
                SubTotal,
                Total,
                Notes,
                DocumentUrl,
                Documentation,
                DateCreated,
                DateUpdated
            )
            SELECT
                Id,
                DataKey,
                Locked,
                OrderStatusType,
                OrderNumber,
                null,
                UserGenerated,
                UserAssigned,
                CustomerId,
                SubTotal,
                Total,
                Notes,
                DocumentUrl,
                Documentation,
                DateCreated,
                DateUpdated
            FROM web.OrdersTmp");

        migrationBuilder.DropTable(
            name: "OrdersTmp",
                       schema: "web");

        migrationBuilder.CreateIndex(
                name: "DataKeyOrderNumberIndex",
                schema: "web",
                table: "Orders",
                unique: true,
                columns: new[] { "DataKey", "OrderNumber" }
            );
        migrationBuilder.CreateIndex(
                name: "DataKeyLockedIndex",
                schema: "web",
                table: "Orders",
                unique: false,
                columns: new[] { "DataKey", "Locked" }
            );
        migrationBuilder.CreateIndex(
                name: "DataKeyOrderStatusTypeIndex",
                schema: "web",
                table: "Orders",
                unique: false,
                columns: new[] { "DataKey", "OrderStatusType" }
            );

        migrationBuilder.CreateIndex(
                name: "DataKeyDateCreatedIndex",
                schema: "web",
                table: "Orders",
                unique: false,
                columns: new[] { "DataKey", "DateCreated" }
            );


        migrationBuilder.AddForeignKey(
            schema: "web",
            table: "OrderItems",
            column: "OrderId",
            name: "FK_OrderItems_Orders_OrderId",
            principalSchema: "web",
            principalTable: "Orders",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict
        );

        migrationBuilder.AddForeignKey(
            schema: "web",
            table: "Orders",
            column: "CustomerId",
            name: "FK_Orders_Customers_CustomerId",
            principalSchema: "web",
            principalTable: "Customers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict
        );

        migrationBuilder.AddForeignKey(
            schema: "web",
            table: "Orders",
            column: "PaymentMethodId",
            name: "FK_Orders_PaymentMethods_PaymentMethodId",
            principalSchema: "web",
            principalTable: "PaymentMethods",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict
        );

    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "OrdersTmp",
            schema: "web",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                DataKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Locked = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                OrderStatusType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                OrderNumber = table.Column<long>(type: "bigint", nullable: false),
                UserGenerated = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserAssigned = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                SubTotal = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false, defaultValueSql: "0.00"),
                Total = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false, defaultValueSql: "0.00"),
                Notes = table.Column<string>(type: "nvarchar(max)", maxLength: 150, nullable: true),
                DocumentUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Documentation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OrdersTmp", x => x.Id);
            });

        migrationBuilder.Sql(@"
            INSERT INTO web.OrdersTmp
            (
                Id,
                DataKey,
                Locked,
                OrderStatusType,
                OrderNumber,
                UserGenerated,
                UserAssigned,
                CustomerId,
                SubTotal,
                Total,
                Notes,
                DocumentUrl,
                Documentation,
                DateCreated,
                DateUpdated
            )
            SELECT
                Id,
                DataKey,
                Locked,
                OrderStatusType,
                OrderNumber,
                UserGenerated,
                UserAssigned,
                CustomerId,
                SubTotal,
                Total,
                Notes,
                DocumentUrl,
                Documentation,
                DateCreated,
                DateUpdated
            FROM web.Orders");

        migrationBuilder.DropForeignKey(
            schema: "web",
            table: "OrderItems",
            name: "FK_OrderItems_Orders_OrderId"
        );

        migrationBuilder.DropForeignKey(
            schema: "web",
            table: "Orders",
            name: "FK_Orders_PaymentMethods_PaymentMethodId"
        );

        migrationBuilder.DropForeignKey(
            schema: "web",
            table: "Orders",
            name: "FK_Orders_Customers_CustomerId"
        );

        migrationBuilder.DropTable(
                   name: "Orders",
                          schema: "web");

        migrationBuilder.CreateTable(
            name: "Orders",
            schema: "web",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                DataKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Locked = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "0"),
                OrderStatusType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                OrderNumber = table.Column<long>(type: "bigint", nullable: false),
                UserGenerated = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserAssigned = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                SubTotal = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false, defaultValueSql: "0.00"),
                Total = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false, defaultValueSql: "0.00"),
                Notes = table.Column<string>(type: "nvarchar(max)", maxLength: 150, nullable: true),
                DocumentUrl = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Documentation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Orders", x => x.Id);
            });

        migrationBuilder.Sql(@"
            INSERT INTO web.Orders
            (
                Id,
                DataKey,
                Locked,
                OrderStatusType,
                OrderNumber,
                UserGenerated,
                UserAssigned,
                CustomerId,
                SubTotal,
                Total,
                Notes,
                DocumentUrl,
                Documentation,
                DateCreated,
                DateUpdated
            )
            SELECT
                Id,
                DataKey,
                Locked,
                OrderStatusType,
                OrderNumber,
                UserGenerated,
                UserAssigned,
                CustomerId,
                SubTotal,
                Total,
                Notes,
                DocumentUrl,
                Documentation,
                DateCreated,
                DateUpdated
            FROM web.OrdersTmp");

        migrationBuilder.AddForeignKey(
            schema: "web",
            table: "Orders",
            column: "CustomerId",
            name: "FK_Orders_Customers_CustomerId",
            principalSchema: "web",
            principalTable: "Customers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict
        );

        migrationBuilder.AddForeignKey(
            schema: "web",
            table: "OrderItems",
            column: "OrderId",
            name: "FK_OrderItems_Orders_OrderId",
            principalSchema: "web",
            principalTable: "Orders",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict
        );

        migrationBuilder.CreateIndex(
                name: "DataKeyOrderNumberIndex",
                schema: "web",
                table: "Orders",
                unique: true,
                columns: new[] { "DataKey", "OrderNumber" }
            );
        migrationBuilder.CreateIndex(
                name: "DataKeyLockedIndex",
                schema: "web",
                table: "Orders",
                unique: false,
                columns: new[] { "DataKey", "Locked" }
            );
        migrationBuilder.CreateIndex(
                name: "DataKeyOrderStatusTypeIndex",
                schema: "web",
                table: "Orders",
                unique: false,
                columns: new[] { "DataKey", "OrderStatusType" }
            );

        migrationBuilder.CreateIndex(
                name: "DataKeyDateCreatedIndex",
                schema: "web",
                table: "Orders",
                unique: false,
                columns: new[] { "DataKey", "DateCreated" }
            );

        migrationBuilder.DropTable(
                   name: "OrdersTmp",
                          schema: "web");
    }
}
