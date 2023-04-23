using Boilerplate.Domain.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Articles.ArticleCreate;
public class ArticleCreateResponse:IArticleCreateResponse
{
    public string? Message { get; set; } 
}