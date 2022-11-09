namespace NetHub.Application.Models.Currency;

public record CurrenciesResponse
{
	public ExchangeResponseDto Exchanges { get; init; } = default!;
	public CryptoResponseDto Crypto { get; init; } = default!;
	public DateTimeOffset Updated { get; init; }
}