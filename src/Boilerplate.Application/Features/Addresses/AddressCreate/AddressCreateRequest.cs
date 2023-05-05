using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.Addresses.AddressCreate;
public class AddressCreateRequest : IRequest<AddressCreateResponse>
{
    public PersonId PersonId { get; set; }
    public string? PrimaryStreet { get; set; }
    public string? SecondaryStreet { get; set; }
    public string? Numeration { get; set; }
    public string? Reference { get; set; }
    public int? Provincia { get; set; }
    public int? Canton { get; set; }
    public int? Parroquia { get; set; }
    public string? Notes { get; set; }
}
