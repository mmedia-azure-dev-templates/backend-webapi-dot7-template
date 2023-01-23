-- Table: web."Users"

-- DROP TABLE IF EXISTS web."Users";

CREATE TABLE IF NOT EXISTS web."Users"
(
    "Id" serial NOT NULL,
    "Name" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "SurName" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "UserName" character varying(50) COLLATE pg_catalog."default" NOT NULL,
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
    CONSTRAINT "Users_Id_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT "Users_Email_key" UNIQUE ("Email"),
    CONSTRAINT "Users_UserName_key" UNIQUE ("UserName")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

COMMENT ON TABLE web."Users"
    IS 'EN ESTA TABLA SE GUARDAN LOS USUARIOS DEL SISTEMA';
-- Index: Users_SurName_idx

-- DROP INDEX IF EXISTS web."Users_SurName_idx";

CREATE INDEX IF NOT EXISTS "Users_SurName_idx"
    ON web."Users" USING btree
    ("SurName" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: "Users_Name_idx"

-- DROP INDEX IF EXISTS web."Users_Name_idx";

CREATE INDEX IF NOT EXISTS "Users_Name_idx"
    ON web."Users" USING btree
    ("Name" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;


insert into web."Users"
(
	"Id",
	"Name",
	"SurName",
	"UserName",
	"Password",
	"Role",
	"RememberToken",
	"Email",
	"IsActive",
	"LastLogin",
	"LastLoginIp",
	"CreatedAt",
	"UpdatedAt",
	"DeletedAt"
)
select 
    "id",
	"nombres",
	"apellidos",
	"username",
	"password",
    'User',
	"remember_token",
	"email",
	"is_active",
	"last_login",
	"last_login_ip",
	"created_at",
	"updated_at",
	"deleted_at"
    FROM  public.users;

update web."Users" set "Role" = 'Admin' where "Id" = 1