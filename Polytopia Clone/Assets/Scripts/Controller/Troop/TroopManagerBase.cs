namespace Controller
{
    public abstract class TroopManagerBase
    {
        public abstract bool Train(Model.BuildingBase building, Model.TroopBase troop);

        public abstract bool MoveTroop(Model.TroopBase troop, Model.TileBase tile);

        public abstract bool Attack(Model.TroopBase attacker, Model.TroopBase target);
    }
}