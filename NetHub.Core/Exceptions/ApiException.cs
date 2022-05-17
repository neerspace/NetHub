using System.Net;

namespace NetHub.Core.Exceptions;

public class ApiException : HttpException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public override string ErrorType => "ApiException";

    public ApiException(string message, IReadOnlyList<ErrorDetails>? details = null) : base(message,
        details)
    {
    }

    public ApiException(string field, string message) : base(field, message)
    {
    }
}