using MediatR;
using NetHub.Application.Models.Currency;

namespace NetHub.Application.Features.Public.Currency;

public record GetCurrenciesRequest : IRequest<CurrenciesResponse>;