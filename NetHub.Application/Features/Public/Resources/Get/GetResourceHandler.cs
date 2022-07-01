using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Extensions;

namespace NetHub.Application.Features.Public.Resources.Get;

public class GetResourceHandler : DbHandler<GetResourceRequest, GetResourceResult>
{
	public GetResourceHandler(IServiceProvider serviceProvider) : base(serviceProvider)
	{
	}

	protected override async Task<GetResourceResult> Handle(GetResourceRequest request)
	{
		var resource = await Database.Set<Resource>().FirstOr404Async(r => r.Id == request.Id);
		return new(resource.Bytes, resource.Mimetype);
	}
}