--
-- PostgreSQL database dump
--

-- Dumped from database version 10.23
-- Dumped by pg_dump version 10.23

-- Started on 2023-01-24 19:49:58 -05

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
-- TOC entry 3983 (class 0 OID 0)
-- Dependencies: 1
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: -
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


SET default_with_oids = false;

--
-- TOC entry 216 (class 1259 OID 307896)
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
-- TOC entry 198 (class 1259 OID 307742)
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
-- TOC entry 199 (class 1259 OID 307750)
-- Name: AbpBackgroundJobs; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpBackgroundJobs" (
    "Id" uuid NOT NULL,
    "JobName" character varying(128) NOT NULL,
    "JobArgs" character varying(1048576) NOT NULL,
    "TryCount" smallint DEFAULT 0 NOT NULL,
    "CreationTime" timestamp without time zone NOT NULL,
    "NextTryTime" timestamp without time zone NOT NULL,
    "LastTryTime" timestamp without time zone,
    "IsAbandoned" boolean DEFAULT false NOT NULL,
    "Priority" smallint DEFAULT 15 NOT NULL,
    "ExtraProperties" text,
    "ConcurrencyStamp" character varying(40)
);


--
-- TOC entry 200 (class 1259 OID 307761)
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
-- TOC entry 217 (class 1259 OID 307909)
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
-- TOC entry 227 (class 1259 OID 308043)
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
-- TOC entry 201 (class 1259 OID 307769)
-- Name: AbpFeatureGroups; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpFeatureGroups" (
    "Id" uuid NOT NULL,
    "Name" character varying(128) NOT NULL,
    "DisplayName" character varying(256) NOT NULL,
    "ExtraProperties" text
);


--
-- TOC entry 203 (class 1259 OID 307785)
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
-- TOC entry 202 (class 1259 OID 307777)
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
-- TOC entry 204 (class 1259 OID 307790)
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
-- TOC entry 218 (class 1259 OID 307922)
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
-- TOC entry 205 (class 1259 OID 307795)
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
-- TOC entry 206 (class 1259 OID 307809)
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
-- TOC entry 207 (class 1259 OID 307814)
-- Name: AbpPermissionGroups; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpPermissionGroups" (
    "Id" uuid NOT NULL,
    "Name" character varying(128) NOT NULL,
    "DisplayName" character varying(256) NOT NULL,
    "ExtraProperties" text
);


--
-- TOC entry 208 (class 1259 OID 307822)
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
-- TOC entry 219 (class 1259 OID 307937)
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
-- TOC entry 209 (class 1259 OID 307830)
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
-- TOC entry 210 (class 1259 OID 307838)
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
-- TOC entry 211 (class 1259 OID 307846)
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
-- TOC entry 220 (class 1259 OID 307950)
-- Name: AbpTenantConnectionStrings; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpTenantConnectionStrings" (
    "TenantId" uuid NOT NULL,
    "Name" character varying(64) NOT NULL,
    "Value" character varying(1024) NOT NULL
);


--
-- TOC entry 212 (class 1259 OID 307854)
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
-- TOC entry 221 (class 1259 OID 307963)
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
-- TOC entry 222 (class 1259 OID 307976)
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
-- TOC entry 223 (class 1259 OID 307986)
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
-- TOC entry 224 (class 1259 OID 308001)
-- Name: AbpUserRoles; Type: TABLE; Schema: web; Owner: -
--

CREATE TABLE web."AbpUserRoles" (
    "UserId" uuid NOT NULL,
    "RoleId" uuid NOT NULL,
    "TenantId" uuid
);


--
-- TOC entry 225 (class 1259 OID 308016)
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
-- TOC entry 213 (class 1259 OID 307863)
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
-- TOC entry 214 (class 1259 OID 307878)
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
-- TOC entry 226 (class 1259 OID 308029)
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
-- TOC entry 215 (class 1259 OID 307887)
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
-- TOC entry 228 (class 1259 OID 308056)
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
-- TOC entry 3768 (class 2606 OID 307903)
-- Name: AbpAuditLogActions PK_AbpAuditLogActions; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpAuditLogActions"
    ADD CONSTRAINT "PK_AbpAuditLogActions" PRIMARY KEY ("Id");


