namespace NetHub.Application.Models;

public record Filtered<TModel>(int Total, IEnumerable<TModel> Data);