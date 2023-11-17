using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WaveSound.Domain;

public static class FilePathUpdater
{
    public static void UpdateFilePath(string newSavePath)
    {
        try
        {
            var jsonFilePath = GetAppSettingsPath();

            var json = File.ReadAllText(jsonFilePath);
            var jsonObject = JObject.Parse(json);

            jsonObject["FilePathConfig"]["SaveFilePath"] = newSavePath;

            var updatedJson = jsonObject.ToString();

            File.WriteAllText(jsonFilePath, updatedJson);

            Console.WriteLine("SaveFilePath updated successfully.");
        }

        catch (Exception ex)
        {
            Console.WriteLine($"Error updating SaveFilePath: {ex.Message}");
        }
    }

    public static string GetAppSettingsPath()
    {
        var appSettingsFileName = "appsettings.json";
        var assemblyPath = Assembly.GetExecutingAssembly().Location;
        var domainFolderPath = Path.GetDirectoryName(assemblyPath);
        var solutionRoot = Path.GetFullPath(Path.Combine(domainFolderPath, "../../../../"));

        return Path.Combine(solutionRoot, appSettingsFileName);
    }

    public static string GetSavePath()
    {
        string json = File.ReadAllText(GetAppSettingsPath());
        dynamic data = JsonConvert.DeserializeObject(json);

        return data.FilePathConfig.SaveFilePath;
    }
}
