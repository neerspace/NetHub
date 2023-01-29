using NetHub.Shared.Models.Currency;

namespace NetHub.Shared.Services;

public interface ICryptoRateService
{
    Task<CryptoResponseDto> GetCryptoCurrenciesAsync(CancellationToken ct = default);
}