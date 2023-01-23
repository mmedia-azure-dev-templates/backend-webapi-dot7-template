-- Table: web."InventoryDocs"

-- DROP TABLE IF EXISTS web."InventoryDocs";

CREATE TABLE IF NOT EXISTS web."InventoryDocs"
(
    "Id" serial NOT NULL,
    "Code" text COLLATE pg_catalog."default" NOT NULL,
    "Description" text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT InventoryDocs_Id_pkey PRIMARY KEY ("Id"),
    CONSTRAINT InventoryDocs_Code_key UNIQUE ("Code"),
    CONSTRAINT InventoryDocs_Description_key UNIQUE ("Description")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

COMMENT ON TABLE web."InventoryDocs"
    IS 'TABLA DONDE SE ALMACENA EL INVENTARIO DE LOS DOCUMENTOS REQUERIDOS EN LAS ORDENES';


insert into web."InventoryDocs"(
	"Id",
    "Code",
    "Description"
)
select 
    "DOCUID",
    "DOCUCODIGO",
    "DOCUDESCRIPCION"
from public.documentos