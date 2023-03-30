namespace Controller
{
    public class TurnManager : TurnManagerBase
    {
        public override void FinishTurn()
        {
            TechTreeManager.Instance.EndTurn();
            Model.GameManager.Get<Model.TurnManagerBase>().FinishTurn();
        }
    }
}
