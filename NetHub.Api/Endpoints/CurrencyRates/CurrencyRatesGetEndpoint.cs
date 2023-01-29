using Microsoft.AspNetCore.Mvc;
using NetHub.Api.Shared;
using NetHub.Api.Shared.Abstractions;
using NetHub.Application.Models.Currency;
using NetHub.Application.Services;

namespace NetHub.Api.Endpoints.CurrencyRates;

[Tags(TagNames.Currency)]
[ApiVersion(Versions.V1)]
public class CurrencyRatesGetEndpoint : ResultEndpoint<CurrenciesResponse>
{
    private readonly ICryptoRateService _cryptoRateService;
    private readonly IExchangeRateService _exchangeRateService;

    public CurrencyRatesGetEndpoint(ICryptoRateService cryptoRateService, IExchangeRateService exchangeRateService)
    {
        _cryptoRateService = cryptoRateService;
        _exchangeRateService = exchangeRateService;
    }


    [HttpGet("currency-rates")]
    public override async Task<CurrenciesResponse> HandleAsync(CancellationToken ct)
    {
        return new CurrenciesResponse
        {
            Exchanges = await _exchangeRateService.GetExchangeCurrenciesAsync(ct),
            Crypto = await _cryptoRateService.GetCryptoCurrenciesAsync(ct),
            Updated = DateTimeOffset.UtcNow
        };
    }
}