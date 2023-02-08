namespace Boilerplate.Domain.Entities;

public class EmailButtonModel
{
    public EmailButtonModel(string text, string url)
    {
        Text = text;
        Url = url;
    }

    public string Text { get; set; }
    public string Url { get; set; }
}
