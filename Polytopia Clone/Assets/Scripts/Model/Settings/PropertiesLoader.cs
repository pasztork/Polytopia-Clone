using System.IO;
using System.Text.Json;
using View;

namespace Model
{
    public static class PropertiesLoader
    {
        public static void Load(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("No file found for property settings!");
            }
            string json = File.ReadAllText(path);
            Settings settings = JsonSerializer.Deserialize<Settings>(json);

            Player.BaseProduction = settings.BaseProduction;
            BuildingBase.BuildingProperties = settings.BuildingProperties;
            TroopBase.TroopProperties = settings.TroopProperties;
        }

        public static DictionaryWrapper SetPlayerProperties()
        {
            return null;
        }
    }
}
