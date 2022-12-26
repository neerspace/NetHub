namespace NetHub.Admin.Infrastructure.Models;

public sealed record Filtered<TModel>(
    int Total,
    IEnumerable<TModel> Data
);