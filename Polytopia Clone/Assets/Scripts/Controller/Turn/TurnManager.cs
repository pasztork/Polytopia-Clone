namespace Controller
{
    public class TurnManager : TurnManagerBase
    {
        public override void FinishTurn()
        {
            Model.GameManager.Get<Model.TurnManagerBase>().FinishTurn();
        }
    }
}
