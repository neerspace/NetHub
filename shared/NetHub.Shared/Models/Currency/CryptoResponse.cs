using System.Text.Json.Serialization;

namespace NetHub.Shared.Models.Currency;

public class CryptoResponse
{
    [JsonPropertyName("bitcoin")]
    public required OneCrypto Btc { get; init; }

[JsonPropertyName("the-open-network")]
public required OneCrypto Ton { get; init; }
}

public class CryptoResponseDto
{
    public OneCryptoDto Btc { get; init; }
    public OneCryptoDto Ton { get; init; }
    public string? Error { get; set; }
}

public class OneCrypto
{
    [JsonPropertyName("usd")]
    public required decimal Usd { get; init; }

    [JsonPropertyName("usd_24h_change")]
    public required decimal Usd24Change { get; init; }

    [JsonPropertyName("uah")]
    public required decimal Uah { get; init; }

    [JsonPropertyName("uah_24h_change")]
    public required decimal Uah24Change { get; init; }
}

public class OneCryptoDto
{
    public required decimal Usd { get; init; }
    public required decimal Usd24Change { get; init; }
    public required decimal Uah { get; init; }
    public required decimal Uah24Change { get; init; }
}