--
-- PostgreSQL database dump
--

-- Dumped from database version 10.23
-- Dumped by pg_dump version 15.1

-- Started on 2023-01-25 05:24:18 -05

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 7 (class 2615 OID 311537)
-- Name: web; Type: SCHEMA; Schema: -; Owner: -
--

-- *not* creating schema, since initdb creates it


--
-- TOC entry 3969 (class 0 OID 0)
-- Dependencies: 7
-- Name: SCHEMA web; Type: COMMENT; Schema: -; Owner: -
--

COMMENT ON SCHEMA web IS '';


--
-- TOC entry 196 (class 1259 OID 311539)
-- Name: AbpAuditLogActions; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpAuditLogActions" (
    "Id" uuid NOT NULL,
    "TenantId" uuid,
    "AuditLogId" uuid NOT NULL,
    "ServiceName" character varying(256),
    "MethodName" character varying(128),
    "Parameters" character varying(2000),
    "ExecutionTime" timestamp without time zone NOT NULL,
    "ExecutionDuration" integer NOT NULL,
    "ExtraProperties" text
);


--
-- TOC entry 197 (class 1259 OID 311545)
-- Name: AbpAuditLogs; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpAuditLogs" (
    "Id" uuid NOT NULL,
    "ApplicationName" character varying(96),
    "UserId" uuid,
    "UserName" character varying(256),
    "TenantId" uuid,
    "TenantName" character varying(64),
    "ImpersonatorUserId" uuid,
    "ImpersonatorUserName" character varying(256),
    "ImpersonatorTenantId" uuid,
    "ImpersonatorTenantName" character varying(64),
    "ExecutionTime" timestamp without time zone NOT NULL,
    "ExecutionDuration" integer NOT NULL,
    "ClientIpAddress" character varying(64),
    "ClientName" character varying(128),
    "ClientId" character varying(64),
    "CorrelationId" character varying(64),
    "BrowserInfo" character varying(512),
    "HttpMethod" character varying(16),
    "Url" character varying(256),
    "Exceptions" text,
    "Comments" character varying(256),
    "HttpStatusCode" integer,
    "ExtraProperties" text,
    "ConcurrencyStamp" character varying(40)
);


--
-- TOC entry 198 (class 1259 OID 311551)
-- Name: AbpClaimTypes; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpClaimTypes" (
    "Id" uuid NOT NULL,
    "Name" character varying(256) NOT NULL,
    "Required" boolean NOT NULL,
    "IsStatic" boolean NOT NULL,
    "Regex" character varying(512),
    "RegexDescription" character varying(128),
    "Description" character varying(256),
    "ValueType" integer NOT NULL,
    "ExtraProperties" text,
    "ConcurrencyStamp" character varying(40)
);


--
-- TOC entry 199 (class 1259 OID 311557)
-- Name: AbpEntityChanges; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpEntityChanges" (
    "Id" uuid NOT NULL,
    "AuditLogId" uuid NOT NULL,
    "TenantId" uuid,
    "ChangeTime" timestamp without time zone NOT NULL,
    "ChangeType" smallint NOT NULL,
    "EntityTenantId" uuid,
    "EntityId" character varying(128) NOT NULL,
    "EntityTypeFullName" character varying(128) NOT NULL,
    "ExtraProperties" text
);


--
-- TOC entry 200 (class 1259 OID 311563)
-- Name: AbpEntityPropertyChanges; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpEntityPropertyChanges" (
    "Id" uuid NOT NULL,
    "TenantId" uuid,
    "EntityChangeId" uuid NOT NULL,
    "NewValue" character varying(512),
    "OriginalValue" character varying(512),
    "PropertyName" character varying(128) NOT NULL,
    "PropertyTypeFullName" character varying(64) NOT NULL
);


--
-- TOC entry 201 (class 1259 OID 311569)
-- Name: AbpFeatureGroups; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpFeatureGroups" (
    "Id" uuid NOT NULL,
    "Name" character varying(128) NOT NULL,
    "DisplayName" character varying(256) NOT NULL,
    "ExtraProperties" text
);


--
-- TOC entry 202 (class 1259 OID 311575)
-- Name: AbpFeatureValues; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpFeatureValues" (
    "Id" uuid NOT NULL,
    "Name" character varying(128) NOT NULL,
    "Value" character varying(128) NOT NULL,
    "ProviderName" character varying(64),
    "ProviderKey" character varying(64)
);


--
-- TOC entry 203 (class 1259 OID 311578)
-- Name: AbpFeatures; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpFeatures" (
    "Id" uuid NOT NULL,
    "GroupName" character varying(128) NOT NULL,
    "Name" character varying(128) NOT NULL,
    "ParentName" character varying(128),
    "DisplayName" character varying(256) NOT NULL,
    "Description" character varying(256),
    "DefaultValue" character varying(256),
    "IsVisibleToClients" boolean NOT NULL,
    "IsAvailableToHost" boolean NOT NULL,
    "AllowedProviders" character varying(256),
    "ValueType" character varying(2048),
    "ExtraProperties" text
);


--
-- TOC entry 204 (class 1259 OID 311584)
-- Name: AbpLinkUsers; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpLinkUsers" (
    "Id" uuid NOT NULL,
    "SourceUserId" uuid NOT NULL,
    "SourceTenantId" uuid,
    "TargetUserId" uuid NOT NULL,
    "TargetTenantId" uuid
);


--
-- TOC entry 205 (class 1259 OID 311587)
-- Name: AbpOrganizationUnitRoles; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpOrganizationUnitRoles" (
    "RoleId" uuid NOT NULL,
    "OrganizationUnitId" uuid NOT NULL,
    "TenantId" uuid,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid
);


--
-- TOC entry 206 (class 1259 OID 311590)
-- Name: AbpOrganizationUnits; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpOrganizationUnits" (
    "Id" uuid NOT NULL,
    "TenantId" uuid,
    "ParentId" uuid,
    "Code" character varying(95) NOT NULL,
    "DisplayName" character varying(128) NOT NULL,
    "ExtraProperties" text,
    "ConcurrencyStamp" character varying(40),
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "DeleterId" uuid,
    "DeletionTime" timestamp without time zone
);


--
-- TOC entry 207 (class 1259 OID 311597)
-- Name: AbpPermissionGrants; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpPermissionGrants" (
    "Id" uuid NOT NULL,
    "TenantId" uuid,
    "Name" character varying(128) NOT NULL,
    "ProviderName" character varying(64) NOT NULL,
    "ProviderKey" character varying(64) NOT NULL
);


--
-- TOC entry 208 (class 1259 OID 311600)
-- Name: AbpPermissionGroups; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpPermissionGroups" (
    "Id" uuid NOT NULL,
    "Name" character varying(128) NOT NULL,
    "DisplayName" character varying(256) NOT NULL,
    "ExtraProperties" text
);


--
-- TOC entry 209 (class 1259 OID 311606)
-- Name: AbpPermissions; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpPermissions" (
    "Id" uuid NOT NULL,
    "GroupName" character varying(128) NOT NULL,
    "Name" character varying(128) NOT NULL,
    "ParentName" character varying(128),
    "DisplayName" character varying(256) NOT NULL,
    "IsEnabled" boolean NOT NULL,
    "MultiTenancySide" smallint NOT NULL,
    "Providers" character varying(128),
    "StateCheckers" character varying(256),
    "ExtraProperties" text
);


--
-- TOC entry 210 (class 1259 OID 311612)
-- Name: AbpRoleClaims; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpRoleClaims" (
    "Id" uuid NOT NULL,
    "RoleId" uuid NOT NULL,
    "TenantId" uuid,
    "ClaimType" character varying(256) NOT NULL,
    "ClaimValue" character varying(1024)
);


--
-- TOC entry 211 (class 1259 OID 311618)
-- Name: AbpRoles; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpRoles" (
    "Id" uuid NOT NULL,
    "TenantId" uuid,
    "Name" character varying(256) NOT NULL,
    "NormalizedName" character varying(256) NOT NULL,
    "IsDefault" boolean NOT NULL,
    "IsStatic" boolean NOT NULL,
    "Isweb" boolean NOT NULL,
    "ExtraProperties" text,
    "ConcurrencyStamp" character varying(40)
);


--
-- TOC entry 212 (class 1259 OID 311624)
-- Name: AbpSecurityLogs; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpSecurityLogs" (
    "Id" uuid NOT NULL,
    "TenantId" uuid,
    "ApplicationName" character varying(96),
    "Identity" character varying(96),
    "Action" character varying(96),
    "UserId" uuid,
    "UserName" character varying(256),
    "TenantName" character varying(64),
    "ClientId" character varying(64),
    "CorrelationId" character varying(64),
    "ClientIpAddress" character varying(64),
    "BrowserInfo" character varying(512),
    "CreationTime" timestamp without time zone NOT NULL,
    "ExtraProperties" text,
    "ConcurrencyStamp" character varying(40)
);


--
-- TOC entry 213 (class 1259 OID 311630)
-- Name: AbpSettings; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpSettings" (
    "Id" uuid NOT NULL,
    "Name" character varying(128) NOT NULL,
    "Value" character varying(2048) NOT NULL,
    "ProviderName" character varying(64),
    "ProviderKey" character varying(64)
);


--
-- TOC entry 214 (class 1259 OID 311636)
-- Name: AbpTenantConnectionStrings; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpTenantConnectionStrings" (
    "TenantId" uuid NOT NULL,
    "Name" character varying(64) NOT NULL,
    "Value" character varying(1024) NOT NULL
);


--
-- TOC entry 215 (class 1259 OID 311642)
-- Name: AbpTenants; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpTenants" (
    "Id" uuid NOT NULL,
    "Name" character varying(64) NOT NULL,
    "ExtraProperties" text,
    "ConcurrencyStamp" character varying(40),
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "DeleterId" uuid,
    "DeletionTime" timestamp without time zone
);


