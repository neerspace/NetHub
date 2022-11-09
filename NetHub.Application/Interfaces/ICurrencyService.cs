using NetHub.Application.Models.Currency;

namespace NetHub.Application.Interfaces;

public interface ICurrencyService
{
	Task<CryptoResponseDto> GetCryptoRate();
	Task<ExchangeResponseDto> GetExchangeRate();
	Task<CurrenciesResponse> GetRates();
}