// using System.ComponentModel.DataAnnotations;
// using MediatR;
//
// namespace NetHub.Application.Features.Public.Users.Sso;
//
// public record SsoEnterRequest : IRequest<(AuthModel, string)>
//
// {
// 	[Required] public string Username { get; set; } = default!;
// 	[Required] public string Email { get; set; } = default!;
// 	public string? Phone { get; set; }
// 	[Required] public string FirstName { get; set; } = default!;
// 	[Required] public string LastName { get; set; } = default!;
// 	[Required] public string MiddleName { get; set; } = default!;
// 	[Required] public string ProviderToken { get; set; } = default!;
// 	[Required] public ProviderType Provider { get; set; }
// 	
// 	public string 
// }