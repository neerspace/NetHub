using NetHub.Application.Models.Currency;

namespace NetHub.Application.Services;

public interface IExchangeRateService
{
    Task<ExchangeResponseDto> GetExchangeCurrenciesAsync(CancellationToken ct = default);
}