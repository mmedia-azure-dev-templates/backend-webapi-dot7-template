-- Table: web."Products"

-- DROP TABLE IF EXISTS web."Products";

CREATE TABLE IF NOT EXISTS web."Products"
(
    "Id" serial NOT NULL,
    "OrderId" integer,
    "Sku" text COLLATE pg_catalog."default",
    "Brand" text COLLATE pg_catalog."default",
    "Name" text COLLATE pg_catalog."default",
    "Descriptions" text COLLATE pg_catalog."default",
    "Weigth" numeric(14,1),
    "Quantity" integer,
    "Price" numeric(14,2),
    "Total" numeric(14,2),
    CONSTRAINT Products_Id_pkey PRIMARY KEY ("Id")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

COMMENT ON TABLE web."Products"
    IS 'TABLA DE LOS PRODUCTOS ASOCIADO A UNA ORDEN';
-- Index: Products_OrderId_idx

-- DROP INDEX IF EXISTS web.Products_OrderId_idx;

CREATE INDEX IF NOT EXISTS Products_OrderId_idx
    ON web."Products" USING btree
    ("OrderId" ASC NULLS LAST)
    TABLESPACE pg_default;


insert into web."Products" (
	"Id",
	"OrderId", 
	"Sku", 
	"Brand", 
	"Name", 
	"Descriptions", 
	"Weigth", 
	"Quantity", 
	"Price", 
	"Total"
)
select 
	"ID",
	"ORDERNUMERO",
	"SKU",
	"BRAND",
	"NAME",
	"DESCRIPTION",
	"WEIGHT",
	"QUANTITY",
	"PRICE",
	"TOTAL"
from public.orderproducts;