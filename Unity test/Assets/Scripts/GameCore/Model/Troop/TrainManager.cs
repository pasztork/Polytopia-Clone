namespace Model
{
    public class TrainManager : TrainManagerBase
    {
        
        public override bool Train(BuildingBase building, TroopBase troop)
        {
            bool trained = GameManager.Get<TurnManagerBase>().CurrentPlayer.Train(building, troop);

            if (trained)
            {
                troop.Player = GameManager.Get<TurnManagerBase>().CurrentPlayer;
                RaiseOnTroopTrained(GameManager.Get<TurnManagerBase>().CurrentPlayer);
            }

            return trained;
        }
    }
}