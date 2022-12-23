namespace NetHub.Application.Options;

public record CurrencyRateOptions
{
	public string CoinGeckoApiUrl { get; init; } = default!;
	public string MonobankApiUrl { get; init; } = default!;
}