--
-- TOC entry 216 (class 1259 OID 311649)
-- Name: AbpUserClaims; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpUserClaims" (
    "Id" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "TenantId" uuid,
    "ClaimType" character varying(256) NOT NULL,
    "ClaimValue" character varying(1024)
);


--
-- TOC entry 217 (class 1259 OID 311655)
-- Name: AbpUserLogins; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpUserLogins" (
    "UserId" uuid NOT NULL,
    "LoginProvider" character varying(64) NOT NULL,
    "TenantId" uuid,
    "ProviderKey" character varying(196) NOT NULL,
    "ProviderDisplayName" character varying(128)
);


--
-- TOC entry 218 (class 1259 OID 311658)
-- Name: AbpUserOrganizationUnits; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpUserOrganizationUnits" (
    "UserId" uuid NOT NULL,
    "OrganizationUnitId" uuid NOT NULL,
    "TenantId" uuid,
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid
);


--
-- TOC entry 219 (class 1259 OID 311661)
-- Name: AbpUserRoles; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpUserRoles" (
    "UserId" uuid NOT NULL,
    "RoleId" uuid NOT NULL,
    "TenantId" uuid
);


--
-- TOC entry 220 (class 1259 OID 311664)
-- Name: AbpUserTokens; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpUserTokens" (
    "UserId" uuid NOT NULL,
    "LoginProvider" character varying(64) NOT NULL,
    "Name" character varying(128) NOT NULL,
    "TenantId" uuid,
    "Value" text
);


--
-- TOC entry 221 (class 1259 OID 311670)
-- Name: AbpUsers; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpUsers" (
    "Id" uuid NOT NULL,
    "TenantId" uuid,
    "UserName" character varying(256) NOT NULL,
    "NormalizedUserName" character varying(256) NOT NULL,
    "Name" character varying(64),
    "Surname" character varying(64),
    "Email" character varying(256) NOT NULL,
    "NormalizedEmail" character varying(256) NOT NULL,
    "EmailConfirmed" boolean DEFAULT false NOT NULL,
    "PasswordHash" character varying(256),
    "SecurityStamp" character varying(256) NOT NULL,
    "IsExternal" boolean DEFAULT false NOT NULL,
    "PhoneNumber" character varying(16),
    "PhoneNumberConfirmed" boolean DEFAULT false NOT NULL,
    "IsActive" boolean NOT NULL,
    "TwoFactorEnabled" boolean DEFAULT false NOT NULL,
    "LockoutEnd" timestamp with time zone,
    "LockoutEnabled" boolean DEFAULT false NOT NULL,
    "AccessFailedCount" integer DEFAULT 0 NOT NULL,
    "ExtraProperties" text,
    "ConcurrencyStamp" character varying(40),
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "DeleterId" uuid,
    "DeletionTime" timestamp without time zone
);


--
-- TOC entry 222 (class 1259 OID 311683)
-- Name: OpenIddictApplications; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."OpenIddictApplications" (
    "Id" uuid NOT NULL,
    "ClientId" character varying(100),
    "ClientSecret" text,
    "ConsentType" character varying(50),
    "DisplayName" text,
    "DisplayNames" text,
    "Permissions" text,
    "PostLogoutRedirectUris" text,
    "Properties" text,
    "RedirectUris" text,
    "Requirements" text,
    "Type" character varying(50),
    "ClientUri" text,
    "LogoUri" text,
    "ExtraProperties" text,
    "ConcurrencyStamp" character varying(40),
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "DeleterId" uuid,
    "DeletionTime" timestamp without time zone
);


--
-- TOC entry 223 (class 1259 OID 311690)
-- Name: OpenIddictAuthorizations; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."OpenIddictAuthorizations" (
    "Id" uuid NOT NULL,
    "ApplicationId" uuid,
    "CreationDate" timestamp without time zone,
    "Properties" text,
    "Scopes" text,
    "Status" character varying(50),
    "Subject" character varying(400),
    "Type" character varying(50),
    "ExtraProperties" text,
    "ConcurrencyStamp" character varying(40),
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "DeleterId" uuid,
    "DeletionTime" timestamp without time zone
);


--
-- TOC entry 224 (class 1259 OID 311697)
-- Name: OpenIddictScopes; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."OpenIddictScopes" (
    "Id" uuid NOT NULL,
    "Description" text,
    "Descriptions" text,
    "DisplayName" text,
    "DisplayNames" text,
    "Name" character varying(200),
    "Properties" text,
    "Resources" text,
    "ExtraProperties" text,
    "ConcurrencyStamp" character varying(40),
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "DeleterId" uuid,
    "DeletionTime" timestamp without time zone
);


--
-- TOC entry 225 (class 1259 OID 311704)
-- Name: OpenIddictTokens; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."OpenIddictTokens" (
    "Id" uuid NOT NULL,
    "ApplicationId" uuid,
    "AuthorizationId" uuid,
    "CreationDate" timestamp without time zone,
    "ExpirationDate" timestamp without time zone,
    "Payload" text,
    "Properties" text,
    "RedemptionDate" timestamp without time zone,
    "ReferenceId" character varying(100),
    "Status" character varying(50),
    "Subject" character varying(400),
    "Type" character varying(50),
    "ExtraProperties" text,
    "ConcurrencyStamp" character varying(40),
    "CreationTime" timestamp without time zone NOT NULL,
    "CreatorId" uuid,
    "LastModificationTime" timestamp without time zone,
    "LastModifierId" uuid,
    "IsDeleted" boolean DEFAULT false NOT NULL,
    "DeleterId" uuid,
    "DeletionTime" timestamp without time zone
);


--
-- TOC entry 226 (class 1259 OID 311711)
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


--
-- TOC entry 3933 (class 0 OID 311539)
-- Dependencies: 196
-- Data for Name: AbpAuditLogActions; Type: TABLE DATA; Schema: web; Owner: -
--

INSERT INTO web."AbpAuditLogActions" VALUES ('3a08f8ee-ed96-2bf7-9b43-dbf737d2fabf', NULL, '3a08f8ee-ed94-e797-57a5-d59477a1989d', 'Volo.Abp.Account.Web.Areas.Account.Controllers.AccountController', 'Login', '{"login":{"userNameOrEmailAddress":"admin","rememberMe":true}}', '2023-01-24 21:29:13.443929', 3798, '{}');
INSERT INTO web."AbpAuditLogActions" VALUES ('3a08fa8f-81ba-c0be-fa04-7c51a23d7451', NULL, '3a08fa8f-81b9-1a73-8088-5ab5c06364cf', 'Volo.Abp.Account.Web.Pages.Account.LoginModel', 'OnPostAsync', '{"action":"Login"}', '2023-01-25 05:04:15.796049', 2423, '{}');
INSERT INTO web."AbpAuditLogActions" VALUES ('3a08fa8f-965b-9e43-5986-4efb876fa9c0', NULL, '3a08fa8f-965b-f5ce-62c9-787f94607e48', 'Volo.Abp.OpenIddict.Controllers.TokenController', 'HandleAsync', '{}', '2023-01-25 05:04:23.275515', 41, '{}');


--
-- TOC entry 3934 (class 0 OID 311545)
-- Dependencies: 197
-- Data for Name: AbpAuditLogs; Type: TABLE DATA; Schema: web; Owner: -
--

