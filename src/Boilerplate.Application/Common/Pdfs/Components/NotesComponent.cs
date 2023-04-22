using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Boilerplate.Application.Common.Pdfs.Components;
public class NotesComponent : IComponent
{
    private string? Notes { get; } = null;
    public NotesComponent(string? notes)
    {
        Notes = notes;
    }
    public void Compose(IContainer container)
    {
        container.Background(Colors.Grey.Lighten3).Padding(10).Column(column =>
        {
            column.Spacing(5);
            column.Item().Text("Notas: ").Bold().FontSize(14);
            if(Notes != null) { 
                column.Item().Text(Notes).FontSize(12);
            }
            if(Notes == null)
            {
                column.Item().Text(text =>
                {
                    text.Span("_________________________________________________________________________________________________________________").Thin();
                });
                column.Item().Text(text =>
                {
                    text.Span("_________________________________________________________________________________________________________________").Thin();
                });
            }
        });
    }
}
