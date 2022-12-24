using NeerCore.DependencyInjection;
using NetHub.Application.Interfaces;
using NetHub.Application.Models.Currency;
using NetHub.Infrastructure.Services.Internal.Currency;

namespace NetHub.Infrastructure.Services;

[Service]
internal class CurrencyService : ICurrencyService
{
    private readonly CryptoRateService _cryptoRateService;
    private readonly ExchangeRateService _exchangeRateService;

    public CurrencyService(CryptoRateService cryptoRateService, ExchangeRateService exchangeRateService)
    {
        _cryptoRateService = cryptoRateService;
        _exchangeRateService = exchangeRateService;
    }

    public Task<CryptoResponseDto> GetCryptoRate() => _cryptoRateService.GetCryptoCurrencies();

    public Task<ExchangeResponseDto> GetExchangeRate() => _exchangeRateService.GetExchangeCurrencies();

    public async Task<CurrenciesResponse> GetRates()
    {
        var exchange = await _exchangeRateService.GetExchangeCurrencies();
        var crypto = await _cryptoRateService.GetCryptoCurrencies();

        return new()
        {
            Exchanges = exchange,
            Crypto = crypto
        };
    }
}