
export class QueryClientKeysHelper {
  public static Keys = {
    articles: 'articles',
    articleSet: 'articleSet',
    article: 'article',
    savedArticles: 'savedArticles',
    contributors: 'contributors',
    contributor: 'contributor',
    authorization: 'authorization',
    user: 'user',
    dashboard: 'dashboard'
  };

  public static ArticlesThread = (language: string, isLogin: boolean | null) =>
    [this.Keys.articles, language, isLogin];

  public static ArticleSet = (id: number) => [this.Keys.articleSet, id];

  public static Article = (id: number, code: string) => [this.Keys.article, id, code];

  public static Contributor = (username: string) => [this.Keys.contributor, username];

  public static Contributors = (articleId: number, languageCode: string) => [this.Keys.contributors, articleId, languageCode];

  public static ContributorArticles = (username: string, articlesLanguage: string) =>
    [this.Keys.contributor, this.Keys.articles, username, articlesLanguage];

  public static Profile = (username: string) => [this.Keys.user, username];

  public static Dashboard = (username?: string) => [this.Keys.dashboard, username];

  public static SavedArticles = () => [this.Keys.savedArticles];
}