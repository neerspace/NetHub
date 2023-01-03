using Microsoft.Extensions.Options;
using Sieve.Models;
using Sieve.Services;

namespace NetHub.Application.SharedServices;

// TODO: Use or remove.
public class NetSieveProcessor : SieveProcessor
{
    public NetSieveProcessor(IOptions<SieveOptions> options, ISieveCustomFilterMethods customFilter) : base(options, customFilter) { }

    protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
    {
        return mapper;
    }
}