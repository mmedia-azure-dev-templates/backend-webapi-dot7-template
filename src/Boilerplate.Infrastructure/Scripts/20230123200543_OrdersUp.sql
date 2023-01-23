-- Table: web."Orders"

-- DROP TABLE IF EXISTS web."Orders";

CREATE TABLE IF NOT EXISTS web."Orders"
(
    "Id" serial NOT NULL,
    "Enterprise" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "GeneratedDate1" date NOT NULL,
    "GeneratedDate2" date,
    "GeneratedHour1" time without time zone NOT NULL,
    "OrderNumber" integer NOT NULL,
    "Credit" numeric(14,2),
    "UserId" integer NOT NULL,
    "ContactId" integer,
    "Assigned" integer,
    "PersonType" character(1) COLLATE pg_catalog."default",
    "CashAdvance" numeric(14,2) NOT NULL DEFAULT '0.00',
    "SubTotal" numeric(14,2) NOT NULL DEFAULT '0.00',
    "Iva" numeric(14,2) NOT NULL DEFAULT '0.00',
    "Total" numeric(14,2) NOT NULL DEFAULT '0.00',
    "Balance" numeric(14,2) NOT NULL DEFAULT '0.00',
    "Agreegment" integer,
    "Term" integer,
    "State" integer,
    "Observations" character varying(150) COLLATE pg_catalog."default",
    "Notes" character varying(150) COLLATE pg_catalog."default",
    "ImgUrl" character varying(100) COLLATE pg_catalog."default",
    "Documentation" jsonb,
    "PaidDate" timestamp with time zone,
    "PaidUser" integer,
    "PaidUserType" character(1) COLLATE pg_catalog."default",
    "PaidState" boolean DEFAULT 'false',
    "Dispatch" character varying(25) COLLATE pg_catalog."default",
    "Extras" text COLLATE pg_catalog."default",
    CONSTRAINT Orders_Id_pkey PRIMARY KEY ("Id"),
    CONSTRAINT Orders_OrderNumber_key UNIQUE ("OrderNumber"),
    CONSTRAINT "Orders_ContactId_fk" FOREIGN KEY ("ContactId")
        REFERENCES web."Contacts" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "Orders_UserId_fk" FOREIGN KEY ("UserId")
        REFERENCES web."Users" ("Id") MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;


insert into web."Orders" (
	"Enterprise",
	"GeneratedDate1",
	"GeneratedDate2",
	"GeneratedHour1",
	"OrderNumber",
	"Credit",
	"UserId",
	"ContactId",
	"Assigned",
	"PersonType",
	"CashAdvance",
	"SubTotal",
	"Iva",
	"Total",
	"Balance",
	"Agreegment",
	"Term",
	"State",
	"Observations",
	"Notes",
	"ImgUrl",
	"Documentation",
	"PaidDate",
	"PaidUser",
	"PaidUserType",
	"PaidState",
	"Dispatch",
	"Extras"
)
select 
	"ORDID",
	"ORDEMPRESA",
	"ORDREGISTROFECHA1",
	"ORDREGISTROFECHA2",
	"ORDREGISTROHORA1",
	"ORDNUMERO",
	"ORDCREDITO",
	"USUID",
	"CTOID",
	"ORDASSIGNED",
	"PERSONTYPE",
	"ORDABONO",
	"ORDSUBTOTAL",
	"ORDIVA",
	"ORDTOTAL",
	"ORDSALDO",
	"ORDCONVENIO",
	"ORDPLAZO",
	"ORDESTADO",
	"ORDNOTASOBS",
	"ORDNOTAS",
	"ORDIMGURL",
	"ORDDOCUMENTACION",
	"ORDDATESALEPAYMENT",
	"ORDSALEUSERPAYMENT",
	"ORDSALEUSERTYPEPAYMENT",
	"ORDSALESTATEPAYMENT",
	"ORDDESPACHO",
	"ORDEXTRAS"
from public.orders;