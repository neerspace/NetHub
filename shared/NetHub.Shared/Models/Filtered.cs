namespace NetHub.Shared.Models;

public record Filtered<TModel>(int Total, IEnumerable<TModel> Data);