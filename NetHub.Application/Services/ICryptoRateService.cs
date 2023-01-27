using NetHub.Application.Models.Currency;

namespace NetHub.Application.Services;

public interface ICryptoRateService
{
    Task<CryptoResponseDto> GetCryptoCurrenciesAsync(CancellationToken ct = default);
}