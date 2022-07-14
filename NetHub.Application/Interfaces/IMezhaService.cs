using NetHub.Application.Models.Mezha;

namespace NetHub.Application.Interfaces;

public interface IMezhaService
{
	Task<PostModel[]> GetNews(PostsFilter filter);
}