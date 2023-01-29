using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using NetHub.Data.SqlServer.Context;
using NetHub.Data.SqlServer.Entities;
using NetHub.Data.SqlServer.Entities.Articles;

namespace NetHub;

public static class HtmlUtility
{
    public static IEnumerable<string> FetchLinksFromSource(string htmlSource)
    {
        var links = new List<string>();
        const string regexImgSrc = @"<img[^>]*?src\s*=\s*[""']?([^'"" >]+?)[ '""][^>]*?>";
        var matchesImgSrc = Regex.Matches(htmlSource, regexImgSrc, RegexOptions.IgnoreCase | RegexOptions.Singleline);
        foreach (Match m in matchesImgSrc)
        {
            var href = m.Groups[1].Value;
            links.Add(href);
        }

        return links;
    }

    public static async Task CheckLinks(ISqlServerDatabase database, long articleId, string html)
    {
        var articleResources = await database.Set<ArticleResource>()
            .Where(ar => ar.ArticleId == articleId)
            .ToArrayAsync();

        var localizationsHtml = await database.Set<ArticleLocalization>()
            .Where(al => al.ArticleId == articleId)
            .Select(al => al.Html)
            .ToArrayAsync();

        var htmlLinks = new List<string>();

        foreach (var lHtml in localizationsHtml)
            htmlLinks.AddRange(FetchLinksFromSource(lHtml));
        htmlLinks.AddRange(FetchLinksFromSource(html));

        var removeResources = articleResources
            .Where(resource => !htmlLinks.Any(l => l.Contains(resource.ResourceId.ToString())))
            .Select(r => new Resource { Id = r.ResourceId })
            .ToArray();

        database.Set<Resource>().RemoveRange(removeResources);
    }
}