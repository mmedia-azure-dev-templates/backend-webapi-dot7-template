using Boilerplate.Application.Features.Users;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Articles.ArticleCreate;
public class ArticleCreateRequest : IRequest<ArticleCreateResponse>
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