--
-- TOC entry 3705 (class 2606 OID 307749)
-- Name: AbpAuditLogs PK_AbpAuditLogs; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpAuditLogs"
    ADD CONSTRAINT "PK_AbpAuditLogs" PRIMARY KEY ("Id");


--
-- TOC entry 3708 (class 2606 OID 307760)
-- Name: AbpBackgroundJobs PK_AbpBackgroundJobs; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpBackgroundJobs"
    ADD CONSTRAINT "PK_AbpBackgroundJobs" PRIMARY KEY ("Id");


--
-- TOC entry 3710 (class 2606 OID 307768)
-- Name: AbpClaimTypes PK_AbpClaimTypes; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpClaimTypes"
    ADD CONSTRAINT "PK_AbpClaimTypes" PRIMARY KEY ("Id");


--
-- TOC entry 3772 (class 2606 OID 307916)
-- Name: AbpEntityChanges PK_AbpEntityChanges; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpEntityChanges"
    ADD CONSTRAINT "PK_AbpEntityChanges" PRIMARY KEY ("Id");


--
-- TOC entry 3800 (class 2606 OID 308050)
-- Name: AbpEntityPropertyChanges PK_AbpEntityPropertyChanges; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpEntityPropertyChanges"
    ADD CONSTRAINT "PK_AbpEntityPropertyChanges" PRIMARY KEY ("Id");


--
-- TOC entry 3713 (class 2606 OID 307776)
-- Name: AbpFeatureGroups PK_AbpFeatureGroups; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpFeatureGroups"
    ADD CONSTRAINT "PK_AbpFeatureGroups" PRIMARY KEY ("Id");


--
-- TOC entry 3720 (class 2606 OID 307789)
-- Name: AbpFeatureValues PK_AbpFeatureValues; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpFeatureValues"
    ADD CONSTRAINT "PK_AbpFeatureValues" PRIMARY KEY ("Id");


--
-- TOC entry 3717 (class 2606 OID 307784)
-- Name: AbpFeatures PK_AbpFeatures; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpFeatures"
    ADD CONSTRAINT "PK_AbpFeatures" PRIMARY KEY ("Id");


--
-- TOC entry 3723 (class 2606 OID 307794)
-- Name: AbpLinkUsers PK_AbpLinkUsers; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpLinkUsers"
    ADD CONSTRAINT "PK_AbpLinkUsers" PRIMARY KEY ("Id");


--
-- TOC entry 3775 (class 2606 OID 307926)
-- Name: AbpOrganizationUnitRoles PK_AbpOrganizationUnitRoles; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpOrganizationUnitRoles"
    ADD CONSTRAINT "PK_AbpOrganizationUnitRoles" PRIMARY KEY ("OrganizationUnitId", "RoleId");


--
-- TOC entry 3727 (class 2606 OID 307803)
-- Name: AbpOrganizationUnits PK_AbpOrganizationUnits; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpOrganizationUnits"
    ADD CONSTRAINT "PK_AbpOrganizationUnits" PRIMARY KEY ("Id");


--
-- TOC entry 3730 (class 2606 OID 307813)
-- Name: AbpPermissionGrants PK_AbpPermissionGrants; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpPermissionGrants"
    ADD CONSTRAINT "PK_AbpPermissionGrants" PRIMARY KEY ("Id");


--
-- TOC entry 3733 (class 2606 OID 307821)
-- Name: AbpPermissionGroups PK_AbpPermissionGroups; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpPermissionGroups"
    ADD CONSTRAINT "PK_AbpPermissionGroups" PRIMARY KEY ("Id");


--
-- TOC entry 3737 (class 2606 OID 307829)
-- Name: AbpPermissions PK_AbpPermissions; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpPermissions"
    ADD CONSTRAINT "PK_AbpPermissions" PRIMARY KEY ("Id");


