-- Table: web."Articles"

-- DROP TABLE IF EXISTS web."Articles";

CREATE TABLE IF NOT EXISTS web."Articles"
(
    "Id" serial NOT NULL, 
    "Provider" integer,
    "Sku" character varying(10) COLLATE pg_catalog."default",
    "Abrevia" character varying(150) COLLATE pg_catalog."default",
    "Name" character varying(150) COLLATE pg_catalog."default",
    "Cost" numeric(14,2) NOT NULL,
    "Brand" integer,
    "Notes" character varying(150) COLLATE pg_catalog."default",
    "Meta" json,
    "Discontinued" boolean,
    CONSTRAINT Articles_Id_pkey PRIMARY KEY ("Id")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

insert into web."Articles"
(
	"Id",
"Provider",
"Sku",
"Abrevia",
"Name",
"Cost",
"Brand",
"Notes",
"Meta",
"Discontinued"
)
select 
    "ARTID",
"ARTPROVEEDOR",
"ARTSKU",
"ARTABREVIA",
"ARTNOMBRE",
"ARTCOSTO",
"ARTMARCA",
"ARTNOTAS",
"ARTMETA",
"ARTDISCONTINUED"
    FROM  public.articulos;
	
