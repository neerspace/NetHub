using System.Text.Json.Serialization;

namespace NetHub.Shared.Models.Currency;

public record CryptoResponse
{
	[JsonPropertyName("bitcoin")] public OneCrypto Btc { get; init; }
	[JsonPropertyName("the-open-network")] public OneCrypto Ton { get; init; }
}

public record CryptoResponseDto
{
	public OneCryptoDto Btc { get; init; } = default!;
	public OneCryptoDto Ton { get; init; } = default!;
}

public record OneCrypto
{
	[JsonPropertyName("usd")] public decimal Usd { get; init; } = default!;
	[JsonPropertyName("usd_24h_change")] public decimal Usd24Change { get; init; } = default!;
	[JsonPropertyName("uah")] public decimal Uah { get; init; } = default!;
	[JsonPropertyName("uah_24h_change")] public decimal Uah24Change { get; init; } = default!;
}

public record OneCryptoDto
{
	public decimal Usd { get; init; } = default!;
	public decimal Usd24Change { get; init; } = default!;
	public decimal Uah { get; init; } = default!;
	public decimal Uah24Change { get; init; } = default!;
}