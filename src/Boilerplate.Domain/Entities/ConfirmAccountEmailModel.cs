namespace Boilerplate.Domain.Entities;

public class ConfirmAccountEmailModel
{
    public ConfirmAccountEmailModel(string confirmEmailUrl)
    {
        ConfirmEmailUrl = confirmEmailUrl;
    }

    public string ConfirmEmailUrl { get; set; }
}
