using Mapster;
using Microsoft.AspNetCore.Http;
using NetHub.Data.SqlServer.Entities;
using NetHub.Shared.Extensions;
using NetHub.Shared.Models.Languages;

namespace NetHub.Admin.Mappings;

public class LanguageMappings : IRegister
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private HttpContext HttpContext => _httpContextAccessor.HttpContext!;

    public LanguageMappings(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;


    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Language, LanguageModel>()
            .Map(m => m.FlagUrl,
                e =>
                    e.FlagId.HasValue
                        ? HttpContext.Request.GetResourceUrl(e.FlagId.Value)
                        : null);
    }
}