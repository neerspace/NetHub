namespace NetHub.Shared.Models.Currency;

public record ExchangeResponseModel
{
    public OneExchangeModel Usd { get; init; } = default!;
    public OneExchangeModel Euro { get; init; } = default!;
}

public record OneExchangeModel
{
    public string CurrencyFrom { get; init; } = default!;
    public string CurrencyTo { get; init; } = default!;
    public decimal Date { get; init; }
    public decimal RateBuy { get; init; }
    public decimal RateSell { get; init; }
}

public record OneExchangeResponseModel
{
    public decimal CurrencyCodeA { get; init; }
    public decimal CurrencyCodeB { get; init; }
    public decimal Date { get; init; }
    public decimal RateBuy { get; init; }
    public decimal RateSell { get; init; }
    public decimal RateCross { get; init; }
}