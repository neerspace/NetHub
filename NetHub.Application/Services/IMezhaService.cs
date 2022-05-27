using NetHub.Application.Models.Mezha;

namespace NetHub.Application.Services;

public interface IMezhaService
{
	Task<PostModel[]> GetNews(PostsFilter filter);
}