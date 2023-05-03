namespace Boilerplate.Application.Features.Orders.OrderPdf;
public class OrderPdfResponse
{
    public bool IsValid { get; set; } = false;
    public string DocumentUrl { get; set; }
}
