using Microsoft.AspNetCore.Http;
using NetHub.Application.Extensions;
using NetHub.Application.Services;
using NetHub.Core.Abstractions.Context;
using NetHub.Core.DependencyInjection;
using NetHub.Core.Exceptions;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Infrastructure.Services;

[Inject]
public class ResourceService : IResourceService
{
	private readonly IDatabaseContext _database;

	public ResourceService(IDatabaseContext database)
	{
		_database = database;
	}

	public async Task<Guid> SaveResourceToDb(IFormFile file)
	{
		if (file.Length == 0)
			throw new ApiException("File is corrupted");
		
		var fileEntity = new Resource
		{
			Filename = file.FileName,
			Bytes = await GetFileBytes(file),
			Mimetype = file.ContentType
		};

		var createdEntity = await _database.Set<Resource>().AddAsync(fileEntity);
		await _database.SaveChangesAsync();

		return createdEntity.Entity.Id;
	}

	public async Task DeleteResourceFromDb(Guid id)
	{
		_database.Set<Resource>().Remove(new() {Id = id});
		await _database.SaveChangesAsync();
	}

	private async Task<byte[]> GetFileBytes(IFormFile file)
	{
		using var stream = new MemoryStream();
		await file.CopyToAsync(stream);
		var fileBytes = stream.ToArray();

		return fileBytes;
	}

	private string GetResourceName(IFormFile file)
		//Unique name: Filename-TimeInMilliseconds.extension
		=>
			$"{file.GetFileNameWithoutExtension()}-{DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond}";
}