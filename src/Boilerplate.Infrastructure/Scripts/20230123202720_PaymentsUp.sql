-- Table: web."Payments"

-- DROP TABLE IF EXISTS web."Payments";

CREATE TABLE IF NOT EXISTS web."Payments"
(
    "Id" serial NOT NULL,
    "PaidDate" timestamp without time zone,
    "FinancialEntity" integer NOT NULL,
    "OrderNumber" integer NOT NULL,
    "Ammount" numeric(14,2) NOT NULL,
    "UsuId" integer NOT NULL,
    "Observation" character varying(200) COLLATE pg_catalog."default",
    "Transaction" character varying(50) COLLATE pg_catalog."default",
    CONSTRAINT "Payments_Id_pkey" PRIMARY KEY ("Id")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

COMMENT ON TABLE web."Payments"
    IS 'TABLA DONDE SE GUARDAN LOS DEPOSITOS';
-- Index: "Payments_OrderNumber_idx"

-- DROP INDEX IF EXISTS web."Payments_OrderNumber_idx"

CREATE INDEX IF NOT EXISTS "Payments_OrderNumber_idx"
    ON web."Payments" USING btree
    ("OrderNumber" ASC NULLS LAST)
    TABLESPACE pg_default;


insert into web."Payments"(
	"Id",
	"PaidDate", 
	"FinancialEntity", 
	"OrderNumber", 
	"Ammount", 
	"UsuId", 
	"Observation", 
	"Transaction"
)
select 
	"PAYID",
	"PAYDATE",
	"PAYENTIDADFINANCIERA",
	"PAYORDEN",
	"PAYAMMOUNT",
	"PAYUSUGENERA",
	"PAYOBSERVACION",
	"PAYTRANSACTION"
from public.payments;