INSERT INTO web."AbpAuditLogs" VALUES ('3a08f8ee-ed94-e797-57a5-d59477a1989d', 'Jiban.PlatForm', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2023-01-24 21:29:11.91545', 5388, '::1', NULL, NULL, 'da94dcf19213473ca87eff552e0b9ba0', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/109.0.0.0 Safari/537.36 Edg/109.0.1518.61', 'POST', '/api/account/login', NULL, '', 200, '{}', '09d792a53eaa401babd311867122d945');
INSERT INTO web."AbpAuditLogs" VALUES ('3a08fa8f-81b9-1a73-8088-5ab5c06364cf', 'Jiban.PlatForm.HttpApi.Host', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2023-01-25 05:04:15.758387', 2467, '127.0.0.1', NULL, NULL, 'f601493389e54688b546e18cd65b6073', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/109.0.0.0 Safari/537.36 Edg/109.0.1518.61', 'POST', '/Account/Login', NULL, '', 302, '{}', 'a0ef66661a164f22b74096e4528d5bbe');
INSERT INTO web."AbpAuditLogs" VALUES ('3a08fa8f-965b-f5ce-62c9-787f94607e48', 'Jiban.PlatForm.HttpApi.Host', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '2023-01-25 05:04:23.265626', 247, '127.0.0.1', NULL, NULL, '30aebb7e6289420a9da96250d0741337', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/109.0.0.0 Safari/537.36 Edg/109.0.1518.61', 'POST', '/connect/token', NULL, '', 200, '{}', 'a892452d050d405ca172b6a5b9308c90');


--
-- TOC entry 3935 (class 0 OID 311551)
-- Dependencies: 198
-- Data for Name: AbpClaimTypes; Type: TABLE DATA; Schema: web; Owner: -
--



--
-- TOC entry 3936 (class 0 OID 311557)
-- Dependencies: 199
-- Data for Name: AbpEntityChanges; Type: TABLE DATA; Schema: web; Owner: -
--



--
-- TOC entry 3937 (class 0 OID 311563)
-- Dependencies: 200
-- Data for Name: AbpEntityPropertyChanges; Type: TABLE DATA; Schema: web; Owner: -
--



--
-- TOC entry 3938 (class 0 OID 311569)
-- Dependencies: 201
-- Data for Name: AbpFeatureGroups; Type: TABLE DATA; Schema: web; Owner: -
--

INSERT INTO web."AbpFeatureGroups" VALUES ('3a08f8df-116d-948b-972c-f1cdcaa1a61f', 'SettingManagement', 'L:AbpSettingManagement,Feature:SettingManagementGroup', '{}');


--
-- TOC entry 3939 (class 0 OID 311575)
-- Dependencies: 202
-- Data for Name: AbpFeatureValues; Type: TABLE DATA; Schema: web; Owner: -
--



--
-- TOC entry 3940 (class 0 OID 311578)
-- Dependencies: 203
-- Data for Name: AbpFeatures; Type: TABLE DATA; Schema: web; Owner: -
--

INSERT INTO web."AbpFeatures" VALUES ('3a08f8df-1175-6882-daf9-5cd0e137c168', 'SettingManagement', 'SettingManagement.Enable', NULL, 'L:AbpSettingManagement,Feature:SettingManagementEnable', 'L:AbpSettingManagement,Feature:SettingManagementEnableDescription', 'true', true, false, NULL, '{"name":"ToggleStringValueType","properties":{},"validator":{"name":"BOOLEAN","properties":{}}}', '{}');
INSERT INTO web."AbpFeatures" VALUES ('3a08f8df-11d8-0bc5-4d4d-8173c1dd7842', 'SettingManagement', 'SettingManagement.AllowChangingEmailSettings', 'SettingManagement.Enable', 'L:AbpSettingManagement,Feature:AllowChangingEmailSettings', NULL, 'false', true, false, NULL, '{"name":"ToggleStringValueType","properties":{},"validator":{"name":"BOOLEAN","properties":{}}}', '{}');


--
-- TOC entry 3941 (class 0 OID 311584)
-- Dependencies: 204
-- Data for Name: AbpLinkUsers; Type: TABLE DATA; Schema: web; Owner: -
--



--
-- TOC entry 3942 (class 0 OID 311587)
-- Dependencies: 205
-- Data for Name: AbpOrganizationUnitRoles; Type: TABLE DATA; Schema: web; Owner: -
--



--
-- TOC entry 3943 (class 0 OID 311590)
-- Dependencies: 206
-- Data for Name: AbpOrganizationUnits; Type: TABLE DATA; Schema: web; Owner: -
--



--
-- TOC entry 3944 (class 0 OID 311597)
-- Dependencies: 207
-- Data for Name: AbpPermissionGrants; Type: TABLE DATA; Schema: web; Owner: -
--

INSERT INTO web."AbpPermissionGrants" VALUES ('3a08f8dd-9927-e9b3-74c7-d7aa99047c6d', NULL, 'AbpIdentity.Roles', 'R', 'admin');
INSERT INTO web."AbpPermissionGrants" VALUES ('3a08f8dd-9939-9f7c-1613-4f67baeecb1a', NULL, 'AbpIdentity.Roles.Create', 'R', 'admin');
INSERT INTO web."AbpPermissionGrants" VALUES ('3a08f8dd-993c-0844-2fb3-5915c5486b9f', NULL, 'AbpIdentity.Roles.ManagePermissions', 'R', 'admin');
INSERT INTO web."AbpPermissionGrants" VALUES ('3a08f8dd-993c-2a59-4b84-29eb513db874', NULL, 'AbpIdentity.Roles.Update', 'R', 'admin');
INSERT INTO web."AbpPermissionGrants" VALUES ('3a08f8dd-993c-dc62-490f-7a427e200968', NULL, 'AbpIdentity.Roles.Delete', 'R', 'admin');
INSERT INTO web."AbpPermissionGrants" VALUES ('3a08f8dd-993d-4cbd-c18a-6ab24c8c6ca4', NULL, 'AbpIdentity.Users', 'R', 'admin');
INSERT INTO web."AbpPermissionGrants" VALUES ('3a08f8dd-993d-4fd4-b9be-ad8dd4bd35aa', NULL, 'AbpIdentity.Users.Update', 'R', 'admin');
INSERT INTO web."AbpPermissionGrants" VALUES ('3a08f8dd-993d-8935-7f56-975dc37f2894', NULL, 'AbpIdentity.Users.Delete', 'R', 'admin');
INSERT INTO web."AbpPermissionGrants" VALUES ('3a08f8dd-993d-e405-342e-b5514bf63014', NULL, 'AbpIdentity.Users.Create', 'R', 'admin');
INSERT INTO web."AbpPermissionGrants" VALUES ('3a08f8dd-993e-0907-744c-c40b896b3880', NULL, 'AbpTenantManagement.Tenants.Update', 'R', 'admin');
INSERT INTO web."AbpPermissionGrants" VALUES ('3a08f8dd-993e-8c71-d066-8cbdf89faa0f', NULL, 'AbpTenantManagement.Tenants', 'R', 'admin');
INSERT INTO web."AbpPermissionGrants" VALUES ('3a08f8dd-993e-acc4-cf5a-25b9689b6211', NULL, 'AbpIdentity.Users.ManagePermissions', 'R', 'admin');
INSERT INTO web."AbpPermissionGrants" VALUES ('3a08f8dd-993e-cc0f-51e3-65a2c26daaa3', NULL, 'AbpTenantManagement.Tenants.Delete', 'R', 'admin');
INSERT INTO web."AbpPermissionGrants" VALUES ('3a08f8dd-993e-f0d8-c484-c6711d983555', NULL, 'AbpTenantManagement.Tenants.Create', 'R', 'admin');
INSERT INTO web."AbpPermissionGrants" VALUES ('3a08f8dd-993f-39ad-1e91-56414b31d487', NULL, 'AbpTenantManagement.Tenants.ManageConnectionStrings', 'R', 'admin');
INSERT INTO web."AbpPermissionGrants" VALUES ('3a08f8dd-993f-60ad-d624-a66c07f6c2a7', NULL, 'FeatureManagement.ManageHostFeatures', 'R', 'admin');
INSERT INTO web."AbpPermissionGrants" VALUES ('3a08f8dd-993f-9da4-ad28-0223d2ce2aa2', NULL, 'AbpTenantManagement.Tenants.ManageFeatures', 'R', 'admin');
INSERT INTO web."AbpPermissionGrants" VALUES ('3a08f8dd-993f-f866-7915-4c430c810da2', NULL, 'SettingManagement.Emailing', 'R', 'admin');
INSERT INTO web."AbpPermissionGrants" VALUES ('3a08f8dd-9940-352b-428d-35e6b9b749fc', NULL, 'SettingManagement.Emailing.Test', 'R', 'admin');


--
-- TOC entry 3945 (class 0 OID 311600)
-- Dependencies: 208
-- Data for Name: AbpPermissionGroups; Type: TABLE DATA; Schema: web; Owner: -
--

INSERT INTO web."AbpPermissionGroups" VALUES ('3a08f8df-0759-52ee-c36c-c53b6154d3f4', 'AbpIdentity', 'L:AbpIdentity,Permission:IdentityManagement', '{}');
INSERT INTO web."AbpPermissionGroups" VALUES ('3a08f8df-08f0-0b14-91b4-750a38a28719', 'FeatureManagement', 'L:AbpFeatureManagement,Permission:FeatureManagement', '{}');
INSERT INTO web."AbpPermissionGroups" VALUES ('3a08f8df-08f0-d1a4-66be-febc010c1516', 'SettingManagement', 'L:AbpSettingManagement,Permission:SettingManagement', '{}');
INSERT INTO web."AbpPermissionGroups" VALUES ('3a08f8df-08f0-ffdd-6ee3-5df43be56d90', 'AbpTenantManagement', 'L:AbpTenantManagement,Permission:TenantManagement', '{}');
INSERT INTO web."AbpPermissionGroups" VALUES ('3a08fa88-65b3-bf1f-4db1-ee4a57cde23a', 'PlatForm', 'F:PlatForm', '{}');


--
-- TOC entry 3946 (class 0 OID 311606)
-- Dependencies: 209
-- Data for Name: AbpPermissions; Type: TABLE DATA; Schema: web; Owner: -
--

INSERT INTO web."AbpPermissions" VALUES ('3a08f8df-08ec-0a26-e922-ba7e967eef44', 'AbpIdentity', 'AbpIdentity.Roles', NULL, 'L:AbpIdentity,Permission:RoleManagement', true, 3, NULL, NULL, '{}');
INSERT INTO web."AbpPermissions" VALUES ('3a08f8df-08f0-0d82-5ca4-47506c490f0c', 'AbpTenantManagement', 'AbpTenantManagement.Tenants.Delete', 'AbpTenantManagement.Tenants', 'L:AbpTenantManagement,Permission:Delete', true, 2, NULL, NULL, '{}');
INSERT INTO web."AbpPermissions" VALUES ('3a08f8df-08f0-11b1-b18b-efbf3b935a97', 'AbpIdentity', 'AbpIdentity.Users.Create', 'AbpIdentity.Users', 'L:AbpIdentity,Permission:Create', true, 3, NULL, NULL, '{}');
INSERT INTO web."AbpPermissions" VALUES ('3a08f8df-08f0-19c3-3613-3321c8037eee', 'AbpIdentity', 'AbpIdentity.Roles.Update', 'AbpIdentity.Roles', 'L:AbpIdentity,Permission:Edit', true, 3, NULL, NULL, '{}');
INSERT INTO web."AbpPermissions" VALUES ('3a08f8df-08f0-20c9-7465-cd37b27bd78a', 'AbpTenantManagement', 'AbpTenantManagement.Tenants.ManageConnectionStrings', 'AbpTenantManagement.Tenants', 'L:AbpTenantManagement,Permission:ManageConnectionStrings', true, 2, NULL, NULL, '{}');
INSERT INTO web."AbpPermissions" VALUES ('3a08f8df-08f0-3357-cb0f-f8607fb71d59', 'SettingManagement', 'SettingManagement.Emailing', NULL, 'L:AbpSettingManagement,Permission:Emailing', true, 3, NULL, NULL, '{}');
INSERT INTO web."AbpPermissions" VALUES ('3a08f8df-08f0-350b-d06d-7049a08d94af', 'AbpTenantManagement', 'AbpTenantManagement.Tenants.Create', 'AbpTenantManagement.Tenants', 'L:AbpTenantManagement,Permission:Create', true, 2, NULL, NULL, '{}');
INSERT INTO web."AbpPermissions" VALUES ('3a08f8df-08f0-3d39-6702-970baef2332f', 'AbpIdentity', 'AbpIdentity.Users.Delete', 'AbpIdentity.Users', 'L:AbpIdentity,Permission:Delete', true, 3, NULL, NULL, '{}');
INSERT INTO web."AbpPermissions" VALUES ('3a08f8df-08f0-3de9-10ff-122f79615cde', 'AbpIdentity', 'AbpIdentity.Users', NULL, 'L:AbpIdentity,Permission:UserManagement', true, 3, NULL, NULL, '{}');
INSERT INTO web."AbpPermissions" VALUES ('3a08f8df-08f0-4e0d-aa90-f731766fd4be', 'AbpTenantManagement', 'AbpTenantManagement.Tenants.Update', 'AbpTenantManagement.Tenants', 'L:AbpTenantManagement,Permission:Edit', true, 2, NULL, NULL, '{}');
INSERT INTO web."AbpPermissions" VALUES ('3a08f8df-08f0-53c7-f411-3ae40a1eb05b', 'AbpIdentity', 'AbpIdentity.UserLookup', NULL, 'L:AbpIdentity,Permission:UserLookup', true, 3, 'C', NULL, '{}');
INSERT INTO web."AbpPermissions" VALUES ('3a08f8df-08f0-7783-01cd-a90de619ea1f', 'AbpTenantManagement', 'AbpTenantManagement.Tenants.ManageFeatures', 'AbpTenantManagement.Tenants', 'L:AbpTenantManagement,Permission:ManageFeatures', true, 2, NULL, NULL, '{}');
INSERT INTO web."AbpPermissions" VALUES ('3a08f8df-08f0-815e-334d-f8173f08a1a3', 'AbpIdentity', 'AbpIdentity.Roles.Delete', 'AbpIdentity.Roles', 'L:AbpIdentity,Permission:Delete', true, 3, NULL, NULL, '{}');
INSERT INTO web."AbpPermissions" VALUES ('3a08f8df-08f0-af42-250f-bb699d77b083', 'AbpTenantManagement', 'AbpTenantManagement.Tenants', NULL, 'L:AbpTenantManagement,Permission:TenantManagement', true, 2, NULL, NULL, '{}');
INSERT INTO web."AbpPermissions" VALUES ('3a08f8df-08f0-b4cd-bfcd-896bb4aea07d', 'AbpIdentity', 'AbpIdentity.Roles.Create', 'AbpIdentity.Roles', 'L:AbpIdentity,Permission:Create', true, 3, NULL, NULL, '{}');
INSERT INTO web."AbpPermissions" VALUES ('3a08f8df-08f0-de12-021c-744b89924350', 'FeatureManagement', 'FeatureManagement.ManageHostFeatures', NULL, 'L:AbpFeatureManagement,Permission:FeatureManagement.ManageHostFeatures', true, 2, NULL, NULL, '{}');
INSERT INTO web."AbpPermissions" VALUES ('3a08f8df-08f0-de56-d58c-4317f10488c3', 'AbpIdentity', 'AbpIdentity.Users.Update', 'AbpIdentity.Users', 'L:AbpIdentity,Permission:Edit', true, 3, NULL, NULL, '{}');
INSERT INTO web."AbpPermissions" VALUES ('3a08f8df-08f0-f187-c935-04aac806c3a6', 'AbpIdentity', 'AbpIdentity.Users.ManagePermissions', 'AbpIdentity.Users', 'L:AbpIdentity,Permission:ChangePermissions', true, 3, NULL, NULL, '{}');
INSERT INTO web."AbpPermissions" VALUES ('3a08f8df-08f0-fc9e-23cc-067dee26fb84', 'AbpIdentity', 'AbpIdentity.Roles.ManagePermissions', 'AbpIdentity.Roles', 'L:AbpIdentity,Permission:ChangePermissions', true, 3, NULL, NULL, '{}');
INSERT INTO web."AbpPermissions" VALUES ('3a08f8df-08fb-2a0e-1004-5cae9cd4d378', 'SettingManagement', 'SettingManagement.Emailing.Test', 'SettingManagement.Emailing', 'L:AbpSettingManagement,Permission:EmailingTest', true, 3, NULL, NULL, '{}');


--
-- TOC entry 3947 (class 0 OID 311612)
-- Dependencies: 210
-- Data for Name: AbpRoleClaims; Type: TABLE DATA; Schema: web; Owner: -
--



--
-- TOC entry 3948 (class 0 OID 311618)
-- Dependencies: 211
-- Data for Name: AbpRoles; Type: TABLE DATA; Schema: web; Owner: -
--

INSERT INTO web."AbpRoles" VALUES ('3a08f8dd-96a6-27c9-77a5-6a47d42d4170', NULL, 'admin', 'ADMIN', false, true, true, '{}', 'f6fef09b26ab49329da706acdd406366');


--
-- TOC entry 3949 (class 0 OID 311624)
-- Dependencies: 212
-- Data for Name: AbpSecurityLogs; Type: TABLE DATA; Schema: web; Owner: -
--

INSERT INTO web."AbpSecurityLogs" VALUES ('3a08f8ee-ea05-1f75-91b9-999a9eeb5075', NULL, 'Jiban.PlatForm', 'Identity', 'LoginSucceeded', '3a08f8dd-9097-9845-0786-f73cf4ad2628', 'admin', NULL, NULL, 'da94dcf19213473ca87eff552e0b9ba0', '::1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/109.0.0.0 Safari/537.36 Edg/109.0.1518.61', '2023-01-24 21:29:16.400898', '{}', 'eb7786e17dc74b208ab08243fb5873a5');
INSERT INTO web."AbpSecurityLogs" VALUES ('3a08fa8f-8137-ae4c-fab6-7f2c66f83fe9', NULL, 'Jiban.PlatForm.HttpApi.Host', 'Identity', 'LoginSucceeded', '3a08f8dd-9097-9845-0786-f73cf4ad2628', 'admin', NULL, NULL, 'f601493389e54688b546e18cd65b6073', '127.0.0.1', 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/109.0.0.0 Safari/537.36 Edg/109.0.1518.61', '2023-01-25 05:04:18.099519', '{}', 'eec4f12339ca43aaae1d098538f0596c');


--
-- TOC entry 3950 (class 0 OID 311630)
-- Dependencies: 213
-- Data for Name: AbpSettings; Type: TABLE DATA; Schema: web; Owner: -
--



--
-- TOC entry 3951 (class 0 OID 311636)
-- Dependencies: 214
-- Data for Name: AbpTenantConnectionStrings; Type: TABLE DATA; Schema: web; Owner: -
--



--
-- TOC entry 3952 (class 0 OID 311642)
-- Dependencies: 215
-- Data for Name: AbpTenants; Type: TABLE DATA; Schema: web; Owner: -
--



--
-- TOC entry 3953 (class 0 OID 311649)
-- Dependencies: 216
-- Data for Name: AbpUserClaims; Type: TABLE DATA; Schema: web; Owner: -
--



--
-- TOC entry 3954 (class 0 OID 311655)
-- Dependencies: 217
-- Data for Name: AbpUserLogins; Type: TABLE DATA; Schema: web; Owner: -
--



--
-- TOC entry 3955 (class 0 OID 311658)
-- Dependencies: 218
-- Data for Name: AbpUserOrganizationUnits; Type: TABLE DATA; Schema: web; Owner: -
--



--
-- TOC entry 3956 (class 0 OID 311661)
-- Dependencies: 219
-- Data for Name: AbpUserRoles; Type: TABLE DATA; Schema: web; Owner: -
--

INSERT INTO web."AbpUserRoles" VALUES ('3a08f8dd-9097-9845-0786-f73cf4ad2628', '3a08f8dd-96a6-27c9-77a5-6a47d42d4170', NULL);


--
-- TOC entry 3957 (class 0 OID 311664)
-- Dependencies: 220
-- Data for Name: AbpUserTokens; Type: TABLE DATA; Schema: web; Owner: -
--



--
-- TOC entry 3958 (class 0 OID 311670)
-- Dependencies: 221
-- Data for Name: AbpUsers; Type: TABLE DATA; Schema: web; Owner: -
--

INSERT INTO web."AbpUsers" VALUES ('3a08f8dd-9097-9845-0786-f73cf4ad2628', NULL, 'admin', 'ADMIN', 'admin', NULL, 'admin@abp.io', 'ADMIN@ABP.IO', false, 'AQAAAAIAAYagAAAAEFzmiCm8Z7VbrrezcTx3VypuqgwDVSpH/yuWtF43LfyWknXHyYi0zBvmgPn9UPTFag==', 'NDKN7XXJZ6KVXSOV7I3QQAYGMSHLARS6', false, NULL, false, true, false, NULL, true, 0, '{}', '84db45a092954c07b1f3b76b3b922823', '2023-01-24 21:10:19.98434', NULL, '2023-01-24 21:10:21.361579', NULL, false, NULL, NULL);


--
-- TOC entry 3959 (class 0 OID 311683)
-- Dependencies: 222
-- Data for Name: OpenIddictApplications; Type: TABLE DATA; Schema: web; Owner: -
--

INSERT INTO web."OpenIddictApplications" VALUES ('3a08f8dd-9d83-3096-e98f-a20741666d22', 'PlatForm_App', NULL, 'implicit', 'Console Test / Angular Application', NULL, '["ept:logout","gt:authorization_code","rst:code","ept:authorization","ept:token","ept:revocation","ept:introspection","gt:password","gt:client_credentials","gt:refresh_token","scp:address","scp:email","scp:phone","scp:profile","scp:roles","scp:PlatForm"]', '["http://localhost:4200"]', NULL, '["http://localhost:4200"]', NULL, 'web', NULL, NULL, '{}', '2bb261c4f03d47eea6d3079ac9fcf542', '2023-01-24 21:10:22.916867', NULL, NULL, NULL, false, NULL, NULL);
INSERT INTO web."OpenIddictApplications" VALUES ('3a08f8dd-9eaf-21cc-4555-94868f8ab5e2', 'PlatForm_Swagger', NULL, 'implicit', 'Swagger Application', NULL, '["ept:logout","gt:authorization_code","rst:code","ept:authorization","ept:token","ept:revocation","ept:introspection","scp:address","scp:email","scp:phone","scp:profile","scp:roles","scp:PlatForm"]', NULL, NULL, '["https://localhost:44316/swagger/oauth2-redirect.html"]', NULL, 'web', NULL, NULL, '{}', '3b67214d27c74f4abde934b7e3323e26', '2023-01-24 21:10:23.032235', NULL, NULL, NULL, false, NULL, NULL);


--
-- TOC entry 3960 (class 0 OID 311690)
-- Dependencies: 223
-- Data for Name: OpenIddictAuthorizations; Type: TABLE DATA; Schema: web; Owner: -
--

INSERT INTO web."OpenIddictAuthorizations" VALUES ('3a08fa8f-87d3-39d6-97a3-ebec60bbc5fd', '3a08f8dd-9d83-3096-e98f-a20741666d22', '2023-01-25 10:04:19.792066', NULL, '["openid","offline_access","PlatForm"]', 'valid', '3a08f8dd-9097-9845-0786-f73cf4ad2628', 'permanent', '{}', 'eb2bb083189c4633bacbee0fe44d1ca4', '2023-01-25 05:04:19.906636', '3a08f8dd-9097-9845-0786-f73cf4ad2628', NULL, NULL, false, NULL, NULL);


--
-- TOC entry 3961 (class 0 OID 311697)
-- Dependencies: 224
-- Data for Name: OpenIddictScopes; Type: TABLE DATA; Schema: web; Owner: -
--

INSERT INTO web."OpenIddictScopes" VALUES ('3a08f8dd-9c4c-3b41-ee46-99cc647fd544', NULL, NULL, 'PlatForm API', NULL, 'PlatForm', NULL, '["PlatForm"]', '{}', '0f7059d64f1f4ab090ef6302d8df101b', '2023-01-24 21:10:22.507824', NULL, NULL, NULL, false, NULL, NULL);


--
-- TOC entry 3962 (class 0 OID 311704)
-- Dependencies: 225
-- Data for Name: OpenIddictTokens; Type: TABLE DATA; Schema: web; Owner: -
--

INSERT INTO web."OpenIddictTokens" VALUES ('3a08fa8f-88f1-1200-c2a1-df32807dca1d', '3a08f8dd-9d83-3096-e98f-a20741666d22', '3a08fa8f-87d3-39d6-97a3-ebec60bbc5fd', '2023-01-25 10:04:20', '2023-01-25 10:09:20', 'eyJhbGciOiJSU0EtT0FFUCIsImVuYyI6IkEyNTZDQkMtSFM1MTIiLCJraWQiOiI3MUNCRTE5QkU2OUQyMUFDMTgyMjlEMEIxODlCMDNDRTFBOEI2ODY4IiwidHlwIjoib2lfYXVjK2p3dCIsImN0eSI6IkpXVCJ9.o4N7Ty9NkZ9UWEU5-VmwJQcY88p9Jifxr0YG9cLuNF2rUXEPtczF_NA2Tbrh62nRW5M7IFexQm1BJPRt65KwhIYNAPyLPCjaTYVoSDuScLMfTU855ZdYWjrEDCri8RM6NoEe6rpnFRg2FZynSMipC7vrBDwDTPCCB12ef6Zoe1SpxeTdg7sv_p9SC-CeUUzDW_0cv8hQjkPSPE38353sThN1I3-VH9TzTtG6jBvkBXVaSHNABN-9AJ8SnOYsu93M-ju69DKtVU2rXrwrJXXBaVEeLUMVuAgxAniq_WbTIFEicKuB8tVeeZG_g2oAsq0sQFUdesRGSWEYbcj0xqpizA.rLq7I_ghz4NTkU_Et-YR9A.j-L5_9XAOAePgElNA5YQkrHAnNZCYxTVAv0bLnEGrgMuqXD2EamrG0CJlLXEtWznanH-jW8Qhi52xxLXBcGem9xrlh8km3Ja372OJDh76WT9HO4BBc_yLVAtgeEzFSai8ktbDYrkVzywqyAAwBaaFwhfsEMwZUYJIVrLAA_0LpHYuTpkO2s8ApYDxFxZl3dR8QnetwHNTd52onhMlJZPvQa527NUlc0di2FasUxx7bIYujmUnxCPmQFM_W-2Z4N4R3BtnQhs96rhLvnBgvoq_rftpKdiA-dwmOBm8iSqWMswgf2T_HL5XlC0FwwsYQdPS3UjJPA7wEj-YL5lRgGCNQ2y-gJ8s-c8Fyl9IvfymTcOVSO3Nrxva8-3AqGbsxBNPK1ySfT-iMcOp6gjqqNrhFeR5uh32OvlhByY7w7LUrN_PtuH5zEadC3BFWnQajG8DbqUDbiRGRd5DhSEL6KEZkbT6kn9rT5O8Qym4t6UBIoWMzWqL4wZTRXfKt_r_9oaEtlpFKzc9N_xlCQelWuQ7j8q0evKmq2uH97wJdiSukCY09JxloTFNTzWfoLXt4-ffvKornYxU2g7ZAj4JzsHSmTrJcCKt5njWBY0lrfoWMB69OR-f_VdGY4iJndNlEQrZWFLKVkK5tBNsLC2O_B7EDox6vu_1CFLb5nfBsY3GQCxLwp1Gwybdh7MexxG2JUXhb_kBSW48-YVtqrdXmDH-hQQHw8_4ZtHDCCI9jKVQ0E5FvfzhGjX63_do8eWuLM14hGBAiVm_ApEdxE5EwSYLUljQ3ASSRDLtXEk1v8srFNCD8XkMb_DNfL7n9tHukutOsaL0ZNXNTxBVO8EmfVuvSMdAhezQrU2hFhTbX2tSdeoAbBRbc2rZa51LbI9LOpVNOSsn1DEJjxoLnjDTFvMpz8fcQ82zpxSuiEgvbaHg6FvKVpwlgIhli23tAsD5WCtJgiA-NyzOUh2GDd4hDoNofY_deKA_M7cj6pDM0ROZCupxtkokNKEdpQvrOq1TfUrwSI5iFhrtbd6_CQsRQg6MBg1mUvdLGU0v1oDSq1m0qgXVn8axQ-hwlhlHStelC3QcgdYj6aTmXk0-FKlthUdTzwDz3-i3Co7ryxwFIadOMU0ZpzgJXxkEWk56CZRO0qH4bWsAdvX7NuBt3o_nyYsUiAzjkCP--OiIrFl5Wo2ZKCTy7Rqhop1R6h2mJpd7mLrt3AjS4OGvcbLOC0dKM47pk_8jfM4sdp6_8os___Uq6XmGagnFFLGN9bt5adSpEAaK-9h4vPq4ctJuSEEfLTAkBIjiPhy7eM0xf6vsvMRBlMRpQCsYzviHjFsoEkiBdQhn7gav-9EYMUUIG1Tbzd4Kp4xy20dRu9RHmVKfL-jor7-QXPja47BYD7_v-_Q_KrOlpXocp6mGCT5xzDk3RXYW6dEAwgCf_hJviXgXLnndTNglTZclI949wy-Uqqq-wk154ye2b39jjhmuf7Xdq0BIy2XUehuAOL8lFRdkRIVea6vWciKGYYWH1Eum44gOpu47a4ZTCgW0nXePeWvTRXbkp-QlzDPCMyJ9WWYxb6RrH7Z4dmfJqUhXQGvYFQd2Rs5cqT0Oo3X9-CsafihxmWVsSNtnRDEJo29mee-nOLYgNlx0sZzLsCrxENXYI-xPWSgk3YuszU9kvcJMnVWOzzQiQSBlVZ1HrpZt4_BlBtse7yf34LxPe5Ss2hNKnsVuKftMgW1Vsa5xofwdnBkXGfo6N_cHgRevjorYqcacn6LQt5jlW0w7neP91MLKk_K-_loixFeQCPyQO5IKXLXPjakjHieHzaxqk4WrE2d64gT_dmi2dKDLDteRt8dZn0FrxPYet4McxPDJB_OC4gJx1iX00JXn4aqYxc0TX-eun3-KAFG26cUvv5JHonOkqEZ_UZGaTv3SByZifNjIZ3Yz_ADACBjHO46iBlWUOJMzkMW6W2OnP69w9tDZ-dA7DzUewfpwIggLhgjFQMlhwHNRvXvwt6K6E1jX1doCoVc5ra5BoYEHf9cIE0AiPcnCqnmhjUjEbcypy8JLmV1qs1QmH3niDHFfkxOv8FC5W3Z5s7g_eHs7IDF47dA5NSBAqg3ktEMqUFk7Dipct4BKjjjAJWbXVixqPMEVIqG2UO1anO38LJChUCmRGWhbgpheG6UatwhBnYFssEgg_5uTQBFDIJnogvjAREFB9f12hpWjs0qf9x9r_DWNqCykOLrLQQ6pkT-QEyQs9HjO1Lecr7GEQ2OiZmZTBbDKjsr0CU8BtXrD6qVdnPUNRzisit3j8EyZG2zImVQEuV2PdXqrPBQS-6aRJ6xpyJnp19d_vDlQGmJ6FC6SNSgJ3J0Wt9REA6InltGqO_3eDfW6m0_9-LByOXqXyNCXpV_jvuONcNmzGVzXXhp7hmrdSAdwoqn5cPBPiMI0qQ0p2nCMRlxj_BuAy1K1kWdUq8pxalx1uk1SwKItQuGmOa2bi2Hl5b81ojz5s9GpUF33jIfUZuZuZQTq7em-K8P4NFnNC7UxZz3TApB12stUsVoT6Uo2K4cy1Sgx2-t05q4dnI8RBT5yQwkGw-lA7hxrysFqaEevtK2gDsIrzFvBukxePsSAMZ5fEPWrPQ-5hxBHByJrb3GqVY8PAEvr2yOeoz9PwfzkmaOfgiHlrysUV_shJvSHLYpxXuh1maY.ifXA40sKrxtnXCx75wWN64i8ELX_6Ayk23AA9-8ge50', NULL, '2023-01-25 10:04:23.322165', 'A6VaDdpMtTwnWry3Rv9cOIcsN39LOHmcjZPWbW8j4UI=', 'redeemed', '3a08f8dd-9097-9845-0786-f73cf4ad2628', 'authorization_code', '{}', '85ab08e5771d449a9215502112825dc2', '2023-01-25 05:04:20.187469', '3a08f8dd-9097-9845-0786-f73cf4ad2628', '2023-01-25 05:04:23.336114', NULL, false, NULL, NULL);
INSERT INTO web."OpenIddictTokens" VALUES ('3a08fa8f-95d0-2482-242a-d1c00d2f1baf', '3a08f8dd-9d83-3096-e98f-a20741666d22', '3a08fa8f-87d3-39d6-97a3-ebec60bbc5fd', '2023-01-25 10:04:23', '2023-01-25 11:04:23', NULL, NULL, NULL, NULL, 'valid', '3a08f8dd-9097-9845-0786-f73cf4ad2628', 'access_token', '{}', '8e1310a1acf94dbaab01902d66d240d6', '2023-01-25 05:04:23.387621', NULL, NULL, NULL, false, NULL, NULL);
INSERT INTO web."OpenIddictTokens" VALUES ('3a08fa8f-95f3-b75b-f0d3-58ae2da5265e', '3a08f8dd-9d83-3096-e98f-a20741666d22', '3a08fa8f-87d3-39d6-97a3-ebec60bbc5fd', '2023-01-25 10:04:23', '2023-02-08 10:04:23', NULL, NULL, NULL, NULL, 'valid', '3a08f8dd-9097-9845-0786-f73cf4ad2628', 'refresh_token', '{}', '8a196250ed0f4889b1c604f99951576e', '2023-01-25 05:04:23.41854', NULL, NULL, NULL, false, NULL, NULL);
INSERT INTO web."OpenIddictTokens" VALUES ('3a08fa8f-962b-e302-7eaf-7e5ad3a3b5e9', '3a08f8dd-9d83-3096-e98f-a20741666d22', '3a08fa8f-87d3-39d6-97a3-ebec60bbc5fd', '2023-01-25 10:04:23', '2023-01-25 10:24:23', NULL, NULL, NULL, NULL, 'valid', '3a08f8dd-9097-9845-0786-f73cf4ad2628', 'id_token', '{}', '5d4dd92847c44d83bc7e0df70bc90b00', '2023-01-25 05:04:23.475798', NULL, NULL, NULL, false, NULL, NULL);


--
-- TOC entry 3963 (class 0 OID 311711)
-- Dependencies: 226
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: web; Owner: -
--

INSERT INTO web."__EFMigrationsHistory" VALUES ('20230125020628_Initial', '7.0.1');


--
-- TOC entry 3694 (class 2606 OID 311716)
-- Name: AbpAuditLogActions PK_AbpAuditLogActions; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpAuditLogActions"
    ADD CONSTRAINT "PK_AbpAuditLogActions" PRIMARY KEY ("Id");


--
-- TOC entry 3698 (class 2606 OID 311718)
-- Name: AbpAuditLogs PK_AbpAuditLogs; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpAuditLogs"
    ADD CONSTRAINT "PK_AbpAuditLogs" PRIMARY KEY ("Id");


--
-- TOC entry 3700 (class 2606 OID 311720)
-- Name: AbpClaimTypes PK_AbpClaimTypes; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpClaimTypes"
    ADD CONSTRAINT "PK_AbpClaimTypes" PRIMARY KEY ("Id");


--
-- TOC entry 3704 (class 2606 OID 311722)
-- Name: AbpEntityChanges PK_AbpEntityChanges; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpEntityChanges"
    ADD CONSTRAINT "PK_AbpEntityChanges" PRIMARY KEY ("Id");


--
-- TOC entry 3707 (class 2606 OID 311724)
-- Name: AbpEntityPropertyChanges PK_AbpEntityPropertyChanges; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpEntityPropertyChanges"
    ADD CONSTRAINT "PK_AbpEntityPropertyChanges" PRIMARY KEY ("Id");


--
-- TOC entry 3710 (class 2606 OID 311726)
-- Name: AbpFeatureGroups PK_AbpFeatureGroups; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpFeatureGroups"
    ADD CONSTRAINT "PK_AbpFeatureGroups" PRIMARY KEY ("Id");


--
-- TOC entry 3713 (class 2606 OID 311728)
-- Name: AbpFeatureValues PK_AbpFeatureValues; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpFeatureValues"
    ADD CONSTRAINT "PK_AbpFeatureValues" PRIMARY KEY ("Id");


--
-- TOC entry 3717 (class 2606 OID 311730)
-- Name: AbpFeatures PK_AbpFeatures; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpFeatures"
    ADD CONSTRAINT "PK_AbpFeatures" PRIMARY KEY ("Id");


--
-- TOC entry 3720 (class 2606 OID 311732)
-- Name: AbpLinkUsers PK_AbpLinkUsers; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpLinkUsers"
    ADD CONSTRAINT "PK_AbpLinkUsers" PRIMARY KEY ("Id");


--
-- TOC entry 3723 (class 2606 OID 311734)
-- Name: AbpOrganizationUnitRoles PK_AbpOrganizationUnitRoles; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpOrganizationUnitRoles"
    ADD CONSTRAINT "PK_AbpOrganizationUnitRoles" PRIMARY KEY ("OrganizationUnitId", "RoleId");


--
-- TOC entry 3727 (class 2606 OID 311736)
-- Name: AbpOrganizationUnits PK_AbpOrganizationUnits; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpOrganizationUnits"
    ADD CONSTRAINT "PK_AbpOrganizationUnits" PRIMARY KEY ("Id");


--
-- TOC entry 3730 (class 2606 OID 311738)
-- Name: AbpPermissionGrants PK_AbpPermissionGrants; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpPermissionGrants"
    ADD CONSTRAINT "PK_AbpPermissionGrants" PRIMARY KEY ("Id");


--
-- TOC entry 3733 (class 2606 OID 311740)
-- Name: AbpPermissionGroups PK_AbpPermissionGroups; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpPermissionGroups"
    ADD CONSTRAINT "PK_AbpPermissionGroups" PRIMARY KEY ("Id");


--
-- TOC entry 3737 (class 2606 OID 311742)
-- Name: AbpPermissions PK_AbpPermissions; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpPermissions"
    ADD CONSTRAINT "PK_AbpPermissions" PRIMARY KEY ("Id");


--
-- TOC entry 3740 (class 2606 OID 311744)
-- Name: AbpRoleClaims PK_AbpRoleClaims; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpRoleClaims"
    ADD CONSTRAINT "PK_AbpRoleClaims" PRIMARY KEY ("Id");


--
-- TOC entry 3743 (class 2606 OID 311746)
-- Name: AbpRoles PK_AbpRoles; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpRoles"
    ADD CONSTRAINT "PK_AbpRoles" PRIMARY KEY ("Id");


--
-- TOC entry 3749 (class 2606 OID 311748)
-- Name: AbpSecurityLogs PK_AbpSecurityLogs; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpSecurityLogs"
    ADD CONSTRAINT "PK_AbpSecurityLogs" PRIMARY KEY ("Id");


--
-- TOC entry 3752 (class 2606 OID 311750)
-- Name: AbpSettings PK_AbpSettings; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpSettings"
    ADD CONSTRAINT "PK_AbpSettings" PRIMARY KEY ("Id");


--
-- TOC entry 3754 (class 2606 OID 311752)
-- Name: AbpTenantConnectionStrings PK_AbpTenantConnectionStrings; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpTenantConnectionStrings"
    ADD CONSTRAINT "PK_AbpTenantConnectionStrings" PRIMARY KEY ("TenantId", "Name");


--
-- TOC entry 3757 (class 2606 OID 311754)
-- Name: AbpTenants PK_AbpTenants; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpTenants"
    ADD CONSTRAINT "PK_AbpTenants" PRIMARY KEY ("Id");


--
-- TOC entry 3760 (class 2606 OID 311756)
-- Name: AbpUserClaims PK_AbpUserClaims; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpUserClaims"
    ADD CONSTRAINT "PK_AbpUserClaims" PRIMARY KEY ("Id");


--
-- TOC entry 3763 (class 2606 OID 311758)
-- Name: AbpUserLogins PK_AbpUserLogins; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpUserLogins"
    ADD CONSTRAINT "PK_AbpUserLogins" PRIMARY KEY ("UserId", "LoginProvider");


--
-- TOC entry 3766 (class 2606 OID 311760)
-- Name: AbpUserOrganizationUnits PK_AbpUserOrganizationUnits; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpUserOrganizationUnits"
    ADD CONSTRAINT "PK_AbpUserOrganizationUnits" PRIMARY KEY ("OrganizationUnitId", "UserId");


--
-- TOC entry 3769 (class 2606 OID 311762)
-- Name: AbpUserRoles PK_AbpUserRoles; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpUserRoles"
    ADD CONSTRAINT "PK_AbpUserRoles" PRIMARY KEY ("UserId", "RoleId");


--
-- TOC entry 3771 (class 2606 OID 311764)
-- Name: AbpUserTokens PK_AbpUserTokens; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpUserTokens"
    ADD CONSTRAINT "PK_AbpUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name");


--
-- TOC entry 3777 (class 2606 OID 311766)
-- Name: AbpUsers PK_AbpUsers; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpUsers"
    ADD CONSTRAINT "PK_AbpUsers" PRIMARY KEY ("Id");


--
-- TOC entry 3780 (class 2606 OID 311768)
-- Name: OpenIddictApplications PK_OpenIddictApplications; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."OpenIddictApplications"
    ADD CONSTRAINT "PK_OpenIddictApplications" PRIMARY KEY ("Id");


--
-- TOC entry 3783 (class 2606 OID 311770)
-- Name: OpenIddictAuthorizations PK_OpenIddictAuthorizations; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."OpenIddictAuthorizations"
    ADD CONSTRAINT "PK_OpenIddictAuthorizations" PRIMARY KEY ("Id");


--
-- TOC entry 3786 (class 2606 OID 311772)
-- Name: OpenIddictScopes PK_OpenIddictScopes; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."OpenIddictScopes"
    ADD CONSTRAINT "PK_OpenIddictScopes" PRIMARY KEY ("Id");


--
-- TOC entry 3791 (class 2606 OID 311774)
-- Name: OpenIddictTokens PK_OpenIddictTokens; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."OpenIddictTokens"
    ADD CONSTRAINT "PK_OpenIddictTokens" PRIMARY KEY ("Id");


--
-- TOC entry 3793 (class 2606 OID 311776)
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- TOC entry 3691 (class 1259 OID 311777)
-- Name: IX_AbpAuditLogActions_AuditLogId; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpAuditLogActions_AuditLogId" ON web."AbpAuditLogActions" USING btree ("AuditLogId");


--
-- TOC entry 3692 (class 1259 OID 311778)
-- Name: IX_AbpAuditLogActions_TenantId_ServiceName_MethodName_Executio~; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpAuditLogActions_TenantId_ServiceName_MethodName_Executio~" ON web."AbpAuditLogActions" USING btree ("TenantId", "ServiceName", "MethodName", "ExecutionTime");


--
-- TOC entry 3695 (class 1259 OID 311779)
-- Name: IX_AbpAuditLogs_TenantId_ExecutionTime; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpAuditLogs_TenantId_ExecutionTime" ON web."AbpAuditLogs" USING btree ("TenantId", "ExecutionTime");


--
-- TOC entry 3696 (class 1259 OID 311780)
-- Name: IX_AbpAuditLogs_TenantId_UserId_ExecutionTime; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpAuditLogs_TenantId_UserId_ExecutionTime" ON web."AbpAuditLogs" USING btree ("TenantId", "UserId", "ExecutionTime");


--
-- TOC entry 3701 (class 1259 OID 311781)
-- Name: IX_AbpEntityChanges_AuditLogId; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpEntityChanges_AuditLogId" ON web."AbpEntityChanges" USING btree ("AuditLogId");


--
-- TOC entry 3702 (class 1259 OID 311782)
-- Name: IX_AbpEntityChanges_TenantId_EntityTypeFullName_EntityId; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpEntityChanges_TenantId_EntityTypeFullName_EntityId" ON web."AbpEntityChanges" USING btree ("TenantId", "EntityTypeFullName", "EntityId");


--
-- TOC entry 3705 (class 1259 OID 311783)
-- Name: IX_AbpEntityPropertyChanges_EntityChangeId; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpEntityPropertyChanges_EntityChangeId" ON web."AbpEntityPropertyChanges" USING btree ("EntityChangeId");


--
-- TOC entry 3708 (class 1259 OID 311784)
-- Name: IX_AbpFeatureGroups_Name; Type: INDEX; Schema: web; Owner: -
--

CREATE UNIQUE INDEX "IX_AbpFeatureGroups_Name" ON web."AbpFeatureGroups" USING btree ("Name");


--
-- TOC entry 3711 (class 1259 OID 311785)
-- Name: IX_AbpFeatureValues_Name_ProviderName_ProviderKey; Type: INDEX; Schema: web; Owner: -
--

CREATE UNIQUE INDEX "IX_AbpFeatureValues_Name_ProviderName_ProviderKey" ON web."AbpFeatureValues" USING btree ("Name", "ProviderName", "ProviderKey");


--
-- TOC entry 3714 (class 1259 OID 311786)
-- Name: IX_AbpFeatures_GroupName; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpFeatures_GroupName" ON web."AbpFeatures" USING btree ("GroupName");


--
-- TOC entry 3715 (class 1259 OID 311787)
-- Name: IX_AbpFeatures_Name; Type: INDEX; Schema: web; Owner: -
--

CREATE UNIQUE INDEX "IX_AbpFeatures_Name" ON web."AbpFeatures" USING btree ("Name");


--
-- TOC entry 3718 (class 1259 OID 311788)
-- Name: IX_AbpLinkUsers_SourceUserId_SourceTenantId_TargetUserId_Targe~; Type: INDEX; Schema: web; Owner: -
--

CREATE UNIQUE INDEX "IX_AbpLinkUsers_SourceUserId_SourceTenantId_TargetUserId_Targe~" ON web."AbpLinkUsers" USING btree ("SourceUserId", "SourceTenantId", "TargetUserId", "TargetTenantId");


--
-- TOC entry 3721 (class 1259 OID 311789)
-- Name: IX_AbpOrganizationUnitRoles_RoleId_OrganizationUnitId; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpOrganizationUnitRoles_RoleId_OrganizationUnitId" ON web."AbpOrganizationUnitRoles" USING btree ("RoleId", "OrganizationUnitId");


--
-- TOC entry 3724 (class 1259 OID 311790)
-- Name: IX_AbpOrganizationUnits_Code; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpOrganizationUnits_Code" ON web."AbpOrganizationUnits" USING btree ("Code");


--
-- TOC entry 3725 (class 1259 OID 311791)
-- Name: IX_AbpOrganizationUnits_ParentId; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpOrganizationUnits_ParentId" ON web."AbpOrganizationUnits" USING btree ("ParentId");


--
-- TOC entry 3728 (class 1259 OID 311792)
-- Name: IX_AbpPermissionGrants_TenantId_Name_ProviderName_ProviderKey; Type: INDEX; Schema: web; Owner: -
--

CREATE UNIQUE INDEX "IX_AbpPermissionGrants_TenantId_Name_ProviderName_ProviderKey" ON web."AbpPermissionGrants" USING btree ("TenantId", "Name", "ProviderName", "ProviderKey");


--
-- TOC entry 3731 (class 1259 OID 311793)
-- Name: IX_AbpPermissionGroups_Name; Type: INDEX; Schema: web; Owner: -
--

CREATE UNIQUE INDEX "IX_AbpPermissionGroups_Name" ON web."AbpPermissionGroups" USING btree ("Name");


--
-- TOC entry 3734 (class 1259 OID 311794)
-- Name: IX_AbpPermissions_GroupName; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpPermissions_GroupName" ON web."AbpPermissions" USING btree ("GroupName");


--
-- TOC entry 3735 (class 1259 OID 311795)
-- Name: IX_AbpPermissions_Name; Type: INDEX; Schema: web; Owner: -
--

CREATE UNIQUE INDEX "IX_AbpPermissions_Name" ON web."AbpPermissions" USING btree ("Name");


--
-- TOC entry 3738 (class 1259 OID 311796)
-- Name: IX_AbpRoleClaims_RoleId; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpRoleClaims_RoleId" ON web."AbpRoleClaims" USING btree ("RoleId");


--
-- TOC entry 3741 (class 1259 OID 311797)
-- Name: IX_AbpRoles_NormalizedName; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpRoles_NormalizedName" ON web."AbpRoles" USING btree ("NormalizedName");


--
-- TOC entry 3744 (class 1259 OID 311798)
-- Name: IX_AbpSecurityLogs_TenantId_Action; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpSecurityLogs_TenantId_Action" ON web."AbpSecurityLogs" USING btree ("TenantId", "Action");


--
-- TOC entry 3745 (class 1259 OID 311799)
-- Name: IX_AbpSecurityLogs_TenantId_ApplicationName; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpSecurityLogs_TenantId_ApplicationName" ON web."AbpSecurityLogs" USING btree ("TenantId", "ApplicationName");


--
-- TOC entry 3746 (class 1259 OID 311800)
-- Name: IX_AbpSecurityLogs_TenantId_Identity; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpSecurityLogs_TenantId_Identity" ON web."AbpSecurityLogs" USING btree ("TenantId", "Identity");


--
-- TOC entry 3747 (class 1259 OID 311801)
-- Name: IX_AbpSecurityLogs_TenantId_UserId; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpSecurityLogs_TenantId_UserId" ON web."AbpSecurityLogs" USING btree ("TenantId", "UserId");


--
-- TOC entry 3750 (class 1259 OID 311802)
-- Name: IX_AbpSettings_Name_ProviderName_ProviderKey; Type: INDEX; Schema: web; Owner: -
--

CREATE UNIQUE INDEX "IX_AbpSettings_Name_ProviderName_ProviderKey" ON web."AbpSettings" USING btree ("Name", "ProviderName", "ProviderKey");


--
-- TOC entry 3755 (class 1259 OID 311803)
-- Name: IX_AbpTenants_Name; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpTenants_Name" ON web."AbpTenants" USING btree ("Name");


--
-- TOC entry 3758 (class 1259 OID 311804)
-- Name: IX_AbpUserClaims_UserId; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpUserClaims_UserId" ON web."AbpUserClaims" USING btree ("UserId");


--
-- TOC entry 3761 (class 1259 OID 311805)
-- Name: IX_AbpUserLogins_LoginProvider_ProviderKey; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpUserLogins_LoginProvider_ProviderKey" ON web."AbpUserLogins" USING btree ("LoginProvider", "ProviderKey");


--
-- TOC entry 3764 (class 1259 OID 311806)
-- Name: IX_AbpUserOrganizationUnits_UserId_OrganizationUnitId; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpUserOrganizationUnits_UserId_OrganizationUnitId" ON web."AbpUserOrganizationUnits" USING btree ("UserId", "OrganizationUnitId");


--
-- TOC entry 3767 (class 1259 OID 311807)
-- Name: IX_AbpUserRoles_RoleId_UserId; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpUserRoles_RoleId_UserId" ON web."AbpUserRoles" USING btree ("RoleId", "UserId");


--
-- TOC entry 3772 (class 1259 OID 311808)
-- Name: IX_AbpUsers_Email; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpUsers_Email" ON web."AbpUsers" USING btree ("Email");


--
-- TOC entry 3773 (class 1259 OID 311809)
-- Name: IX_AbpUsers_NormalizedEmail; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpUsers_NormalizedEmail" ON web."AbpUsers" USING btree ("NormalizedEmail");


--
-- TOC entry 3774 (class 1259 OID 311810)
-- Name: IX_AbpUsers_NormalizedUserName; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpUsers_NormalizedUserName" ON web."AbpUsers" USING btree ("NormalizedUserName");


--
-- TOC entry 3775 (class 1259 OID 311811)
-- Name: IX_AbpUsers_UserName; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpUsers_UserName" ON web."AbpUsers" USING btree ("UserName");


--
-- TOC entry 3778 (class 1259 OID 311812)
-- Name: IX_OpenIddictApplications_ClientId; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_OpenIddictApplications_ClientId" ON web."OpenIddictApplications" USING btree ("ClientId");


--
-- TOC entry 3781 (class 1259 OID 311813)
-- Name: IX_OpenIddictAuthorizations_ApplicationId_Status_Subject_Type; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_OpenIddictAuthorizations_ApplicationId_Status_Subject_Type" ON web."OpenIddictAuthorizations" USING btree ("ApplicationId", "Status", "Subject", "Type");


--
-- TOC entry 3784 (class 1259 OID 311814)
-- Name: IX_OpenIddictScopes_Name; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_OpenIddictScopes_Name" ON web."OpenIddictScopes" USING btree ("Name");


--
-- TOC entry 3787 (class 1259 OID 311815)
-- Name: IX_OpenIddictTokens_ApplicationId_Status_Subject_Type; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_OpenIddictTokens_ApplicationId_Status_Subject_Type" ON web."OpenIddictTokens" USING btree ("ApplicationId", "Status", "Subject", "Type");


--
-- TOC entry 3788 (class 1259 OID 311816)
-- Name: IX_OpenIddictTokens_AuthorizationId; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_OpenIddictTokens_AuthorizationId" ON web."OpenIddictTokens" USING btree ("AuthorizationId");


--
-- TOC entry 3789 (class 1259 OID 311817)
-- Name: IX_OpenIddictTokens_ReferenceId; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_OpenIddictTokens_ReferenceId" ON web."OpenIddictTokens" USING btree ("ReferenceId");


--
-- TOC entry 3794 (class 2606 OID 311818)
-- Name: AbpAuditLogActions FK_AbpAuditLogActions_AbpAuditLogs_AuditLogId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpAuditLogActions"
    ADD CONSTRAINT "FK_AbpAuditLogActions_AbpAuditLogs_AuditLogId" FOREIGN KEY ("AuditLogId") REFERENCES web."AbpAuditLogs"("Id") ON DELETE CASCADE;


--
-- TOC entry 3795 (class 2606 OID 311823)
-- Name: AbpEntityChanges FK_AbpEntityChanges_AbpAuditLogs_AuditLogId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpEntityChanges"
    ADD CONSTRAINT "FK_AbpEntityChanges_AbpAuditLogs_AuditLogId" FOREIGN KEY ("AuditLogId") REFERENCES web."AbpAuditLogs"("Id") ON DELETE CASCADE;


--
-- TOC entry 3796 (class 2606 OID 311828)
-- Name: AbpEntityPropertyChanges FK_AbpEntityPropertyChanges_AbpEntityChanges_EntityChangeId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpEntityPropertyChanges"
    ADD CONSTRAINT "FK_AbpEntityPropertyChanges_AbpEntityChanges_EntityChangeId" FOREIGN KEY ("EntityChangeId") REFERENCES web."AbpEntityChanges"("Id") ON DELETE CASCADE;


--
-- TOC entry 3797 (class 2606 OID 311833)
-- Name: AbpOrganizationUnitRoles FK_AbpOrganizationUnitRoles_AbpOrganizationUnits_OrganizationU~; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpOrganizationUnitRoles"
    ADD CONSTRAINT "FK_AbpOrganizationUnitRoles_AbpOrganizationUnits_OrganizationU~" FOREIGN KEY ("OrganizationUnitId") REFERENCES web."AbpOrganizationUnits"("Id") ON DELETE CASCADE;


--
-- TOC entry 3798 (class 2606 OID 311838)
-- Name: AbpOrganizationUnitRoles FK_AbpOrganizationUnitRoles_AbpRoles_RoleId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpOrganizationUnitRoles"
    ADD CONSTRAINT "FK_AbpOrganizationUnitRoles_AbpRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES web."AbpRoles"("Id") ON DELETE CASCADE;


--
-- TOC entry 3799 (class 2606 OID 311843)
-- Name: AbpOrganizationUnits FK_AbpOrganizationUnits_AbpOrganizationUnits_ParentId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpOrganizationUnits"
    ADD CONSTRAINT "FK_AbpOrganizationUnits_AbpOrganizationUnits_ParentId" FOREIGN KEY ("ParentId") REFERENCES web."AbpOrganizationUnits"("Id");


--
-- TOC entry 3800 (class 2606 OID 311848)
-- Name: AbpRoleClaims FK_AbpRoleClaims_AbpRoles_RoleId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpRoleClaims"
    ADD CONSTRAINT "FK_AbpRoleClaims_AbpRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES web."AbpRoles"("Id") ON DELETE CASCADE;


--
-- TOC entry 3801 (class 2606 OID 311853)
-- Name: AbpTenantConnectionStrings FK_AbpTenantConnectionStrings_AbpTenants_TenantId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpTenantConnectionStrings"
    ADD CONSTRAINT "FK_AbpTenantConnectionStrings_AbpTenants_TenantId" FOREIGN KEY ("TenantId") REFERENCES web."AbpTenants"("Id") ON DELETE CASCADE;


--
-- TOC entry 3802 (class 2606 OID 311858)
-- Name: AbpUserClaims FK_AbpUserClaims_AbpUsers_UserId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpUserClaims"
    ADD CONSTRAINT "FK_AbpUserClaims_AbpUsers_UserId" FOREIGN KEY ("UserId") REFERENCES web."AbpUsers"("Id") ON DELETE CASCADE;


--
-- TOC entry 3803 (class 2606 OID 311863)
-- Name: AbpUserLogins FK_AbpUserLogins_AbpUsers_UserId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpUserLogins"
    ADD CONSTRAINT "FK_AbpUserLogins_AbpUsers_UserId" FOREIGN KEY ("UserId") REFERENCES web."AbpUsers"("Id") ON DELETE CASCADE;


--
-- TOC entry 3804 (class 2606 OID 311868)
-- Name: AbpUserOrganizationUnits FK_AbpUserOrganizationUnits_AbpOrganizationUnits_OrganizationU~; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpUserOrganizationUnits"
    ADD CONSTRAINT "FK_AbpUserOrganizationUnits_AbpOrganizationUnits_OrganizationU~" FOREIGN KEY ("OrganizationUnitId") REFERENCES web."AbpOrganizationUnits"("Id") ON DELETE CASCADE;


--
-- TOC entry 3805 (class 2606 OID 311873)
-- Name: AbpUserOrganizationUnits FK_AbpUserOrganizationUnits_AbpUsers_UserId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpUserOrganizationUnits"
    ADD CONSTRAINT "FK_AbpUserOrganizationUnits_AbpUsers_UserId" FOREIGN KEY ("UserId") REFERENCES web."AbpUsers"("Id") ON DELETE CASCADE;


--
-- TOC entry 3806 (class 2606 OID 311878)
-- Name: AbpUserRoles FK_AbpUserRoles_AbpRoles_RoleId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpUserRoles"
    ADD CONSTRAINT "FK_AbpUserRoles_AbpRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES web."AbpRoles"("Id") ON DELETE CASCADE;


--
-- TOC entry 3807 (class 2606 OID 311883)
-- Name: AbpUserRoles FK_AbpUserRoles_AbpUsers_UserId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpUserRoles"
    ADD CONSTRAINT "FK_AbpUserRoles_AbpUsers_UserId" FOREIGN KEY ("UserId") REFERENCES web."AbpUsers"("Id") ON DELETE CASCADE;


--
-- TOC entry 3808 (class 2606 OID 311888)
-- Name: AbpUserTokens FK_AbpUserTokens_AbpUsers_UserId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpUserTokens"
    ADD CONSTRAINT "FK_AbpUserTokens_AbpUsers_UserId" FOREIGN KEY ("UserId") REFERENCES web."AbpUsers"("Id") ON DELETE CASCADE;


--
-- TOC entry 3809 (class 2606 OID 311893)
-- Name: OpenIddictAuthorizations FK_OpenIddictAuthorizations_OpenIddictApplications_Application~; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."OpenIddictAuthorizations"
    ADD CONSTRAINT "FK_OpenIddictAuthorizations_OpenIddictApplications_Application~" FOREIGN KEY ("ApplicationId") REFERENCES web."OpenIddictApplications"("Id");


--
-- TOC entry 3810 (class 2606 OID 311898)
-- Name: OpenIddictTokens FK_OpenIddictTokens_OpenIddictApplications_ApplicationId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."OpenIddictTokens"
    ADD CONSTRAINT "FK_OpenIddictTokens_OpenIddictApplications_ApplicationId" FOREIGN KEY ("ApplicationId") REFERENCES web."OpenIddictApplications"("Id");


--
-- TOC entry 3811 (class 2606 OID 311903)
-- Name: OpenIddictTokens FK_OpenIddictTokens_OpenIddictAuthorizations_AuthorizationId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."OpenIddictTokens"
    ADD CONSTRAINT "FK_OpenIddictTokens_OpenIddictAuthorizations_AuthorizationId" FOREIGN KEY ("AuthorizationId") REFERENCES web."OpenIddictAuthorizations"("Id");


-- Completed on 2023-01-25 05:24:20 -05

--
-- PostgreSQL database dump complete
--

