-- Table: web."Inscriptions"

-- DROP TABLE IF EXISTS web."Inscriptions";

CREATE TABLE IF NOT EXISTS web."Inscriptions"
(
    "Id" serial NOT NULL,
    "Agreement" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "InscriptionDate" timestamp with time zone NOT NULL,
    "Applicant" integer NOT NULL,
    "Information" jsonb,
    CONSTRAINT "Inscriptions_Id_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT "Inscriptions_Applicant_key" UNIQUE ("Applicant")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;


insert into web."Inscriptions" (
	"Id",
	"Agreement", 
	"InscriptionDate", 
	"Applicant", 
	"Information"
) 
select 
	"insid",
	"insconvenio",
	"insfecha",
	"inssolicitante",
	"insdatos" 
from public.inscriptions