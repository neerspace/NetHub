using Microsoft.AspNetCore.Http;

namespace NetHub.Shared.Extensions;

public static class FormFileExtensions
{
	public static string GetFileNameWithoutExtension(this IFormFile file)
		=> Path.ChangeExtension(file.FileName, null);

	public static string GetFileExtension(this IFormFile file)
		=> Path.GetExtension(file.FileName);
}