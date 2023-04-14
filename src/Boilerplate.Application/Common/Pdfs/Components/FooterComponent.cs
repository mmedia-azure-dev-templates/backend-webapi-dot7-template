using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Boilerplate.Application.Common.Pdfs.Components;
public class FooterComponent : IComponent
{

    public FooterComponent()
    {
    }

    public void Compose(IContainer container)
    {
        container.Column(column =>
        {
            column.Item().AlignCenter().Text(x =>
             {
                 x.CurrentPageNumber();
                 x.Span(" / ");
                 x.TotalPages();
             });
        });
    }
}