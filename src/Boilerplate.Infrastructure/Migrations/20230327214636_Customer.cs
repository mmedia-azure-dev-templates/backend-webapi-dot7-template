﻿using System;
using Amazon.S3.Model;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Customer : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
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
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Customers",
            schema: "web"); 
    }
}
