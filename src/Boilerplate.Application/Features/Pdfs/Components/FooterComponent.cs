using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Boilerplate.Application.Features.Pdfs.Components;
public class FooterComponent : IComponent
{
    private string Title { get; }
    private Address Address { get; }

    public FooterComponent(string title, Address address)
    {
        Title = title;
        Address = address;
    }

    public void Compose(IContainer container)
    {
        container.Column(column =>
        {
            column.Spacing(2);

            column.Item().BorderBottom(1).PaddingBottom(5).Text(Title).SemiBold();

            column.Item().Text(Address.CompanyName);
            column.Item().Text(Address.Street);
            column.Item().Text($"{Address.City}, {Address.State}");
            column.Item().Text(Address.Email);
            column.Item().Text(Address.Phone);
        });
    }
}