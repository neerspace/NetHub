using Microsoft.Extensions.Options;
using NeerCore.DependencyInjection;
using Sieve.Models;
using Sieve.Services;

namespace NetHub.Admin.Services;

[Service<ISieveProcessor>]
public class AdminSieveProcessor : SieveProcessor
{
    public AdminSieveProcessor(IOptions<SieveOptions> options) : base(options) { }

    protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper) =>
        mapper.ApplyConfigurationsFromAssembly(GetType().Assembly);
}