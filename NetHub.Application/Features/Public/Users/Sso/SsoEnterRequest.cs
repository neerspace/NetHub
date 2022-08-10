using System.ComponentModel.DataAnnotations;
using MediatR;
using NetHub.Application.Features.Public.Users.Dto;

namespace NetHub.Application.Features.Public.Users.Sso;

public record SsoEnterRequest : IRequest<(AuthResult, string)>

{
	[Required] public string Username { get; set; } = default!;
	[Required] public string Email { get; set; } = default!;
	[Required] public string FirstName { get; set; } = default!;
	public string? LastName { get; set; } = default!;
	public string? MiddleName { get; set; }
	public Dictionary<string, string>? ProviderMetadata { get; set; }
	[Required] public ProviderType Provider { get; set; }
}