namespace Boilerplate.Domain.Entities.Emails;

public class ForgotPasswordMailData
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
}
