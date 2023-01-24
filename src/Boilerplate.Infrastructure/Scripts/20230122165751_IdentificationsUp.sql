-- Table: web."Identifications"

-- DROP TABLE IF EXISTS web."Identifications";

CREATE TABLE IF NOT EXISTS web."Identifications"
(
    "Id" serial NOT NULL,
    "UserId" integer NOT NULL DEFAULT 0,
    "CatTypeDocument" integer NOT NULL DEFAULT 0,
    "CatNacionality" integer NOT NULL,
    "Ndocument" character varying(15) COLLATE pg_catalog."default" NOT NULL,
    "CatGender" integer,
    "CatCivilStatus" integer,
    "BirthDate" date,
    "EntryDate" date,
    "DepartureDate" date,
    "Hired" smallint NOT NULL DEFAULT 0,
    "ImgUrl" character varying(200) COLLATE pg_catalog."default",
    "CurriculumUrl" character varying(200) COLLATE pg_catalog."default",
    "Mobile" character varying(50) COLLATE pg_catalog."default",
    "Phone" character varying(50) COLLATE pg_catalog."default",
    "Address" character varying(200) COLLATE pg_catalog."default",
    "UbcProvincia" integer,
    "UbcCanton" integer,
    "UbcParroquia" integer,
    "Notes" character varying(50) COLLATE pg_catalog."default",
    CONSTRAINT "Identifications_Id_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT "Identifications_Ndocumento_key" UNIQUE ("Ndocument"),
    CONSTRAINT "Identifications_UserId_key" UNIQUE ("UserId"),
    CONSTRAINT "Identifications_UserId_Users" FOREIGN KEY ("UserId")
        REFERENCES web."Users" ("Id") MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;
COMMENT ON TABLE web."Identifications"
    IS 'TABLA HACE JOIN CON TABLA USERS AQUI SE ALMACENA LOS DATOS INFORMATIVOS DEL USUARIO';

insert into web."Identifications"
(
	"Id",
	"UserId",
	"CatTypeDocument",
	"CatNacionality",
	"Ndocument",
	"CatGender",
	"CatCivilStatus",
	"BirthDate",
	"EntryDate",
	"DepartureDate",
	"Hired",
	"ImgUrl",
	"CurriculumUrl",
	"Mobile",
	"Phone",
	"Address",
	"UbcProvincia",
	"UbcCanton",
	"UbcParroquia",
	"Notes"
)
select 
    "IDTID",
	"USUID",
	"CATTIPODOCUMENTO",
	"CATNACIONALIDAD",
	"IDTNDOCUMENTO",
	"CATGENERO",
	"CATESTADOCIVIL",
	"IDTFECNACIMIENTO",
	"IDTFECINGRESO",
	"IDTFECSALIDA",
	"IDTCONTRATADO",
	"IDTIMGURL",
	"IDTHOJAVIDAURL",
	"IDTCELULAR",
	"IDTTELEFONO",
	"IDTDIRECCION",
	"UBCPROVINCIA",
	"UBCCANTON",
	"UBCPARROQUIA",
	"IDTNOTAS"
    FROM  public.identificaciones;