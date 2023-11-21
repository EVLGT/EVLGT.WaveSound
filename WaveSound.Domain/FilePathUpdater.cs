using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WaveSound.Domain;

public static class FilePathUpdater
{
    /// <summary>
    /// Updates the path in the appsettings.json file where audio files should be saved to.
    /// If debugging: Uncomment the code below "Debug Path Configuration"
    /// If releasing: Uncomment the code below "Release Path Configuration"
    /// </summary>
    /// <param name="newSavePath">Path of the new file saving location</param>
    public static void UpdateFilePath(string newSavePath)
    {
        try
        {
            // Release Path Configuration
            var jsonFilePath = "appsettings.json";
            // Debug Path Configuration
            //var jsonFilePath = GetAppSettingsPath();

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

    /// <summary>
    /// This method retrieves the path of the appsettings.json file.
    /// If debugging: Uncomment the code below "Debug Path Configuration"
    /// If releasing: Uncomment the code below "Relase Path Configuration"
    /// </summary>
    /// <returns>A string of the path (location) of appsettings.json</returns>
    public static string GetAppSettingsPath()
    {
        // Debug Path Configuration
        //var appSettingsFileName = "appsettings.json";
        //var assemblyPath = Assembly.GetExecutingAssembly().Location;
        //var domainFolderPath = Path.GetDirectoryName(assemblyPath);
        //var solutionRoot = Path.GetFullPath(Path.Combine(domainFolderPath, "../../../../"));

        //return Path.Combine(solutionRoot, appSettingsFileName);

        // Release Path Configuration
        return "appsettings.json";
    }

    public static string GetSavePath()
    {
        string json = File.ReadAllText(GetAppSettingsPath());
        dynamic data = JsonConvert.DeserializeObject(json);

        return data.FilePathConfig.SaveFilePath;
    }
}
