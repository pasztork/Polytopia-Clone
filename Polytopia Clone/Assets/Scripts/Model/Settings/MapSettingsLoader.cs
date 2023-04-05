using System.IO;
using System.Text.Json;

namespace Model
{
    public static class MapSettingsLoader
    {
        public static void Load(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("No file found for map properties!");
            }
            string json = File.ReadAllText(path);
            MapGenerationProperties properties = JsonSerializer.Deserialize<MapGenerationProperties>(json);
            GameManager.Get<MapGeneratorBase>().MGP = properties;
        }
    }
}