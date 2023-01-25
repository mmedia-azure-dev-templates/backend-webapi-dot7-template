using System;
using Boilerplate.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AbpTables : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(Utils.GetRawSql("20220124212104_AbpTablesUp.sql"));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "AbpAuditLogActions", schema: "web");

        migrationBuilder.DropTable(
            name: "AbpBackgroundJobs", schema: "web");

        migrationBuilder.DropTable(
            name: "AbpClaimTypes", schema: "web");

        migrationBuilder.DropTable(
            name: "AbpEntityPropertyChanges", schema: "web");

        migrationBuilder.DropTable(
            name: "AbpFeatureGroups", schema: "web");

        migrationBuilder.DropTable(
            name: "AbpFeatures", schema: "web");

        migrationBuilder.DropTable(
            name: "AbpFeatureValues", schema: "web");

        migrationBuilder.DropTable(
            name: "AbpLinkUsers", schema: "web");

        migrationBuilder.DropTable(
            name: "AbpOrganizationUnitRoles", schema: "web");

        migrationBuilder.DropTable(
            name: "AbpPermissionGrants", schema: "web");

        migrationBuilder.DropTable(
            name: "AbpPermissionGroups", schema: "web");

        migrationBuilder.DropTable(
            name: "AbpPermissions", schema: "web");

        migrationBuilder.DropTable(
            name: "AbpRoleClaims", schema: "web");

        migrationBuilder.DropTable(
            name: "AbpSecurityLogs", schema: "web");

        migrationBuilder.DropTable(
            name: "AbpSettings", schema: "web");

        migrationBuilder.DropTable(
            name: "AbpTenantConnectionStrings", schema: "web");

        migrationBuilder.DropTable(
            name: "AbpUserClaims", schema: "web");

        migrationBuilder.DropTable(
            name: "AbpUserLogins", schema: "web");

        migrationBuilder.DropTable(
            name: "AbpUserOrganizationUnits", schema: "web");

        migrationBuilder.DropTable(
            name: "AbpUserRoles", schema: "web");

        migrationBuilder.DropTable(
            name: "AbpUserTokens", schema: "web");

        migrationBuilder.DropTable(
            name: "OpenIddictScopes", schema: "web");

        migrationBuilder.DropTable(
            name: "OpenIddictTokens", schema: "web");

        migrationBuilder.DropTable(
            name: "AbpEntityChanges", schema: "web");

        migrationBuilder.DropTable(
            name: "AbpTenants", schema: "web");

        migrationBuilder.DropTable(
            name: "AbpOrganizationUnits", schema: "web");

        migrationBuilder.DropTable(
            name: "AbpRoles", schema: "web");

        migrationBuilder.DropTable(
            name: "AbpUsers", schema: "web");

        migrationBuilder.DropTable(
            name: "OpenIddictAuthorizations", schema: "web");

        migrationBuilder.DropTable(
            name: "AbpAuditLogs", schema: "web");

        migrationBuilder.DropTable(
            name: "OpenIddictApplications", schema: "web");
    }
}
