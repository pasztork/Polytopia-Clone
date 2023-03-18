namespace Model
{
    public class GameManager
    {
        public static GameManager instance;
        public static GameManager Instance
        {
            get
            {
                instance ??= new GameManager();
                return instance;
            }

            private set => instance = value;
        }

        public void Start(int mapSize, float waterProbability)
        {
            MapManager.Instance.Size = mapSize;
            MapManager.Instance.GenerateMap(waterProbability);
        }
    }
}