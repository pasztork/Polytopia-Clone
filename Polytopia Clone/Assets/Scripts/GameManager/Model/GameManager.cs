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
        }

        public void Start()
        {
            TurnManager.Instance.Start();
        }
    }
}