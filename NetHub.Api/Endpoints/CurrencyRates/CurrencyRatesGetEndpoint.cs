using Microsoft.AspNetCore.Mvc;
using NetHub.Admin.Api.Abstractions;
using NetHub.Api.Shared;
using NetHub.Application.Interfaces;
using NetHub.Application.Models.Currency;

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