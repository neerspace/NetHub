using System.Net;

namespace NetHub.Core.Exceptions;

public class NotFoundException : HttpException
{
	public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
	public override string ErrorType => "NotFound";


	public NotFoundException(string message) : base(message) { }
}