--
-- TOC entry 3778 (class 2606 OID 307944)
-- Name: AbpRoleClaims PK_AbpRoleClaims; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpRoleClaims"
    ADD CONSTRAINT "PK_AbpRoleClaims" PRIMARY KEY ("Id");


--
-- TOC entry 3740 (class 2606 OID 307837)
-- Name: AbpRoles PK_AbpRoles; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpRoles"
    ADD CONSTRAINT "PK_AbpRoles" PRIMARY KEY ("Id");


--
-- TOC entry 3746 (class 2606 OID 307845)
-- Name: AbpSecurityLogs PK_AbpSecurityLogs; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpSecurityLogs"
    ADD CONSTRAINT "PK_AbpSecurityLogs" PRIMARY KEY ("Id");


--
-- TOC entry 3749 (class 2606 OID 307853)
-- Name: AbpSettings PK_AbpSettings; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpSettings"
    ADD CONSTRAINT "PK_AbpSettings" PRIMARY KEY ("Id");


--
-- TOC entry 3780 (class 2606 OID 307957)
-- Name: AbpTenantConnectionStrings PK_AbpTenantConnectionStrings; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpTenantConnectionStrings"
    ADD CONSTRAINT "PK_AbpTenantConnectionStrings" PRIMARY KEY ("TenantId", "Name");


--
-- TOC entry 3752 (class 2606 OID 307862)
-- Name: AbpTenants PK_AbpTenants; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpTenants"
    ADD CONSTRAINT "PK_AbpTenants" PRIMARY KEY ("Id");


--
-- TOC entry 3783 (class 2606 OID 307970)
-- Name: AbpUserClaims PK_AbpUserClaims; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpUserClaims"
    ADD CONSTRAINT "PK_AbpUserClaims" PRIMARY KEY ("Id");


--
-- TOC entry 3786 (class 2606 OID 307980)
-- Name: AbpUserLogins PK_AbpUserLogins; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpUserLogins"
    ADD CONSTRAINT "PK_AbpUserLogins" PRIMARY KEY ("UserId", "LoginProvider");


--
-- TOC entry 3789 (class 2606 OID 307990)
-- Name: AbpUserOrganizationUnits PK_AbpUserOrganizationUnits; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpUserOrganizationUnits"
    ADD CONSTRAINT "PK_AbpUserOrganizationUnits" PRIMARY KEY ("OrganizationUnitId", "UserId");


--
-- TOC entry 3792 (class 2606 OID 308005)
-- Name: AbpUserRoles PK_AbpUserRoles; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpUserRoles"
    ADD CONSTRAINT "PK_AbpUserRoles" PRIMARY KEY ("UserId", "RoleId");


--
-- TOC entry 3794 (class 2606 OID 308023)
-- Name: AbpUserTokens PK_AbpUserTokens; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpUserTokens"
    ADD CONSTRAINT "PK_AbpUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name");


--
-- TOC entry 3758 (class 2606 OID 307877)
-- Name: AbpUsers PK_AbpUsers; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpUsers"
    ADD CONSTRAINT "PK_AbpUsers" PRIMARY KEY ("Id");


--
-- TOC entry 3761 (class 2606 OID 307886)
-- Name: OpenIddictApplications PK_OpenIddictApplications; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."OpenIddictApplications"
    ADD CONSTRAINT "PK_OpenIddictApplications" PRIMARY KEY ("Id");


--
-- TOC entry 3797 (class 2606 OID 308037)
-- Name: OpenIddictAuthorizations PK_OpenIddictAuthorizations; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."OpenIddictAuthorizations"
    ADD CONSTRAINT "PK_OpenIddictAuthorizations" PRIMARY KEY ("Id");


--
-- TOC entry 3764 (class 2606 OID 307895)
-- Name: OpenIddictScopes PK_OpenIddictScopes; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."OpenIddictScopes"
    ADD CONSTRAINT "PK_OpenIddictScopes" PRIMARY KEY ("Id");


