using NetHub.Application.Models.Currency;

namespace NetHub.Application.Interfaces;

public interface IExchangeRateService
{
    Task<ExchangeResponseDto> GetExchangeCurrenciesAsync(CancellationToken ct = default);
}