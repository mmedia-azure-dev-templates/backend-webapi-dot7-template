-- Table: web."Users"

-- DROP TABLE IF EXISTS web."Users";

CREATE TABLE IF NOT EXISTS web."Users"
(
    "Id" serial NOT NULL,
    "Nombres" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "Apellidos" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "Username" character varying(50) COLLATE pg_catalog."default" NOT NULL,
	"Password" text COLLATE pg_catalog."default" NOT NULL,
    "Role" text COLLATE pg_catalog."default" NOT NULL,
    "RememberToken" character varying(100) COLLATE pg_catalog."default" DEFAULT 'NULL::character varying',
    "Email" character varying(60) COLLATE pg_catalog."default" NOT NULL,
    "IsActive" smallint NOT NULL,
    "LastLogin" timestamp without time zone,
    "LastLoginIp" character varying(50) COLLATE pg_catalog."default" DEFAULT 'NULL::character varying',
    "CreatedAt" timestamp(0) without time zone NOT NULL,
    "UpdatedAt" timestamp without time zone NOT NULL,
    "DeletedAt" timestamp without time zone,
    CONSTRAINT Users_Id PRIMARY KEY ("Id"),
    CONSTRAINT Users_Email UNIQUE ("Email"),
    CONSTRAINT Users_Username UNIQUE ("Username")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

COMMENT ON TABLE web."Users"
    IS 'EN ESTA TABLA SE GUARDAN LOS USUARIOS DEL SISTEMA';
-- Index: Idx_Users_Apellidos

-- DROP INDEX IF EXISTS web.Idx_Users_Apellidos;

CREATE INDEX IF NOT EXISTS Idx_Users_Apellidos
    ON web."Users" USING btree
    ("Apellidos" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: idx_Users_nombres

-- DROP INDEX IF EXISTS web.Idx_Users_nombres;

CREATE INDEX IF NOT EXISTS Idx_Users_Nombres
    ON web."Users" USING btree
    ("Nombres" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;