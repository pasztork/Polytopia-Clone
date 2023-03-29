namespace Controller
{
    public class TroopManager : TroopManagerBase
    {
        public override bool Train(Model.BuildingBase building, Model.TroopBase troop)
        {
            return Model.DependencyContainer.Get<Model.TrainManagerBase>().Train(building, troop);
        }

        public override bool MoveTroop(Model.TroopBase troop, Model.TileBase tile)
        {
            return Model.DependencyContainer.Get<Model.TurnManagerBase>().CurrentPlayer.MoveTroop(troop, tile);
        }

        public override bool Attack(Model.TroopBase attacker, Model.TroopBase target)
        {
            return Model.DependencyContainer.Get<Model.TurnManagerBase>().CurrentPlayer.Attack(attacker, target);
        }
    }
}