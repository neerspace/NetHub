using NeerCore.Data.EntityFramework.Extensions;
using NetHub.Application.Tools;
using NetHub.Data.SqlServer.Entities;

namespace NetHub.Application.Features.Public.Resources.Get;

internal sealed class GetResourceHandler : DbHandler<GetResourceRequest, GetResourceResult>
{
    public GetResourceHandler(IServiceProvider serviceProvider) : base(serviceProvider) { }


    public override async Task<GetResourceResult> Handle(GetResourceRequest request, CancellationToken ct)
    {
        var resource = await Database.Set<Resource>().FirstOr404Async(r => r.Id == request.Id, ct);
        return new(resource.Bytes, resource.Mimetype);
    }
}