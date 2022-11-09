using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Api.Abstractions;
using NetHub.Application.Features.Public.Currency;
using NetHub.Application.Models.Currency;

namespace NetHub.Api.Areas.Public.Controllers;

[AllowAnonymous]
public class CurrencyController: ApiController
{
	[HttpGet]
	public async Task<CurrenciesResponse> GetRates() 
		=> await Mediator.Send(new GetCurrenciesRequest());
}