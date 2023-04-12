using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AlterCustomer : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(name: "web");
        migrationBuilder.Sql(@"IF (EXISTS (SELECT 1 FROM sys.objects WHERE name = 'FK_Orders_Customers_CustomerId'))
        BEGIN
            ALTER TABLE [web].Orders DROP CONSTRAINT FK_Orders_Customers_CustomerId
        END");
        migrationBuilder.Sql("ALTER TABLE web.Orders ALTER COLUMN CustomerId UniqueIdentifier NULL;");
        migrationBuilder.Sql("UPDATE web.Orders SET CustomerId = null");

        //migrationBuilder.DropForeignKey(
        //    name: "FK_Orders_Customers_CustomerId",
        //    table: "Orders",
        //    schema: "web"
        //);
        migrationBuilder.DropTable(name: "Customers", schema: "web");

        migrationBuilder.CreateTable(
            name: "Customers",
            schema: "web",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                DataKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                DocumentType = table.Column<string>(type: "nvarchar(50)", nullable: false),
                Ndocument = table.Column<string>(type: "nvarchar(30)", nullable: false),
                BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                GenderType = table.Column<string>(type: "nvarchar(450)", nullable: true),
                CivilStatusType = table.Column<string>(type: "nvarchar(450)", nullable: true),
                FirstName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                LastName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                Mobile = table.Column<string>(type: "nvarchar(50)", nullable: true),
                Phone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                Dateupdated = table.Column<DateTime>(type: "datetime2", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Customers", x => x.Id);
            });

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

        //migrationBuilder.CreateIndex(
        //        name: "DataKeyEmailIndex",
        //        schema: "web",
        //        table: "Customers",
        //        unique: true,
        //        columns: new[] { "DataKey", "Email" }
        //    );

        migrationBuilder.Sql(@"CREATE UNIQUE INDEX [DataKeyEmailIndex]
        ON web.Customers (DataKey, Email)
        WHERE Email IS NOT NULL ;");

        //migrationBuilder.CreateIndex(
        //    name: "DataKeyNdocumentIndex",
        //    schema: "web",
        //    table: "Customers",
        //    unique: true,
        //    columns: new[] { "DataKey", "Ndocument" }
        //);

        migrationBuilder.Sql(@"CREATE UNIQUE INDEX [DataKeyNdocumentIndex]
        ON web.Customers (DataKey, DocumentType,Ndocument)
        WHERE DocumentType IS NOT NULL AND Ndocument IS NOT NULL;");

        migrationBuilder.CreateIndex(
            name: "FirstNameIndex",
            schema: "web",
            table: "Customers",
            columns: new[] { "DataKey", "FirstName" }
        );

        migrationBuilder.CreateIndex(
            name: "LastNameIndex",
            schema: "web",
            table: "Customers",
            columns: new[] { "DataKey", "LastName" }
        );

    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(name: "web");
        migrationBuilder.DropForeignKey(
            name: "FK_Orders_Customers_CustomerId",
            table: "Orders",
            schema: "web"
        );
        migrationBuilder.DropTable(name: "Customers", schema: "web");
        /*Original Table*/
        migrationBuilder.CreateTable(
            name: "Customers",
            schema: "web",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                DataKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                DocumentType = table.Column<string>(type: "nvarchar(50)", nullable: false),
                Ndocument = table.Column<string>(type: "nvarchar(30)", nullable: false),
                BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                GenderType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                CivilStatusType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                FirstName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                LastName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                Mobile = table.Column<string>(type: "nvarchar(50)", nullable: false),
                Phone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                PrimaryStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                SecondaryStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Numeration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Provincia = table.Column<int>(type: "int", nullable: false),
                Canton = table.Column<int>(type: "int", nullable: false),
                Parroquia = table.Column<int>(type: "int", nullable: false),
                Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                Dateupdated = table.Column<DateTime>(type: "datetime2", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Customers", x => x.Id);
            });

        migrationBuilder.CreateIndex(
                name: "DataKeyEmailIndex",
                schema: "web",
                table: "Customers",
                unique: true,
                columns: new[] { "DataKey", "Email" }
            );

        migrationBuilder.CreateIndex(
            name: "DataKeyNdocumentIndex",
            schema: "web",
            table: "Customers",
            unique: true,
            columns: new[] { "DataKey", "Ndocument" }
        );

        migrationBuilder.CreateIndex(
            name: "FirstNameIndex",
            schema: "web",
            table: "Customers",
            columns: new[] { "DataKey", "FirstName" }
        );

        migrationBuilder.CreateIndex(
            name: "LastNameIndex",
            schema: "web",
            table: "Customers",
            columns: new[] { "DataKey", "LastName" }
        );

        //migrationBuilder.AddForeignKey(
        //    schema: "web",
        //    table: "Orders",
        //    column: "CustomerId",
        //    name: "FK_Orders_Customers_CustomerId",
        //    principalSchema: "web",
        //    principalTable: "Customers",
        //    principalColumn: "Id",
        //    onDelete: ReferentialAction.NoAction
        //);
    }
}
