using MediatR;
using NetHub.Application.Interfaces;
using NetHub.Application.Models.Currency;

namespace NetHub.Application.Features.Public.Currency;

internal sealed class GetCurrenciesHandler : IRequestHandler<GetCurrenciesRequest, CurrenciesResponse>
{

    public GetCurrenciesHandler(ICurrencyService currencyService) => _currencyService = currencyService;


    public async Task<CurrenciesResponse> Handle(GetCurrenciesRequest request, CancellationToken ct)
    {
        }
}