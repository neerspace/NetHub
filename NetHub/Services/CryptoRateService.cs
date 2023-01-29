using System.Text.Json;
using Mapster;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Primitives;
using NeerCore.DependencyInjection;
using NetHub.Shared.Constants;
using NetHub.Shared.Models.Currency;
using NetHub.Shared.Services;

namespace NetHub.Services;

[Service(Lifetime = Lifetime.Singleton)]
internal sealed class CryptoRateService : ICryptoRateService
{
    private readonly HttpClient _client;
    private readonly string _apiUri;

    public CryptoRateService(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient(HttpClientNames.CoinGeckoClient);
        _apiUri = new UriBuilder
        {
            Path = "/api/v3/simple/price",
            Query = new QueryBuilder(new KeyValuePair<string, StringValues>[]
            {
                new("ids", new[] { "the-open-network", "bitcoin" }),
                new("vs_currencies", new[] { "usd", "uah" }),
                new("include_24hr_change", "true")
            }).ToString()
        }.ToString();
    }


    public async Task<CryptoResponseDto> GetCryptoCurrenciesAsync(CancellationToken ct = default)
    {
        var message = await _client.GetAsync(_apiUri, ct);
        var response = await message.Content.ReadAsStringAsync(ct);
        return JsonSerializer.Deserialize<CryptoResponse>(response)!.Adapt<CryptoResponseDto>();
    }
}