--
-- TOC entry 3805 (class 2606 OID 308064)
-- Name: OpenIddictTokens PK_OpenIddictTokens; Type: CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."OpenIddictTokens"
    ADD CONSTRAINT "PK_OpenIddictTokens" PRIMARY KEY ("Id");

--
-- TOC entry 3765 (class 1259 OID 308075)
-- Name: IX_AbpAuditLogActions_AuditLogId; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpAuditLogActions_AuditLogId" ON web."AbpAuditLogActions" USING btree ("AuditLogId");


--
-- TOC entry 3766 (class 1259 OID 308076)
-- Name: IX_AbpAuditLogActions_TenantId_ServiceName_MethodName_Executio~; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpAuditLogActions_TenantId_ServiceName_MethodName_Executio~" ON web."AbpAuditLogActions" USING btree ("TenantId", "ServiceName", "MethodName", "ExecutionTime");


--
-- TOC entry 3702 (class 1259 OID 308077)
-- Name: IX_AbpAuditLogs_TenantId_ExecutionTime; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpAuditLogs_TenantId_ExecutionTime" ON web."AbpAuditLogs" USING btree ("TenantId", "ExecutionTime");


--
-- TOC entry 3703 (class 1259 OID 308078)
-- Name: IX_AbpAuditLogs_TenantId_UserId_ExecutionTime; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpAuditLogs_TenantId_UserId_ExecutionTime" ON web."AbpAuditLogs" USING btree ("TenantId", "UserId", "ExecutionTime");


--
-- TOC entry 3706 (class 1259 OID 308079)
-- Name: IX_AbpBackgroundJobs_IsAbandoned_NextTryTime; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpBackgroundJobs_IsAbandoned_NextTryTime" ON web."AbpBackgroundJobs" USING btree ("IsAbandoned", "NextTryTime");


--
-- TOC entry 3769 (class 1259 OID 308080)
-- Name: IX_AbpEntityChanges_AuditLogId; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpEntityChanges_AuditLogId" ON web."AbpEntityChanges" USING btree ("AuditLogId");


--
-- TOC entry 3770 (class 1259 OID 308081)
-- Name: IX_AbpEntityChanges_TenantId_EntityTypeFullName_EntityId; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpEntityChanges_TenantId_EntityTypeFullName_EntityId" ON web."AbpEntityChanges" USING btree ("TenantId", "EntityTypeFullName", "EntityId");


--
-- TOC entry 3798 (class 1259 OID 308082)
-- Name: IX_AbpEntityPropertyChanges_EntityChangeId; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpEntityPropertyChanges_EntityChangeId" ON web."AbpEntityPropertyChanges" USING btree ("EntityChangeId");


--
-- TOC entry 3711 (class 1259 OID 308083)
-- Name: IX_AbpFeatureGroups_Name; Type: INDEX; Schema: web; Owner: -
--

CREATE UNIQUE INDEX "IX_AbpFeatureGroups_Name" ON web."AbpFeatureGroups" USING btree ("Name");


--
-- TOC entry 3718 (class 1259 OID 308086)
-- Name: IX_AbpFeatureValues_Name_ProviderName_ProviderKey; Type: INDEX; Schema: web; Owner: -
--

CREATE UNIQUE INDEX "IX_AbpFeatureValues_Name_ProviderName_ProviderKey" ON web."AbpFeatureValues" USING btree ("Name", "ProviderName", "ProviderKey");


--
-- TOC entry 3714 (class 1259 OID 308084)
-- Name: IX_AbpFeatures_GroupName; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpFeatures_GroupName" ON web."AbpFeatures" USING btree ("GroupName");


--
-- TOC entry 3715 (class 1259 OID 308085)
-- Name: IX_AbpFeatures_Name; Type: INDEX; Schema: web; Owner: -
--

CREATE UNIQUE INDEX "IX_AbpFeatures_Name" ON web."AbpFeatures" USING btree ("Name");


--
-- TOC entry 3721 (class 1259 OID 308087)
-- Name: IX_AbpLinkUsers_SourceUserId_SourceTenantId_TargetUserId_Targe~; Type: INDEX; Schema: web; Owner: -
--

