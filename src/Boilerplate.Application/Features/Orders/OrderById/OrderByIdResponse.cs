using Boilerplate.Application.Features.Articles.ArticleSearch;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using System;
using System.Collections.Generic;

namespace Boilerplate.Application.Features.Orders.OrderById;
public class OrderByIdResponse
{
    public Order Order { get; set; }
    public List<ArticleSearchResponse> ArticleSearchResponse { get; set; }
    public ApplicationUser UserGeneratedApplicationUser { get; set; }
    public UserInformation UserGeneratedInformationUser { get; set; }
    public ApplicationUser UserAssignedApplicationUser { get; set; }
    public UserInformation UserAssignedUserInformation { get; set; }
    public Customer Customer { get; set; }
}
