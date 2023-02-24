using Boilerplate.Domain.Entities.Common;

namespace Boilerplate.Domain.Implementations;

public interface IAuthenticateResponse
{
    public SweetAlert SweetAlert { get; set; }
    public bool Transaction { get; set; }
}
