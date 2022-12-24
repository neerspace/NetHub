using MediatR;
using NetHub.Application.Interfaces;
using NetHub.Application.Models.Currency;

namespace NetHub.Application.Features.Public.Currency;

internal sealed class GetCurrenciesHandler : IRequestHandler<GetCurrenciesRequest, CurrenciesResponse>
{
    private readonly ICurrencyService _currencyService;

    public GetCurrenciesHandler(ICurrencyService currencyService) => _currencyService = currencyService;


    public async Task<CurrenciesResponse> Handle(GetCurrenciesRequest request, CancellationToken ct)
    {
        var result = await _currencyService.GetRates();

        return result with { Updated = DateTimeOffset.UtcNow };
    }
}