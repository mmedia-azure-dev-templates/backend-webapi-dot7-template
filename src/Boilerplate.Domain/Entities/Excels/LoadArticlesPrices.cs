using Boilerplate.Domain.Entities.Enums;

namespace Boilerplate.Domain.Entities.Excels;
public class LoadArticlesPrices
{
}

public class HeaderArticlesPrices
{
    public string Sku { get; set; }
    public string Display { get; set; }
    public PaymentMethodsType? DirectCredit { get; set; } = null;
    public PaymentMethodsType? Fcme { get; set; } = null;
}


