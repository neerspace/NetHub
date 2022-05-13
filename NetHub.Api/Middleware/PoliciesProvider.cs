using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using NetHub.Core.Constants;
using NetHub.Core.DependencyInjection;

namespace NetHub.Api.Middleware;

[Inject]
public class PoliciesProvider : DefaultAuthorizationPolicyProvider
{
    private readonly AuthorizationOptions _options;

    public PoliciesProvider(IOptions<AuthorizationOptions> optionsAccessor) : base(optionsAccessor)
    {
        _options = optionsAccessor.Value;
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        switch (policyName)
        {
            case Policies.User:
                var policy = await base.GetPolicyAsync(policyName) ??
                             new AuthorizationPolicyBuilder().RequireScope(Scopes.NetHubUser).Build();
                return policy;
            case Policies.Admin:
                var policy1 = await base.GetPolicyAsync(policyName) ??
                              new AuthorizationPolicyBuilder().RequireScope(Scopes.NetHubAdmin).Build();
                return policy1;
            case Policies.Master:
                var policy2 = await base.GetPolicyAsync(policyName) ??
                              new AuthorizationPolicyBuilder().RequireScope(Scopes.NetHubMaster).Build();
                return policy2;
        }

        return null;
    }
}