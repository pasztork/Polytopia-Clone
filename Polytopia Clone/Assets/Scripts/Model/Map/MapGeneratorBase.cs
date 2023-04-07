using System;

namespace Model
{
    public abstract class MapGeneratorBase
    {
        public MapGenerationProperties MGP { protected get; set; }

        public abstract void GenerateMap();

        public abstract void ConnectLoadedMap();
    }
}