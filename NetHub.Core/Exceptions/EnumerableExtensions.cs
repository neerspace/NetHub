namespace NetHub.Core.Exceptions;

public static class EnumerableExtensions
{
    public static (IEnumerable<T> True, IEnumerable<T> False) SplitBy<T>(
        this IEnumerable<T> collection, Func<T, bool> predicate)
    {
        var trueCollection = new List<T>();
        var falseCollection = new List<T>();

        foreach (var item in collection)
        {
            if (predicate(item))
                trueCollection.Add(item);
            else
                falseCollection.Add(item);
        }

        return (trueCollection, falseCollection);
    }
}