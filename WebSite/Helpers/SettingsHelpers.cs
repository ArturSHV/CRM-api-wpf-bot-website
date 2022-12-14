using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebSite.Helpers
{
    public static class SettingsHelpers
    {
        public static string? ReturnHostString()
        {
            string workingDirectory = Environment.CurrentDirectory;
            var filePath = Path.Combine(workingDirectory, "appsettings.json");
            string json = File.ReadAllText(filePath);
            var host = JObject.Parse(json)["Host"]?.Value<string>();
            return host;
        }

    }
}
