namespace NetHub.Tests.ConsoleApp;

public class Assets
{
    private static readonly string s_assetsDirectoryPath = Path.Join(AppDomain.CurrentDomain.BaseDirectory, "Assets");


    public static string GetAssetPath(string filename) => Path.Join(s_assetsDirectoryPath, filename);

    public static string GetTextAsset(string filename) => File.ReadAllText(GetAssetPath(filename));
}