using Boilerplate.Application.Features.Articles.ArticleSearch;
using Boilerplate.Application.Features.Users.GetUsers;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using System;
using System.Collections.Generic;

namespace Boilerplate.Application.Features.Orders.OrderPdf;
public class OrderPdfResponse
{
    public bool IsValid { get; set; } = false;
    public string DocumentUrl { get; set; }
}
