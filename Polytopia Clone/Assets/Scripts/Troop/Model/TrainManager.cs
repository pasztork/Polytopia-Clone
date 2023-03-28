namespace Model
{
    public class TrainManager : TrainManagerBase
    {
        public override bool Train(BuildingBase building, TroopBase troop)
        {
            bool trained = DependencyContainer.Get<TurnManagerBase>().CurrentPlayer.Train(building, troop);

            if (trained)
            {
                troop.Tile = building.Tile;
                troop.Player = DependencyContainer.Get<TurnManagerBase>().CurrentPlayer;
                RaiseOnTroopTrained(DependencyContainer.Get<TurnManagerBase>().CurrentPlayer);
                LogDataWrapper.Instance.TriggerTrain(troop);
            }

            return trained;
        }
    }
}