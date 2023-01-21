using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boilerplate.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddDataUsers : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("\r\ninsert into web.\"Users\"\r\n(\r\n\t\"Id\",\r\n\t\"Nombres\",\r\n\t\"Apellidos\",\r\n\t\"Username\",\r\n\t\"Password\",\r\n\t\"Role\",\r\n\t\"RememberToken\",\r\n\t\"Email\",\r\n\t\"IsActive\",\r\n\t\"LastLogin\",\r\n\t\"LastLoginIp\",\r\n\t\"CreatedAt\",\r\n\t\"UpdatedAt\",\r\n\t\"DeletedAt\"\r\n)\r\nselect \r\n    \"id\",\r\n\t\"nombres\",\r\n\t\"apellidos\",\r\n\t\"username\",\r\n\t\"password\",\r\n    'User',\r\n\t\"remember_token\",\r\n\t\"email\",\r\n\t\"is_active\",\r\n\t\"last_login\",\r\n\t\"last_login_ip\",\r\n\t\"created_at\",\r\n\t\"updated_at\",\r\n\t\"deleted_at\"\r\n    FROM  public.users;\r\n\r\nupdate web.\"Users\" set \"Role\" = 'Admin' where \"Id\" = 1");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {

    }
}
