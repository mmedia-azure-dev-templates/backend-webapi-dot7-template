-- Table: web."Postulants"

-- DROP TABLE IF EXISTS web."Postulants";

CREATE TABLE IF NOT EXISTS web."Postulants"
(
    "Id" serial NOT NULL,
    "Contacted" boolean,
    "Name" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "SurName" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "UserName" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "Email" character varying(60) COLLATE pg_catalog."default" NOT NULL,
    "CatTypeDocument" integer NOT NULL,
    "CatNacionality" integer NOT NULL,
    "Ndocument" character varying(15) COLLATE pg_catalog."default" NOT NULL,
    "Gender" integer NOT NULL,
    "CatCivilStatus" integer NOT NULL,
    "BirthDate" date NOT NULL,
    "UbcProvincia" integer NOT NULL,
    "UbcCanton" integer NOT NULL,
    "UbcParroquia" integer NOT NULL,
    "Address" character varying(200) COLLATE pg_catalog."default",
    "Phone" character varying(12) COLLATE pg_catalog."default" NOT NULL,
    "Mobile" character varying(15) COLLATE pg_catalog."default" NOT NULL,
    "State" integer NOT NULL DEFAULT 118,
    "ImgUrl" character varying(200) COLLATE pg_catalog."default",
    "CurriculumUrl" character varying(200) COLLATE pg_catalog."default",
    "CreatedAt" timestamp without time zone,
    "UpdatedAt" timestamp without time zone,
    "DeletedAt" timestamp without time zone,
    CONSTRAINT "Postulants_Id_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT "Postulants_Email_key" UNIQUE ("Email"),
    CONSTRAINT "Postulants_Ndocument_key" UNIQUE ("Ndocument"),
    CONSTRAINT "Postulants_UserName_key" UNIQUE ("UserName")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

COMMENT ON TABLE web."Postulants"
    IS 'POSTULANTES AQUI SE GUARDAN LAS PERSONAS QUE SE REGISTRAN EN EL SISTEMA';

insert into web."Postulants"
(
	"Id",
	"Contacted",
	"Name",
	"SurName",
	"UserName",
	"Email",
	"CatTypeDocument",
	"CatNacionality",
	"Ndocument",
	"Gender",
	"CatCivilStatus",
	"BirthDate",
	"UbcProvincia",
	"UbcCanton",
	"UbcParroquia",
	"Address",
	"Phone",
	"Mobile",
	"State",
	"ImgUrl",
	"CurriculumUrl",
	"CreatedAt",
	"UpdatedAt",
	"DeletedAt"
)
select 
    "PTLID",
	"PTLCONTACTED",
	"PTLNOMBRES",
	"PTLAPELLIDOS",
	"PTLUSERNAME",
	"PTLEMAIL",
	"PTLTIPODOCUMENTO",
	"PTLNACIONALIDAD",
	"PTLNDOCUMENTO",
	"PTLGENERO",
	"PTLESTADOCIVIL",
	"PTLFECNACIMIENTO",
	"PTLPROVINCIA",
	"PTLCANTON",
	"PTLPARROQUIA",
	"PTLDIRECCION",
	"PTLTELEFONO",
	"PTLCELULAR",
	"PTLESTADO",
	"PTLIMGURL",
	"PTLHOJAVIDAURL",
	"created_at",
	"updated_at",
	"deleted_at"
    FROM  public.postulants;