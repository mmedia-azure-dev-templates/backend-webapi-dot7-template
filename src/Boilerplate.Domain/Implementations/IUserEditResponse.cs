using Boilerplate.Domain.Entities.Common;

namespace Boilerplate.Domain.Implementations;
public interface IUserEditResponse
{
    public SweetAlert SweetAlert { get; set; }
    public bool Transaction { get; set; }
}
