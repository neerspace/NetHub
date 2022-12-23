using System.Collections.ObjectModel;
using System.Text.Json;
using Mapster;
using NetHub.Application.Models.Currency;
using NetHub.Core.DependencyInjection;

namespace NetHub.Infrastructure.Services.Internal.Currency;

[Inject]
public class CryptoRateService
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