using System.Globalization;
using Microsoft.AspNetCore.Identity;

namespace NetHub.Application.Extensions;

public static class IdentityResultExtensions
{
    private static readonly TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
    private static readonly string[] fields = { "email", "username", "password" };

    public static IReadOnlyDictionary<string, object> ToErrorDetails(this IdentityResult identityResult) =>
        identityResult.Errors.ToDictionary(FieldName, err => err.Description as object);

    private static string FieldName(IdentityError err)
    {
        string? fieldName = fields.FirstOrDefault(f => err.Code.ToLower().Contains(f));
        return textInfo.ToTitleCase(fieldName ?? err.Code);
    }
}