CREATE UNIQUE INDEX "IX_AbpLinkUsers_SourceUserId_SourceTenantId_TargetUserId_Targe~" ON web."AbpLinkUsers" USING btree ("SourceUserId", "SourceTenantId", "TargetUserId", "TargetTenantId");


--
-- TOC entry 3773 (class 1259 OID 308088)
-- Name: IX_AbpOrganizationUnitRoles_RoleId_OrganizationUnitId; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpOrganizationUnitRoles_RoleId_OrganizationUnitId" ON web."AbpOrganizationUnitRoles" USING btree ("RoleId", "OrganizationUnitId");


--
-- TOC entry 3724 (class 1259 OID 308089)
-- Name: IX_AbpOrganizationUnits_Code; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpOrganizationUnits_Code" ON web."AbpOrganizationUnits" USING btree ("Code");


--
-- TOC entry 3725 (class 1259 OID 308090)
-- Name: IX_AbpOrganizationUnits_ParentId; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpOrganizationUnits_ParentId" ON web."AbpOrganizationUnits" USING btree ("ParentId");


--
-- TOC entry 3728 (class 1259 OID 308091)
-- Name: IX_AbpPermissionGrants_TenantId_Name_ProviderName_ProviderKey; Type: INDEX; Schema: web; Owner: -
--

CREATE UNIQUE INDEX "IX_AbpPermissionGrants_TenantId_Name_ProviderName_ProviderKey" ON web."AbpPermissionGrants" USING btree ("TenantId", "Name", "ProviderName", "ProviderKey");


--
-- TOC entry 3731 (class 1259 OID 308092)
-- Name: IX_AbpPermissionGroups_Name; Type: INDEX; Schema: web; Owner: -
--

CREATE UNIQUE INDEX "IX_AbpPermissionGroups_Name" ON web."AbpPermissionGroups" USING btree ("Name");


--
-- TOC entry 3734 (class 1259 OID 308093)
-- Name: IX_AbpPermissions_GroupName; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpPermissions_GroupName" ON web."AbpPermissions" USING btree ("GroupName");


--
-- TOC entry 3735 (class 1259 OID 308094)
-- Name: IX_AbpPermissions_Name; Type: INDEX; Schema: web; Owner: -
--

CREATE UNIQUE INDEX "IX_AbpPermissions_Name" ON web."AbpPermissions" USING btree ("Name");


--
-- TOC entry 3776 (class 1259 OID 308095)
-- Name: IX_AbpRoleClaims_RoleId; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpRoleClaims_RoleId" ON web."AbpRoleClaims" USING btree ("RoleId");


--
-- TOC entry 3738 (class 1259 OID 308096)
-- Name: IX_AbpRoles_NormalizedName; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpRoles_NormalizedName" ON web."AbpRoles" USING btree ("NormalizedName");


--
-- TOC entry 3741 (class 1259 OID 308097)
-- Name: IX_AbpSecurityLogs_TenantId_Action; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpSecurityLogs_TenantId_Action" ON web."AbpSecurityLogs" USING btree ("TenantId", "Action");


--
-- TOC entry 3742 (class 1259 OID 308098)
-- Name: IX_AbpSecurityLogs_TenantId_ApplicationName; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpSecurityLogs_TenantId_ApplicationName" ON web."AbpSecurityLogs" USING btree ("TenantId", "ApplicationName");


--
-- TOC entry 3743 (class 1259 OID 308099)
-- Name: IX_AbpSecurityLogs_TenantId_Identity; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpSecurityLogs_TenantId_Identity" ON web."AbpSecurityLogs" USING btree ("TenantId", "Identity");


--
-- TOC entry 3744 (class 1259 OID 308100)
-- Name: IX_AbpSecurityLogs_TenantId_UserId; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpSecurityLogs_TenantId_UserId" ON web."AbpSecurityLogs" USING btree ("TenantId", "UserId");


