using System.Dynamic;
using System.Text.Json;
using Mapster;
using Microsoft.AspNetCore.Http.Extensions;
using NeerCore.DependencyInjection;
using NetHub.Constants;
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
        _apiUri = "/api/v3/simple/price"
            + new QueryBuilder(new KeyValuePair<string, string>[]
            {
                new("ids", "the-open-network,bitcoin"),
                new("vs_currencies", "usd,uah"),
                new("include_24hr_change", "true")
            });
    }


    public async Task<CryptoResponseDto> GetCryptoCurrenciesAsync(CancellationToken ct = default)
    {
        var response = await _client.GetAsync(_apiUri, ct);
        if (response.IsSuccessStatusCode)
        {
            var message = await response.Content.ReadAsStringAsync(ct);
            return JsonSerializer.Deserialize<CryptoResponse>(message)!.Adapt<CryptoResponseDto>();
        }

        try
        {
            var message = await response.Content.ReadAsStringAsync(ct);
            dynamic json = JsonSerializer.Deserialize<ExpandoObject>(message)!;
            return new CryptoResponseDto { Error = json.message };
        }
        catch (Exception)
        {
            return new CryptoResponseDto { Error = "Unknown Error" };
        }
    }
}