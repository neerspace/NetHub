namespace NetHub.Core.Extensions;

public static class DictionaryExtensions
{
    public static bool ContainsKeys<T, TK>(this Dictionary<T, TK> dictionary, params T[] keys) where T : notnull
        => keys.All(dictionary.ContainsKey);
}