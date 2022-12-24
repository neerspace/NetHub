using System.Text.Json;
using Mapster;
using NeerCore.DependencyInjection;
using NetHub.Application.Models.Currency;

namespace NetHub.Infrastructure.Services.Internal.Currency;

[Service]
internal sealed class CryptoRateService
{
    private readonly HttpClient _client;

    public CryptoRateService(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient("CoinGeckoClient");
    }

    public async Task<CryptoResponseDto> GetCryptoCurrencies()
    {
        var message =
            await _client.GetAsync("/api/v3/simple/price" +
                                   "?ids=the-open-network,bitcoin" +
                                   "&vs_currencies=usd,uah" +
                                   "&include_24hr_change=true");

        var response = await message.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<CryptoResponse>(response)!.Adapt<CryptoResponseDto>();
    }
}