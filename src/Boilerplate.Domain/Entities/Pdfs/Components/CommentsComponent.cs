using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Boilerplate.Domain.Entities.Pdfs.Components;
public class CommentsComponent : IComponent
{
    private string Comments { get; }
    public CommentsComponent(string comments)
    {
        Comments = comments;
    }
    public void Compose(IContainer container)
    {
        container.Background(Colors.Grey.Lighten3).Padding(10).Column(column =>
        {
            column.Spacing(5);
            column.Item().Text("Comments").FontSize(14);
            column.Item().Text(Comments);
        });
    }
}
