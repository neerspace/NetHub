using Mapster;
using NetHub.Shared.Models.Currency;

namespace NetHub.Mappings;

public class CurrencyRateMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<OneExchangeResponseModel, OneExchangeModel>()
            .Map(od => od.CurrencyFrom, or => or.CurrencyCodeA)
            .Map(od => od.CurrencyTo, or => or.CurrencyCodeB);
    }
}