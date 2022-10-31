using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebApi.Helpers
{
    public static class SettingsHelpers
    {
        public static string? ReturnHostString()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            var filePath = Path.Combine(projectDirectory, "appsettings.json");
            string json = File.ReadAllText(filePath);
            var host = JObject.Parse(json)["Host"]?.Value<string>();
            return host;
        }

    }
}
