using NetHub.Admin.Api.Abstractions;
using NetHub.Application.Interfaces;
using NetHub.Application.Models.Currency;

namespace NetHub.Api.Endpoints.CurrencyRates;

public class GetCurrencyRatesEndpoint : ResultEndpoint<CurrenciesResponse>
{
    private readonly ICryptoRateService _cryptoRateService;
    private readonly IExchangeRateService _exchangeRateService;

    public GetCurrencyRatesEndpoint(ICryptoRateService cryptoRateService, IExchangeRateService exchangeRateService)
    {
        _cryptoRateService = cryptoRateService;
        _exchangeRateService = exchangeRateService;
    }


    public override async Task<CurrenciesResponse> HandleAsync(CancellationToken ct = default)
    {
        return new CurrenciesResponse
        {
            Exchanges = await _exchangeRateService.GetExchangeCurrenciesAsync(ct),
            Crypto = await _cryptoRateService.GetCryptoCurrenciesAsync(ct),
            Updated = DateTimeOffset.UtcNow
        };
    }
}