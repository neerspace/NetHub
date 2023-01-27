using LazyCache;
using Mapster;
using NeerCore.DependencyInjection;
using NetHub.Application.Constants;
using NetHub.Application.Models.Currency;
using NetHub.Application.Services;
using Newtonsoft.Json;

namespace NetHub.Infrastructure.Services;

[Service(Lifetime = Lifetime.Singleton)]
internal sealed class ExchangeRateService : IExchangeRateService
{
    private const short UsdIsoCode = 840;
    private const short EuroIsoCode = 978;
    private const short UahIsoCode = 980;
    private const string CacheKey = "Monobank";

    private readonly HttpClient _client;
    private readonly IAppCache _memoryCache;

    public ExchangeRateService(IHttpClientFactory clientFactory, IAppCache memoryCache)
    {
        _memoryCache = memoryCache;
        _client = clientFactory.CreateClient(HttpClientNames.MonobankClient);
    }


    public async Task<ExchangeResponseDto> GetExchangeCurrenciesAsync(CancellationToken ct = default) =>
        await _memoryCache
            .GetOrAddAsync(CacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(6);
                return await UpdateExchangeRatesCache();
            });

    private async Task<ExchangeResponseDto> UpdateExchangeRatesCache()
    {
        var message = await _client.GetAsync("/bank/currency");

        var response = JsonConvert.DeserializeObject<OneExchangeResponse[]>(
            await message.Content.ReadAsStringAsync())!;

        var usdResponse = response.First(r =>
            r.CurrencyCodeA is UsdIsoCode && r.CurrencyCodeB is UahIsoCode);

        var euroResponse = response.First(r =>
            r.CurrencyCodeA is EuroIsoCode && r.CurrencyCodeB is UahIsoCode);

        return new()
        {
            Usd = usdResponse.Adapt<OneExchangeDto>() with
            {
                CurrencyFrom = "USD",
                CurrencyTo = "UAH"
            },
            Euro = euroResponse.Adapt<OneExchangeDto>() with
            {
                CurrencyFrom = "EURO",
                CurrencyTo = "UAH"
            },
        };
    }
}