using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddIndexesPaymentMethods : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql(@"CREATE UNIQUE INDEX [DataKeyPaymentMethodsTypeIndex]
        ON web.PaymentMethods (DataKey, PaymentMethodsType)
        WHERE PaymentMethodsType IS NOT NULL ;");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "DataKeyPaymentMethodsTypeIndex",
            schema: "web",
            table: "PaymentMethods"
        );
    }
}
