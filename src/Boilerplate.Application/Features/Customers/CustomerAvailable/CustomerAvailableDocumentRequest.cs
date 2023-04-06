using Boilerplate.Domain.Entities.Enums;
using MediatR;

namespace Boilerplate.Application.Features.Customers.CustomerAvailable;
public class CustomerAvailableDocumentRequest : IRequest<CustomerAvailableDocumentResponse>
{
    public IdentificationType DocumentType { get; set; }
    public string Ndocument { get; set; }
}