--
-- TOC entry 3747 (class 1259 OID 308101)
-- Name: IX_AbpSettings_Name_ProviderName_ProviderKey; Type: INDEX; Schema: web; Owner: -
--

CREATE UNIQUE INDEX "IX_AbpSettings_Name_ProviderName_ProviderKey" ON web."AbpSettings" USING btree ("Name", "ProviderName", "ProviderKey");


--
-- TOC entry 3750 (class 1259 OID 308102)
-- Name: IX_AbpTenants_Name; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpTenants_Name" ON web."AbpTenants" USING btree ("Name");


--
-- TOC entry 3781 (class 1259 OID 308103)
-- Name: IX_AbpUserClaims_UserId; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpUserClaims_UserId" ON web."AbpUserClaims" USING btree ("UserId");


--
-- TOC entry 3784 (class 1259 OID 308104)
-- Name: IX_AbpUserLogins_LoginProvider_ProviderKey; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpUserLogins_LoginProvider_ProviderKey" ON web."AbpUserLogins" USING btree ("LoginProvider", "ProviderKey");


--
-- TOC entry 3787 (class 1259 OID 308105)
-- Name: IX_AbpUserOrganizationUnits_UserId_OrganizationUnitId; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpUserOrganizationUnits_UserId_OrganizationUnitId" ON web."AbpUserOrganizationUnits" USING btree ("UserId", "OrganizationUnitId");


--
-- TOC entry 3790 (class 1259 OID 308106)
-- Name: IX_AbpUserRoles_RoleId_UserId; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpUserRoles_RoleId_UserId" ON web."AbpUserRoles" USING btree ("RoleId", "UserId");


--
-- TOC entry 3753 (class 1259 OID 308107)
-- Name: IX_AbpUsers_Email; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpUsers_Email" ON web."AbpUsers" USING btree ("Email");


--
-- TOC entry 3754 (class 1259 OID 308108)
-- Name: IX_AbpUsers_NormalizedEmail; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpUsers_NormalizedEmail" ON web."AbpUsers" USING btree ("NormalizedEmail");


--
-- TOC entry 3755 (class 1259 OID 308109)
-- Name: IX_AbpUsers_NormalizedUserName; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpUsers_NormalizedUserName" ON web."AbpUsers" USING btree ("NormalizedUserName");


--
-- TOC entry 3756 (class 1259 OID 308110)
-- Name: IX_AbpUsers_UserName; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_AbpUsers_UserName" ON web."AbpUsers" USING btree ("UserName");


--
-- TOC entry 3759 (class 1259 OID 308111)
-- Name: IX_OpenIddictApplications_ClientId; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_OpenIddictApplications_ClientId" ON web."OpenIddictApplications" USING btree ("ClientId");


--
-- TOC entry 3795 (class 1259 OID 308112)
-- Name: IX_OpenIddictAuthorizations_ApplicationId_Status_Subject_Type; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_OpenIddictAuthorizations_ApplicationId_Status_Subject_Type" ON web."OpenIddictAuthorizations" USING btree ("ApplicationId", "Status", "Subject", "Type");


--
-- TOC entry 3762 (class 1259 OID 308113)
-- Name: IX_OpenIddictScopes_Name; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_OpenIddictScopes_Name" ON web."OpenIddictScopes" USING btree ("Name");


--
-- TOC entry 3801 (class 1259 OID 308114)
-- Name: IX_OpenIddictTokens_ApplicationId_Status_Subject_Type; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_OpenIddictTokens_ApplicationId_Status_Subject_Type" ON web."OpenIddictTokens" USING btree ("ApplicationId", "Status", "Subject", "Type");


--
-- TOC entry 3802 (class 1259 OID 308115)
-- Name: IX_OpenIddictTokens_AuthorizationId; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_OpenIddictTokens_AuthorizationId" ON web."OpenIddictTokens" USING btree ("AuthorizationId");


--
-- TOC entry 3803 (class 1259 OID 308116)
-- Name: IX_OpenIddictTokens_ReferenceId; Type: INDEX; Schema: web; Owner: -
--

