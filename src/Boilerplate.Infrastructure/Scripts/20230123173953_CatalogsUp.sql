-- Table: web."Catalogs"

-- DROP TABLE IF EXISTS web."Catalogs";

CREATE TABLE IF NOT EXISTS web."Catalogs"
(
    "Id" serial NOT NULL,
    "Name" character varying(150) COLLATE pg_catalog."default" NOT NULL,
    "Parent" integer,
    "Value" jsonb,
    CONSTRAINT "Catalogs_Id_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT "Catalogs_Parent_fkey" FOREIGN KEY ("Parent")
        REFERENCES web."Catalogs" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "Catalogs_Parent_check" CHECK ("Parent" >= 0)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

COMMENT ON TABLE web."Catalogs"
    IS 'TABLA MAESTRA CATALOGO DEL SISTEMA CONTIENE DE TODO CONFIGURACIONES';
-- Index: "Catalogs_Parent_idx"

-- DROP INDEX IF EXISTS web."Catalogs_Parent_idx";

CREATE INDEX IF NOT EXISTS "Catalogs_Parent_idx"
    ON web."Catalogs" USING btree
    ("Parent" ASC NULLS LAST)
    TABLESPACE pg_default;

insert into web."Catalogs"(
	"Id",
    "Name",
    "Parent",
    "Value"
) 
select 
    "id",
    "nombre",
    "idpadre",
    "valor" 
from public.catalogos