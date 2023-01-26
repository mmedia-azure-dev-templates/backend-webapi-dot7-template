
drop schema if exists web CASCADE;

create schema web;



-- Table: web."Users"

-- DROP TABLE IF EXISTS web."Users";

CREATE TABLE IF NOT EXISTS web."Users"
(
    "Id" serial NOT NULL,
    "Name" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "SurName" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "UserName" character varying(50) COLLATE pg_catalog."default" NOT NULL,
	"Password" text COLLATE pg_catalog."default" NOT NULL,
    "Role" text COLLATE pg_catalog."default" NOT NULL,
    "RememberToken" character varying(100) COLLATE pg_catalog."default" DEFAULT 'NULL::character varying',
    "Email" character varying(60) COLLATE pg_catalog."default" NOT NULL,
    "IsActive" smallint NOT NULL,
    "LastLogin" timestamp without time zone,
    "LastLoginIp" character varying(50) COLLATE pg_catalog."default" DEFAULT 'NULL::character varying',
    "CreatedAt" timestamp(0) without time zone NOT NULL,
    "UpdatedAt" timestamp without time zone NOT NULL,
    "DeletedAt" timestamp without time zone,
    CONSTRAINT "Users_Id_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT "Users_Email_key" UNIQUE ("Email"),
    CONSTRAINT "Users_UserName_key" UNIQUE ("UserName")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

COMMENT ON TABLE web."Users"
    IS 'EN ESTA TABLA SE GUARDAN LOS USUARIOS DEL SISTEMA';
-- Index: Users_SurName_idx

-- DROP INDEX IF EXISTS web."Users_SurName_idx";

CREATE INDEX IF NOT EXISTS "Users_SurName_idx"
    ON web."Users" USING btree
    ("SurName" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: "Users_Name_idx"

-- DROP INDEX IF EXISTS web."Users_Name_idx";

CREATE INDEX IF NOT EXISTS "Users_Name_idx"
    ON web."Users" USING btree
    ("Name" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;


insert into web."Users"
(
	"Id",
	"Name",
	"SurName",
	"UserName",
	"Password",
	"Role",
	"RememberToken",
	"Email",
	"IsActive",
	"LastLogin",
	"LastLoginIp",
	"CreatedAt",
	"UpdatedAt",
	"DeletedAt"
)
select 
    "id",
	"nombres",
	"apellidos",
	"username",
	"password",
    'User',
	"remember_token",
	"email",
	"is_active",
	"last_login",
	"last_login_ip",
	"created_at",
	"updated_at",
	"deleted_at"
    FROM  public.users;

update web."Users" set "Role" = 'Admin' where "Id" = 1;

-- Table: web."Identifications"

-- DROP TABLE IF EXISTS web."Identifications";

CREATE TABLE IF NOT EXISTS web."Identifications"
(
    "Id" serial NOT NULL,
    "UserId" integer NOT NULL DEFAULT 0,
    "CatTypeDocument" integer NOT NULL DEFAULT 0,
    "CatNacionality" integer NOT NULL,
    "Ndocument" character varying(15) COLLATE pg_catalog."default" NOT NULL,
    "CatGender" integer,
    "CatCivilStatus" integer,
    "BirthDate" date,
    "EntryDate" date,
    "DepartureDate" date,
    "Hired" smallint NOT NULL DEFAULT 0,
    "ImgUrl" character varying(200) COLLATE pg_catalog."default",
    "CurriculumUrl" character varying(200) COLLATE pg_catalog."default",
    "Mobile" character varying(50) COLLATE pg_catalog."default",
    "Phone" character varying(50) COLLATE pg_catalog."default",
    "Address" character varying(200) COLLATE pg_catalog."default",
    "UbcProvincia" integer,
    "UbcCanton" integer,
    "UbcParroquia" integer,
    "Notes" character varying(50) COLLATE pg_catalog."default",
    CONSTRAINT "Identifications_Id_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT "Identifications_Ndocumento_key" UNIQUE ("Ndocument"),
    CONSTRAINT "Identifications_UserId_key" UNIQUE ("UserId"),
    CONSTRAINT "Identifications_UserId_Users" FOREIGN KEY ("UserId")
        REFERENCES web."Users" ("Id") MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;
COMMENT ON TABLE web."Identifications"
    IS 'TABLA HACE JOIN CON TABLA USERS AQUI SE ALMACENA LOS DATOS INFORMATIVOS DEL USUARIO';

insert into web."Identifications"
(
	"Id",
	"UserId",
	"CatTypeDocument",
	"CatNacionality",
	"Ndocument",
	"CatGender",
	"CatCivilStatus",
	"BirthDate",
	"EntryDate",
	"DepartureDate",
	"Hired",
	"ImgUrl",
	"CurriculumUrl",
	"Mobile",
	"Phone",
	"Address",
	"UbcProvincia",
	"UbcCanton",
	"UbcParroquia",
	"Notes"
)
select 
    "IDTID",
	"USUID",
	"CATTIPODOCUMENTO",
	"CATNACIONALIDAD",
	"IDTNDOCUMENTO",
	"CATGENERO",
	"CATESTADOCIVIL",
	"IDTFECNACIMIENTO",
	"IDTFECINGRESO",
	"IDTFECSALIDA",
	"IDTCONTRATADO",
	"IDTIMGURL",
	"IDTHOJAVIDAURL",
	"IDTCELULAR",
	"IDTTELEFONO",
	"IDTDIRECCION",
	"UBCPROVINCIA",
	"UBCCANTON",
	"UBCPARROQUIA",
	"IDTNOTAS"
    FROM  public.identificaciones;

-- Table: web."GeographicLocation"

-- DROP TABLE IF EXISTS web."GeographicLocation";

CREATE TABLE IF NOT EXISTS web."GeographicLocation"
(
    "Id" serial NOT NULL,
    "Code" character varying(50) COLLATE pg_catalog."default" NOT NULL DEFAULT 0,
    "Name" character varying(150) COLLATE pg_catalog."default" NOT NULL DEFAULT 0,
    "Parent" integer DEFAULT 0,
    "Parroquia" smallint NOT NULL DEFAULT 0,
    CONSTRAINT "GeographicLocation_Id_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT "GeographicLocation_ParentId_fkey" FOREIGN KEY ("Parent")
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

insert into web."GeographicLocation"
(
	"Id",
	"Code",
	"Name",
	"Parent",
	"Parroquia"
)
select 
    "id",
	"codigo",
	"nombre",
	"idpadre",
	"parroquia"
FROM  public.ubicaciongeografica;

-- Table: web."Contacts"

-- DROP TABLE IF EXISTS web."Contacts";

CREATE TABLE IF NOT EXISTS web."Contacts"
(
    "Id" serial not null,
    "Ndocument" character varying(15) COLLATE pg_catalog."default",
    "Name" character varying(50) COLLATE pg_catalog."default",
    "SurName" character varying(50) COLLATE pg_catalog."default",
    "Email" character varying(50) COLLATE pg_catalog."default",
    "Mobile" character varying(20) COLLATE pg_catalog."default",
    "Phone" character varying(20) COLLATE pg_catalog."default",
    "Address" character varying(400) COLLATE pg_catalog."default",
    "CatTypeDocument" integer,
    "CatNacionality" integer,
    "UbcProvincia" integer,
    "UbcCanton" integer,
    "UbcParroquia" integer,
    "Notes" character varying(50) COLLATE pg_catalog."default",
    "Supervisor" integer,
    "CatCivilStatus" character varying(25) COLLATE pg_catalog."default",
    "BirthDate" timestamp without time zone,
    CONSTRAINT "Contacts_Id_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT "Contacts_Ndocument_key" UNIQUE ("Ndocument")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

COMMENT ON TABLE web."Contacts"
    IS 'TABLA DONDE SE ALMACENAN LOS CLIENTES';
-- Index: IDX_CTOAPELLIDOS

-- DROP INDEX IF EXISTS web."Contacts_SurName_idx";

CREATE INDEX IF NOT EXISTS "Contacts_SurName_idx"
    ON web."Contacts" USING btree
    ("SurName" COLLATE pg_catalog."default" varchar_pattern_ops ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: IDX_CTONOMBRES

-- DROP INDEX IF EXISTS web."Contacts_Name_idx";

CREATE INDEX IF NOT EXISTS "Contacts_Name_idx"
    ON web."Contacts" USING btree
    ("Name" COLLATE pg_catalog."default" varchar_pattern_ops ASC NULLS LAST)
    TABLESPACE pg_default;

insert into web."Contacts"
(
	"Id",
	"Ndocument",
	"Name",
	"SurName",
	"Email",
	"Mobile",
	"Phone",
	"Address",
	"CatTypeDocument",
	"CatNacionality",
	"UbcProvincia",
	"UbcCanton",
	"UbcParroquia",
	"Notes",
	"Supervisor",
	"CatCivilStatus",
	"BirthDate"
)
select 
    "CTOID",
	"CTODOCUMENTO",
	"CTONOMBRES",
	"CTOAPELLIDOS",
	"CTOEMAIL",
	"CTOCELULAR",
	"CTOTELEFONO",
	"CTODIRECCION",
	"CTOTIPODOCUMENTO",
	"CTOTIPONACIONALIDAD",
	"CTOPROVINCIA",
	"CTOCANTON",
	"CTOPARROQUIA",
	"CTONOTAS",
	"CTOSUPERVISOR",
	"CTOESTADOCIVIL",
	"CTOFECHANACIMIENTO"
    FROM  public.contacts;
-- Table: web."Postulants"

-- DROP TABLE IF EXISTS web."Postulants";

CREATE TABLE IF NOT EXISTS web."Postulants"
(
    "Id" serial NOT NULL,
    "Contacted" boolean,
    "Name" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "SurName" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "UserName" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "Email" character varying(60) COLLATE pg_catalog."default" NOT NULL,
    "CatTypeDocument" integer NOT NULL,
    "CatNacionality" integer NOT NULL,
    "Ndocument" character varying(15) COLLATE pg_catalog."default" NOT NULL,
    "Gender" integer NOT NULL,
    "CatCivilStatus" integer NOT NULL,
    "BirthDate" date NOT NULL,
    "UbcProvincia" integer NOT NULL,
    "UbcCanton" integer NOT NULL,
    "UbcParroquia" integer NOT NULL,
    "Address" character varying(200) COLLATE pg_catalog."default",
    "Phone" character varying(12) COLLATE pg_catalog."default" NOT NULL,
    "Mobile" character varying(15) COLLATE pg_catalog."default" NOT NULL,
    "State" integer NOT NULL DEFAULT 118,
    "ImgUrl" character varying(200) COLLATE pg_catalog."default",
    "CurriculumUrl" character varying(200) COLLATE pg_catalog."default",
    "CreatedAt" timestamp without time zone,
    "UpdatedAt" timestamp without time zone,
    "DeletedAt" timestamp without time zone,
    CONSTRAINT "Postulants_Id_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT "Postulants_Email_key" UNIQUE ("Email"),
    CONSTRAINT "Postulants_Ndocument_key" UNIQUE ("Ndocument"),
    CONSTRAINT "Postulants_UserName_key" UNIQUE ("UserName")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

COMMENT ON TABLE web."Postulants"
    IS 'POSTULANTES AQUI SE GUARDAN LAS PERSONAS QUE SE REGISTRAN EN EL SISTEMA';

insert into web."Postulants"
(
	"Id",
	"Contacted",
	"Name",
	"SurName",
	"UserName",
	"Email",
	"CatTypeDocument",
	"CatNacionality",
	"Ndocument",
	"Gender",
	"CatCivilStatus",
	"BirthDate",
	"UbcProvincia",
	"UbcCanton",
	"UbcParroquia",
	"Address",
	"Phone",
	"Mobile",
	"State",
	"ImgUrl",
	"CurriculumUrl",
	"CreatedAt",
	"UpdatedAt",
	"DeletedAt"
)
select 
    "PTLID",
	"PTLCONTACTED",
	"PTLNOMBRES",
	"PTLAPELLIDOS",
	"PTLUSERNAME",
	"PTLEMAIL",
	"PTLTIPODOCUMENTO",
	"PTLNACIONALIDAD",
	"PTLNDOCUMENTO",
	"PTLGENERO",
	"PTLESTADOCIVIL",
	"PTLFECNACIMIENTO",
	"PTLPROVINCIA",
	"PTLCANTON",
	"PTLPARROQUIA",
	"PTLDIRECCION",
	"PTLTELEFONO",
	"PTLCELULAR",
	"PTLESTADO",
	"PTLIMGURL",
	"PTLHOJAVIDAURL",
	"created_at",
	"updated_at",
	"deleted_at"
    FROM  public.postulants;


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
    CONSTRAINT "Articles_Id_pkey" PRIMARY KEY ("Id")
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
	
-- Table: web."Catalogs"

-- DROP TABLE IF EXISTS web."Catalogs";

CREATE TABLE IF NOT EXISTS web."Catalogs"
(
    "Id" serial NOT NULL,
    "Name" character varying(150) COLLATE pg_catalog."default" NOT NULL,
    "CustomParent" integer,
    "CustomData" jsonb,
    CONSTRAINT "Catalogs_Id_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT "Catalogs_Parent_fkey" FOREIGN KEY ("CustomParent")
        REFERENCES web."Catalogs" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT "Catalogs_Parent_check" CHECK ("CustomParent" >= 0)
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
    ("CustomParent" ASC NULLS LAST)
    TABLESPACE pg_default;

insert into web."Catalogs"(
	"Id",
    "Name",
    "CustomParent",
    "CustomData"
) 
select 
    "id",
    "nombre",
    "idpadre",
    "valor" 
from public.catalogos;


-- Table: web."Counters"

-- DROP TABLE IF EXISTS web."Counters";

CREATE TABLE IF NOT EXISTS web."Counters"
(
    "Id" serial NOT NULL,
    "Slug" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "CustomCounter" bigint NOT NULL,
    CONSTRAINT "Counters_Id_pkey" PRIMARY KEY ("Id")
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
from public."contadores";

-- Table: web."InventoryDocs"

-- DROP TABLE IF EXISTS web."InventoryDocs";

CREATE TABLE IF NOT EXISTS web."InventoryDocs"
(
    "Id" serial NOT NULL,
    "Code" text COLLATE pg_catalog."default" NOT NULL,
    "Description" text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "InventoryDocs_Id_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT "InventoryDocs_Code_key" UNIQUE ("Code"),
    CONSTRAINT "InventoryDocs_Description_key" UNIQUE ("Description")
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
from public.documentos;


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
from public.inscriptions;


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
    CONSTRAINT "Products_Id_pkey" PRIMARY KEY ("Id")
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

COMMENT ON TABLE web."Products"
    IS 'TABLA DE LOS PRODUCTOS ASOCIADO A UNA ORDEN';
-- Index: Products_OrderId_idx

-- DROP INDEX IF EXISTS web.Products_OrderId_idx;

CREATE INDEX IF NOT EXISTS "Products_OrderId_idx"
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
    CONSTRAINT "Orders_Id_pkey" PRIMARY KEY ("Id"),
    CONSTRAINT "Orders_OrderNumber_key" UNIQUE ("OrderNumber"),
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
	"Id",
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
