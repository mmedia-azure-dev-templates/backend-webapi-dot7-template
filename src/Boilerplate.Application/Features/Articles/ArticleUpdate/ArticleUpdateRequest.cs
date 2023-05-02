using MediatR;

namespace Boilerplate.Application.Features.Articles.ArticleUpdate;
public class ArticleUpdateRequest : IRequest<ArticleUpdateResponse>
{
    public int Provider { get; set; }

    public string Sku { get; set; }

    public string Abrevia { get; set; }

    public string Display { get; set; }

    public decimal Cost { get; set; }

    public int Brand { get; set; }

    public string? Notes { get; set; }

    public string? Meta { get; set; }

    public bool Discontinued { get; set; }
}
