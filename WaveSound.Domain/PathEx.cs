using System.Text;

namespace WaveSound.Domain;

public static class PathEx
{
    private static readonly HashSet<char> InvalidFileNameChars = new(Path.GetInvalidFileNameChars());

    public static string EscapeFileName(string path)
    {
        var buffer = new StringBuilder(path.Length);

        foreach (var c in path)
        {
            buffer.Append(!InvalidFileNameChars.Contains(c) ? c : '_');
        }

        return buffer.ToString();
    }
}