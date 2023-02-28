namespace NetHub.Shared.Models.Currency;

public record CurrenciesResponse
{
    public ExchangeResponseModel Exchanges { get; init; } = default!;
    public CryptoResponseDto Crypto { get; init; } = default!;
    public DateTimeOffset Updated { get; init; }
}