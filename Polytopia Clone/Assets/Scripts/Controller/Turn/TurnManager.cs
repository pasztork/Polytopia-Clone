namespace Controller
{
    public class TurnManager : TurnManagerBase
    {
        public override void FinishTurn()
        {
            TechTreeManager.Instance.EndTurn();
            Model.DependencyContainer.Get<Model.TurnManagerBase>().FinishTurn();
        }
    }
}
