namespace NetHub.Application.Constants;

public class MezhaArticleTypes
{
	public static ArticleTypeModel News => new(1, nameof(News));
	
}


public record ArticleTypeModel(int Id, string Name);