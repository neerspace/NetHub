using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.Articles;
using NetHub.Shared.Models.Articles;

namespace NetHub.Extensions;

public static class ArticlesExtensions
{
    public static IQueryable<ArticleModel> GetExtendedArticles(
        this ISqlServerDatabase database, long? userId = null, bool loadBody = false, bool loadContributors = false, Expression<Func<Article, bool>>? whereExpression = null)
    {
        IQueryable<Article> articlesDbSet = database
            .Set<Article>()
            .Include(a => a.ArticleSet);

        if (whereExpression is not null)
            articlesDbSet = articlesDbSet.Where(whereExpression);

        if (loadContributors)
            articlesDbSet = articlesDbSet
                .Include(l => l.Contributors)
                .ThenInclude(c => c.User);

        if (userId == null)
            return articlesDbSet.Select(l => new ArticleModel
            {
                Id = l.Id,
                ArticleSetId = l.ArticleSetId,
                LanguageCode = l.LanguageCode,
                Title = l.Title,
                Contributors = loadContributors
                    ? l.Contributors.Select(c => new ArticleContributorModel
                    {
                        Role = c.Role,
                        UserName = c.User!.UserName,
                        ProfilePhotoUrl = c.User!.ProfilePhotoUrl
                    }).ToArray()
                    : new ArticleContributorModel[] { },
                Description = l.Description,
                Html = loadBody ? l.Html : string.Empty,
                Views = l.Views,
                Status = l.Status,
                Created = l.Created,
                Updated = l.Updated,
                Published = l.Published,
                Banned = l.Banned,
                Rate = l.ArticleSet!.Rate,
            });

        var votesDbSet = database.Set<ArticleSetVote>();
        var savedDbSet = database.Set<SavedArticle>();

        var result = from l in articlesDbSet
            //Join votes
            join _v in votesDbSet on
                new {l.ArticleSetId, UserId = (long)userId}
                equals
                new {_v.ArticleSetId, _v.UserId}
                into votes
            from v in votes.DefaultIfEmpty()
            //Join savings
            join _s in savedDbSet on
                new {ArticleId = l.Id, UserId = (long)userId}
                equals
                new {_s.ArticleId, _s.UserId}
                into saved
            from s in saved.DefaultIfEmpty()
            select new ArticleModel
            {
                Id = l.Id,
                ArticleSetId = l.ArticleSetId,
                LanguageCode = l.LanguageCode,
                Title = l.Title,
                Contributors = loadContributors
                    ? l.Contributors.Select(c => new ArticleContributorModel
                    {
                        Role = c.Role,
                        UserName = c.User!.UserName,
                        ProfilePhotoUrl = c.User!.ProfilePhotoUrl
                    }).ToArray()
                    : new ArticleContributorModel[] { },
                Description = l.Description,
                Html = loadBody ? l.Html : string.Empty,
                Views = l.Views,
                Status = l.Status,
                Created = l.Created,
                Updated = l.Updated,
                Published = l.Published,
                Banned = l.Banned,
                Vote = v.Vote,
                Rate = l.ArticleSet!.Rate,
                IsSaved = s != null,
                SavedDate = s != null ? s.SavedDate : null
            };

        return result;
    }
}