namespace Model
{
    public class GameManager : GameManagerBase
    {
        public override void Start()
        {
            foreach (Player player in Players)
                player.SetupStartingPosition();

            DependencyContainer.Get<TechTreeManagerBase>().BuildTechTree();
            DependencyContainer.Get<TurnManagerBase>().Start();
        }
    }
}