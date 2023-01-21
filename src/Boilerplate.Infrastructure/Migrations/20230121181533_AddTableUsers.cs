using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddTableUsers : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("-- Table: web.\"Users\"\r\n\r\n-- DROP TABLE IF EXISTS web.\"Users\";\r\n\r\nCREATE TABLE IF NOT EXISTS web.\"Users\"\r\n(\r\n    \"Id\" serial NOT NULL,\r\n    \"Nombres\" character varying(50) COLLATE pg_catalog.\"default\" NOT NULL,\r\n    \"Apellidos\" character varying(50) COLLATE pg_catalog.\"default\" NOT NULL,\r\n    \"Username\" character varying(50) COLLATE pg_catalog.\"default\" NOT NULL,\r\n\t\"Password\" text COLLATE pg_catalog.\"default\" NOT NULL,\r\n    \"Role\" text COLLATE pg_catalog.\"default\" NOT NULL,\r\n    \"RememberToken\" character varying(100) COLLATE pg_catalog.\"default\" DEFAULT 'NULL::character varying',\r\n    \"Email\" character varying(60) COLLATE pg_catalog.\"default\" NOT NULL,\r\n    \"IsActive\" smallint NOT NULL,\r\n    \"LastLogin\" timestamp without time zone,\r\n    \"LastLoginIp\" character varying(50) COLLATE pg_catalog.\"default\" DEFAULT 'NULL::character varying',\r\n    \"CreatedAt\" timestamp(0) without time zone NOT NULL,\r\n    \"UpdatedAt\" timestamp without time zone NOT NULL,\r\n    \"DeletedAt\" timestamp without time zone,\r\n    CONSTRAINT Users_Id PRIMARY KEY (\"Id\"),\r\n    CONSTRAINT Users_Email UNIQUE (\"Email\"),\r\n    CONSTRAINT Users_Username UNIQUE (\"Username\")\r\n)\r\nWITH (\r\n    OIDS = FALSE\r\n)\r\nTABLESPACE pg_default;\r\n\r\nCOMMENT ON TABLE web.\"Users\"\r\n    IS 'EN ESTA TABLA SE GUARDAN LOS USUARIOS DEL SISTEMA';\r\n-- Index: Idx_Users_Apellidos\r\n\r\n-- DROP INDEX IF EXISTS web.Idx_Users_Apellidos;\r\n\r\nCREATE INDEX IF NOT EXISTS Idx_Users_Apellidos\r\n    ON web.\"Users\" USING btree\r\n    (\"Apellidos\" COLLATE pg_catalog.\"default\" ASC NULLS LAST)\r\n    TABLESPACE pg_default;\r\n-- Index: idx_Users_nombres\r\n\r\n-- DROP INDEX IF EXISTS web.Idx_Users_nombres;\r\n\r\nCREATE INDEX IF NOT EXISTS Idx_Users_Nombres\r\n    ON web.\"Users\" USING btree\r\n    (\"Nombres\" COLLATE pg_catalog.\"default\" ASC NULLS LAST)\r\n    TABLESPACE pg_default;");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {

    }
}
