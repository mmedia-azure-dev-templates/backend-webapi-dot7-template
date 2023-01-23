-- Table: web."GeographicLocation"

-- DROP TABLE IF EXISTS web."GeographicLocation";

CREATE TABLE IF NOT EXISTS web."GeographicLocation"
(
    "Id" serial NOT NULL,
    "Code" character varying(50) COLLATE pg_catalog."default" NOT NULL DEFAULT 0,
    "Name" character varying(150) COLLATE pg_catalog."default" NOT NULL DEFAULT 0,
    "ParentId" integer DEFAULT 0,
    "Parroquia" smallint NOT NULL DEFAULT 0,
    CONSTRAINT GeographicLocation_Id_pkey PRIMARY KEY ("Id"),
    CONSTRAINT GeographicLocation_ParentId_fkey FOREIGN KEY ("ParentId")
        REFERENCES web."GeographicLocation" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

COMMENT ON TABLE web."GeographicLocation"
    IS 'TABLA CON LA DISTRIBUCION GEOGRÁFICA DEL ECUADOR PROVINCIAS, CANTONES, PARROQUIAS';
-- Index: GeographicLocation_ParentId_idx

-- DROP INDEX IF EXISTS web.GeographicLocation_ParentId_idx;

CREATE INDEX IF NOT EXISTS GeographicLocation_ParentId_idx
    ON web."GeographicLocation" USING btree
    ("ParentId" ASC NULLS LAST)
    TABLESPACE pg_default;
	
insert into web."GeographicLocation"
(
	"Id",
	"Code",
	"Name",
	"ParentId",
	"Parroquia"
)
select 
    "id",
	"codigo",
	"nombre",
	"idpadre",
	"parroquia"
    FROM  public.ubicaciongeografica;