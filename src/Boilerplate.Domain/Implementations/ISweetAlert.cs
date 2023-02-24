using Boilerplate.Domain.Entities.Enums;

namespace Boilerplate.Domain.Implementations;
public interface ISweetAlert
{
    public string Text { get; set; }
    public string Title { get; set; }
    public SweetAlertIconType Icon { get; set; }
}
