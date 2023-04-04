using System.IO;
using System.Text.Json;

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
            // TODO: Load all values into static fields in each class
        }
    }
}
