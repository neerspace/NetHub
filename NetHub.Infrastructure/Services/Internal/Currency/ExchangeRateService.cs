using LazyCache;
using Mapster;
using NetHub.Application.Models.Currency;
using NetHub.Core.DependencyInjection;
using Newtonsoft.Json;

namespace NetHub.Infrastructure.Services.Internal.Currency;

[Inject]
public class ExchangeRateService
{
	private const short USD_ISO_CODE = 840;
	private const short EURO_ISO_CODE = 978;
	private const short UAH_ISO_CODE = 980;
	private const string CACHE_KEY = "Monobank";

	private readonly HttpClient _client;

	private readonly IAppCache _memoryCache;

	public ExchangeRateService(IHttpClientFactory clientFactory, IAppCache memoryCache)
	{
		_memoryCache = memoryCache;
		_client = clientFactory.CreateClient("MonobankClient");
	}

	public async Task<ExchangeResponseDto> GetExchangeCurrencies() =>
		await _memoryCache
			.GetOrAddAsync(CACHE_KEY, async entry =>
			{
				entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(6);
				return await UpdateExchangeRatesCache();
			});

	private async Task<ExchangeResponseDto> UpdateExchangeRatesCache()
	{
		var message = await _client.GetAsync("/bank/currency");

		var response = JsonConvert.DeserializeObject<OneExchangeResponse[]>(await message.Content.ReadAsStringAsync())!;

		var usdResponse = response.First(r =>
			r.CurrencyCodeA is USD_ISO_CODE &&
			r.CurrencyCodeB is UAH_ISO_CODE);

		var euroResponse = response.First(r =>
			r.CurrencyCodeA is EURO_ISO_CODE &&
			r.CurrencyCodeB is UAH_ISO_CODE);

		return new()
		{
			Usd = usdResponse.Adapt<OneExchangeDto>() with {CurrencyFrom = "USD", CurrencyTo = "UAH"},
			Euro = euroResponse.Adapt<OneExchangeDto>() with {CurrencyFrom = "EURO", CurrencyTo = "UAH"},
		};
	}
}