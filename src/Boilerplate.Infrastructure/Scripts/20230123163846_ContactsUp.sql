-- Table: web."Contacts"

-- DROP TABLE IF EXISTS web."Contacts";

CREATE TABLE IF NOT EXISTS web."Contacts"
(
    "Id" serial not null,
    "Ndocument" character varying(15) COLLATE pg_catalog."default",
    "Name" character varying(50) COLLATE pg_catalog."default",
    "SurName" character varying(50) COLLATE pg_catalog."default",
    "Email" character varying(50) COLLATE pg_catalog."default",
    "Mobile" character varying(20) COLLATE pg_catalog."default",
    "Phone" character varying(20) COLLATE pg_catalog."default",
    "Address" character varying(400) COLLATE pg_catalog."default",
    "CatTypeDocument" integer,
    "CatNacionality" integer,
    "UbcProvincia" integer,
    "UbcCanton" integer,
    "UbcParroquia" integer,
    "Notes" character varying(50) COLLATE pg_catalog."default",
    "Supervisor" integer,
    "CatCivilStatus" character varying(25) COLLATE pg_catalog."default",
    "BirthDate" timestamp without time zone,
    CONSTRAINT "Contacts_Id_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT "Contacts_Ndocument_key" UNIQUE ("Ndocument")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

COMMENT ON TABLE web."Contacts"
    IS 'TABLA DONDE SE ALMACENAN LOS CLIENTES';
-- Index: IDX_CTOAPELLIDOS

-- DROP INDEX IF EXISTS web."Contacts_SurName_idx";

CREATE INDEX IF NOT EXISTS "Contacts_SurName_idx"
    ON web."Contacts" USING btree
    ("SurName" COLLATE pg_catalog."default" varchar_pattern_ops ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: IDX_CTONOMBRES

-- DROP INDEX IF EXISTS web."Contacts_Name_idx";

CREATE INDEX IF NOT EXISTS "Contacts_Name_idx"
    ON web."Contacts" USING btree
    ("Name" COLLATE pg_catalog."default" varchar_pattern_ops ASC NULLS LAST)
    TABLESPACE pg_default;

insert into web."Contacts"
(
	"Id",
	"Ndocument",
	"Name",
	"SurName",
	"Email",
	"Mobile",
	"Phone",
	"Address",
	"CatTypeDocument",
	"CatNacionality",
	"UbcProvincia",
	"UbcCanton",
	"UbcParroquia",
	"Notes",
	"Supervisor",
	"CatCivilStatus",
	"BirthDate"
)
select 
    "CTOID",
	"CTODOCUMENTO",
	"CTONOMBRES",
	"CTOAPELLIDOS",
	"CTOEMAIL",
	"CTOCELULAR",
	"CTOTELEFONO",
	"CTODIRECCION",
	"CTOTIPODOCUMENTO",
	"CTOTIPONACIONALIDAD",
	"CTOPROVINCIA",
	"CTOCANTON",
	"CTOPARROQUIA",
	"CTONOTAS",
	"CTOSUPERVISOR",
	"CTOESTADOCIVIL",
	"CTOFECHANACIMIENTO"
    FROM  public.contacts;
