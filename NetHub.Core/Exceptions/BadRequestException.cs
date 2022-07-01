using System.Net;

namespace NetHub.Core.Exceptions;

public class BadRequestException : HttpException
{
	public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
	public override string ErrorType => "BadRequest";

	public BadRequestException(string message) : base(message) { }
}