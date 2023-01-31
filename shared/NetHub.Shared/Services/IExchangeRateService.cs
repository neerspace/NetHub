using NetHub.Shared.Models.Currency;

namespace NetHub.Shared.Services;

public interface IExchangeRateService
{
    Task<ExchangeResponseModel> GetExchangeCurrenciesAsync(CancellationToken ct = default);
}