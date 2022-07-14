using System.ComponentModel.DataAnnotations;
using MediatR;

namespace NetHub.Application.Features.Public.Users.Sso;

public record SsoEnterRequest : IRequest<(AuthModel, string)>

{
	[Required] public string Username { get; set; } = default!;
	[Required] public string Email { get; set; } = default!;
	[Required] public string FirstName { get; set; } = default!;
	public string? LastName { get; set; } = default!;
	public string? MiddleName { get; set; }
	public Dictionary<string, string>? ProviderMetadata { get; set; }
	[Required] public ProviderType Provider { get; set; }
}