CREATE INDEX "IX_OpenIddictTokens_ReferenceId" ON web."OpenIddictTokens" USING btree ("ReferenceId");


--
-- TOC entry 3807 (class 2606 OID 307904)
-- Name: AbpAuditLogActions FK_AbpAuditLogActions_AbpAuditLogs_AuditLogId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpAuditLogActions"
    ADD CONSTRAINT "FK_AbpAuditLogActions_AbpAuditLogs_AuditLogId" FOREIGN KEY ("AuditLogId") REFERENCES web."AbpAuditLogs"("Id") ON DELETE CASCADE;


--
-- TOC entry 3808 (class 2606 OID 307917)
-- Name: AbpEntityChanges FK_AbpEntityChanges_AbpAuditLogs_AuditLogId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpEntityChanges"
    ADD CONSTRAINT "FK_AbpEntityChanges_AbpAuditLogs_AuditLogId" FOREIGN KEY ("AuditLogId") REFERENCES web."AbpAuditLogs"("Id") ON DELETE CASCADE;


--
-- TOC entry 3821 (class 2606 OID 308051)
-- Name: AbpEntityPropertyChanges FK_AbpEntityPropertyChanges_AbpEntityChanges_EntityChangeId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpEntityPropertyChanges"
    ADD CONSTRAINT "FK_AbpEntityPropertyChanges_AbpEntityChanges_EntityChangeId" FOREIGN KEY ("EntityChangeId") REFERENCES web."AbpEntityChanges"("Id") ON DELETE CASCADE;


--
-- TOC entry 3809 (class 2606 OID 307927)
-- Name: AbpOrganizationUnitRoles FK_AbpOrganizationUnitRoles_AbpOrganizationUnits_OrganizationU~; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpOrganizationUnitRoles"
    ADD CONSTRAINT "FK_AbpOrganizationUnitRoles_AbpOrganizationUnits_OrganizationU~" FOREIGN KEY ("OrganizationUnitId") REFERENCES web."AbpOrganizationUnits"("Id") ON DELETE CASCADE;


--
-- TOC entry 3810 (class 2606 OID 307932)
-- Name: AbpOrganizationUnitRoles FK_AbpOrganizationUnitRoles_AbpRoles_RoleId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpOrganizationUnitRoles"
    ADD CONSTRAINT "FK_AbpOrganizationUnitRoles_AbpRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES web."AbpRoles"("Id") ON DELETE CASCADE;


--
-- TOC entry 3806 (class 2606 OID 307804)
-- Name: AbpOrganizationUnits FK_AbpOrganizationUnits_AbpOrganizationUnits_ParentId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpOrganizationUnits"
    ADD CONSTRAINT "FK_AbpOrganizationUnits_AbpOrganizationUnits_ParentId" FOREIGN KEY ("ParentId") REFERENCES web."AbpOrganizationUnits"("Id");


--
-- TOC entry 3811 (class 2606 OID 307945)
-- Name: AbpRoleClaims FK_AbpRoleClaims_AbpRoles_RoleId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpRoleClaims"
    ADD CONSTRAINT "FK_AbpRoleClaims_AbpRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES web."AbpRoles"("Id") ON DELETE CASCADE;


--
-- TOC entry 3812 (class 2606 OID 307958)
-- Name: AbpTenantConnectionStrings FK_AbpTenantConnectionStrings_AbpTenants_TenantId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpTenantConnectionStrings"
    ADD CONSTRAINT "FK_AbpTenantConnectionStrings_AbpTenants_TenantId" FOREIGN KEY ("TenantId") REFERENCES web."AbpTenants"("Id") ON DELETE CASCADE;


--
-- TOC entry 3813 (class 2606 OID 307971)
-- Name: AbpUserClaims FK_AbpUserClaims_AbpUsers_UserId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpUserClaims"
    ADD CONSTRAINT "FK_AbpUserClaims_AbpUsers_UserId" FOREIGN KEY ("UserId") REFERENCES web."AbpUsers"("Id") ON DELETE CASCADE;


