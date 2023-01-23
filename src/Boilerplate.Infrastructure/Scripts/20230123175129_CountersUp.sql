-- Table: web."Counters"

-- DROP TABLE IF EXISTS web."Counters";

CREATE TABLE IF NOT EXISTS web."Counters"
(
    "Id" serial NOT NULL,
    "Slug" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "CustomCounter" bigint NOT NULL,
    CONSTRAINT Counters_Id_pkey PRIMARY KEY ("Id")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

COMMENT ON TABLE web."Counters"
    IS 'TABLA DE CONTADORES DE ORDENES DEL SISTEMA';

insert into web."Counters"(
	"Id",
"Slug",
"CustomCounter"
) 
select 
"ID",
"SLUG",
"CUSTOMCOUNTER"
from public."contadores"