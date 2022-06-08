using System.Drawing;

namespace NetHub.Recommendations.Entities;
	
public class UserArticleRatingsTable
{
	public List<UserArticleRatings> Users { get; set; }

	public List<long> UserIndexToId { get; set; }

	public List<long> ArticleIndexToId { get; set; }

	public UserArticleRatingsTable()
	{
		Users = new List<UserArticleRatings>();
		UserIndexToId = new List<int>();
		ArticleIndexToId = new List<int>();
	}

	public void AppendUserFeatures(double[][] userFeatures)
	{
		for (int i = 0; i < UserIndexToId.Count; i++)
		{
			Users[i].AppendRatings(userFeatures[i]);
		}
	}

	public void AppendArticleFeatures(double[][] articleFeatures)
	{
		for (int f = 0; f < articleFeatures[0].Length; f++)
		{
			UserArticleRatings newFeature = new UserArticleRatings(int.MaxValue, ArticleIndexToId.Count);

			for (int a = 0; a < ArticleIndexToId.Count; a++)
			{
				newFeature.ArticleRatings[a] = articleFeatures[a][f];
			}

			Users.Add(newFeature);
		}
	}

	// internal void AppendArticleFeatures(List<ArticleTagCounts> articleTags)
	// {
	//     double[][] features = new double[articleTags.Count][];
	//
	//     for (int a = 0; a < articleTags.Count; a++)
	//     {
	//         features[a] = new double[articleTags[a].TagCounts.Length];
	//
	//         for (int f = 0; f < articleTags[a].TagCounts.Length; f++)
	//         {
	//             features[a][f] = articleTags[a].TagCounts[f];
	//         }
	//     }
	//
	//     AppendArticleFeatures(features);
	// }

	// public void SaveSparcityVisual(string file)
	// {
	//     double min = Users.Min(x => x.ArticleRatings.Min());
	//     double max = Users.Max(x => x.ArticleRatings.Max());
	//
	//     Bitmap b = new Bitmap(ArticleIndexToID.Count, UserIndexToID.Count);
	//     int numPixels = 0;
	//
	//     for (int x = 0; x < ArticleIndexToID.Count; x++)
	//     {
	//         for (int y = 0; y < UserIndexToID.Count; y++)
	//         {
	//             //int brightness = 255 - (int)(((UserArticleRatings[y].ArticleRatings[x] - min) / (max - min)) * 255);
	//             //brightness = Math.Max(0, Math.Min(255, brightness));
	//
	//             int brightness = Users[y].ArticleRatings[x] == 0 ? 255 : 0;
	//
	//             Color c = Color.FromArgb(brightness, brightness, brightness);
	//
	//             b.SetPixel(x, y, c);
	//
	//             numPixels += Users[y].ArticleRatings[x] != 0 ? 1 : 0;
	//         }
	//     }
	//
	//     double sparcity = (double)numPixels / (ArticleIndexToID.Count * UserIndexToID.Count);
	//
	//     b.Save(file);
	// }

	/// <summary>
	/// Generate a CSV report of users and how many ratings they've given
	/// </summary>
	public void SaveUserRatingDistribution(string file)
	{
		int bucketSize = 4;
		int maxRatings = Users.Max(x => x.ArticleRatings.Count(y => y != 0));
		List<int> buckets = new List<int>();

		for (int i = 0; i <= Math.Floor((double) maxRatings / bucketSize); i++)
		{
			buckets.Add(0);
		}

		foreach (UserArticleRatings ratings in Users)
		{
			buckets[(int) Math.Floor((double) ratings.ArticleRatings.Count(x => x != 0) / bucketSize)]++;
		}

		using (StreamWriter w = new StreamWriter(file))
		{
			w.WriteLine("numArticlesRead,numUsers");

			for (int i = 0; i <= Math.Floor((double) maxRatings / bucketSize); i++)
			{
				w.WriteLine("=\"" + (i * bucketSize) + "-" + (((i + 1) * bucketSize) - 1) + "\"," + buckets[i / bucketSize]);
			}
		}
	}

	/// <summary>
	/// Generate a CSV report of articles and how many ratings they've gotten
	/// </summary>
	public void SaveArticleRatingDistribution(string file)
	{
		int bucketSize = 2;
		int maxRatings = 3000;
		List<int> buckets = new List<int>();

		for (int i = 0; i <= Math.Floor((double) maxRatings / bucketSize); i++)
		{
			buckets.Add(0);
		}

		for (int i = 0; i < ArticleIndexToId.Count; i++)
		{
			int readers = Users.Select(x => x.ArticleRatings[i]).Count(x => x != 0);
			buckets[(int) Math.Floor((double) readers / bucketSize)]++;
		}

		while (buckets[buckets.Count - 1] == 0)
		{
			buckets.RemoveAt(buckets.Count - 1);
		}

		using (StreamWriter w = new StreamWriter(file))
		{
			w.WriteLine("numReaders,numArticles");

			for (int i = 0; i < buckets.Count; i++)
			{
				w.WriteLine("=\"" + (i * bucketSize) + "-" + (((i + 1) * bucketSize) - 1) + "\"," + buckets[i]);
			}
		}
	}
}