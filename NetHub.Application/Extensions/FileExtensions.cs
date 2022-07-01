using Microsoft.AspNetCore.Http;

namespace NetHub.Application.Extensions;

public static class FileExtensions
{
	public static string GetFileNameWithoutExtension(this IFormFile file)
		=> Path.ChangeExtension(file.FileName, null);

	public static string GetFileExtension(this IFormFile file)
		=> Path.GetExtension(file.FileName);
}