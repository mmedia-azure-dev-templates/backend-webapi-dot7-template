using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;
using System;

namespace Boilerplate.Application.Features.ArticlesItems.ArticleItemCreateUpdateBySku;
public class ArticleItemUpdateBySkuResponse : IAddressUpdateResponse
{
    public Article? Article { get; set; }
    public ArticleItem? ArticleItem { get; set; }
}