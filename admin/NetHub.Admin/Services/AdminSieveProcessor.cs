using Microsoft.Extensions.Options;
using NeerCore.DependencyInjection;
using Sieve.Models;
using Sieve.Services;

namespace NetHub.Admin.Services;

[Service(ServiceType = typeof(ISieveProcessor))]
public class AdminSieveProcessor : SieveProcessor
{
    public AdminSieveProcessor(IOptions<SieveOptions> options, ISieveCustomFilterMethods customFilter)
        : base(options, customFilter) { }

    protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper) =>
        mapper.ApplyConfigurationsFromAssembly(GetType().Assembly);
}