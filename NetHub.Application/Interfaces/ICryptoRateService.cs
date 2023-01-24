using NetHub.Application.Models.Currency;

namespace NetHub.Application.Interfaces;

public interface ICryptoRateService
{
    Task<CryptoResponseDto> GetCryptoCurrenciesAsync(CancellationToken ct = default);
}