--
-- TOC entry 3814 (class 2606 OID 307981)
-- Name: AbpUserLogins FK_AbpUserLogins_AbpUsers_UserId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpUserLogins"
    ADD CONSTRAINT "FK_AbpUserLogins_AbpUsers_UserId" FOREIGN KEY ("UserId") REFERENCES web."AbpUsers"("Id") ON DELETE CASCADE;


--
-- TOC entry 3815 (class 2606 OID 307991)
-- Name: AbpUserOrganizationUnits FK_AbpUserOrganizationUnits_AbpOrganizationUnits_OrganizationU~; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpUserOrganizationUnits"
    ADD CONSTRAINT "FK_AbpUserOrganizationUnits_AbpOrganizationUnits_OrganizationU~" FOREIGN KEY ("OrganizationUnitId") REFERENCES web."AbpOrganizationUnits"("Id") ON DELETE CASCADE;


--
-- TOC entry 3816 (class 2606 OID 307996)
-- Name: AbpUserOrganizationUnits FK_AbpUserOrganizationUnits_AbpUsers_UserId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpUserOrganizationUnits"
    ADD CONSTRAINT "FK_AbpUserOrganizationUnits_AbpUsers_UserId" FOREIGN KEY ("UserId") REFERENCES web."AbpUsers"("Id") ON DELETE CASCADE;


--
-- TOC entry 3817 (class 2606 OID 308006)
-- Name: AbpUserRoles FK_AbpUserRoles_AbpRoles_RoleId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpUserRoles"
    ADD CONSTRAINT "FK_AbpUserRoles_AbpRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES web."AbpRoles"("Id") ON DELETE CASCADE;


--
-- TOC entry 3818 (class 2606 OID 308011)
-- Name: AbpUserRoles FK_AbpUserRoles_AbpUsers_UserId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpUserRoles"
    ADD CONSTRAINT "FK_AbpUserRoles_AbpUsers_UserId" FOREIGN KEY ("UserId") REFERENCES web."AbpUsers"("Id") ON DELETE CASCADE;


--
-- TOC entry 3819 (class 2606 OID 308024)
-- Name: AbpUserTokens FK_AbpUserTokens_AbpUsers_UserId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."AbpUserTokens"
    ADD CONSTRAINT "FK_AbpUserTokens_AbpUsers_UserId" FOREIGN KEY ("UserId") REFERENCES web."AbpUsers"("Id") ON DELETE CASCADE;


--
-- TOC entry 3820 (class 2606 OID 308038)
-- Name: OpenIddictAuthorizations FK_OpenIddictAuthorizations_OpenIddictApplications_Application~; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."OpenIddictAuthorizations"
    ADD CONSTRAINT "FK_OpenIddictAuthorizations_OpenIddictApplications_Application~" FOREIGN KEY ("ApplicationId") REFERENCES web."OpenIddictApplications"("Id");


--
-- TOC entry 3822 (class 2606 OID 308065)
-- Name: OpenIddictTokens FK_OpenIddictTokens_OpenIddictApplications_ApplicationId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."OpenIddictTokens"
    ADD CONSTRAINT "FK_OpenIddictTokens_OpenIddictApplications_ApplicationId" FOREIGN KEY ("ApplicationId") REFERENCES web."OpenIddictApplications"("Id");


--
-- TOC entry 3823 (class 2606 OID 308070)
-- Name: OpenIddictTokens FK_OpenIddictTokens_OpenIddictAuthorizations_AuthorizationId; Type: FK CONSTRAINT; Schema: web; Owner: -
--

ALTER TABLE ONLY web."OpenIddictTokens"
    ADD CONSTRAINT "FK_OpenIddictTokens_OpenIddictAuthorizations_AuthorizationId" FOREIGN KEY ("AuthorizationId") REFERENCES web."OpenIddictAuthorizations"("Id");


-- Completed on 2023-01-24 19:49:59 -05

--
-- PostgreSQL database dump complete
--

