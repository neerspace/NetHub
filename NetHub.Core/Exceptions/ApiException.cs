using System.Net;
using NeerCore.Exceptions;

namespace NetHub.Core.Exceptions;

public class ApiException : HttpException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public override string ErrorType => "ApiException";

    public ApiException(string message) : base(message) { }

    public ApiException(string field, string message) : base(field, message) { }
}