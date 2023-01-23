-- Table: web."Catalogs"

-- DROP TABLE IF EXISTS web."Catalogs";

CREATE TABLE IF NOT EXISTS web."Catalogs"
(
    "Id" serial NOT NULL,
    "Name" character varying(150) COLLATE pg_catalog."default" NOT NULL,
    "ParentId" integer,
    "Value" jsonb,
    CONSTRAINT Catalogs_Id_pkey PRIMARY KEY ("Id"),
    CONSTRAINT Catalogs_ParentId_fkey FOREIGN KEY ("ParentId")
        REFERENCES web."Catalogs" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT Catalogs_ParentId_check CHECK ("ParentId" >= 0)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

COMMENT ON TABLE web."Catalogs"
    IS 'TABLA MAESTRA CATALOGO DEL SISTEMA CONTIENE DE TODO CONFIGURACIONES';
-- Index: Catalogs_ParentId_idx

-- DROP INDEX IF EXISTS web."Catalogs"_ParentId_idx;

CREATE INDEX IF NOT EXISTS Catalogs_ParentId_idx
    ON web."Catalogs" USING btree
    ("ParentId" ASC NULLS LAST)
    TABLESPACE pg_default;

insert into web."Catalogs"(
	"Id",
"Name",
"ParentId",
"Value"
) 
select 
"id",
"nombre",
"idpadre",
"valor" from public.catalogos