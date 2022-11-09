namespace NetHub.Application.Models.Currency;

public record ExchangeResponseDto
{
	public OneExchangeDto Usd { get; init; } = default!;
	public OneExchangeDto Euro { get; init; } = default!;

}

public record OneExchangeDto
{
	public string CurrencyFrom { get; init; } = default!;
	public string CurrencyTo { get; init; } = default!;
	public decimal Date { get; init; }
	public decimal RateBuy { get; init; }
	public decimal RateSell { get; init; }
}

public record OneExchangeResponse
{
	public decimal CurrencyCodeA { get; init; }
	public decimal CurrencyCodeB { get; init; }
	public decimal Date { get; init; }
	public decimal RateBuy { get; init; }
	public decimal RateSell { get; init; }
	public decimal RateCross { get; init; }
}