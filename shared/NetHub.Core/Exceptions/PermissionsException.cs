using System.Net;
using NeerCore.Exceptions;

namespace NetHub.Core.Exceptions;

public class PermissionsException : HttpException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.Forbidden;

    public override string ErrorType => "PermissionDenied";

    public PermissionsException() : base("You have no permissions to do this") { }
}