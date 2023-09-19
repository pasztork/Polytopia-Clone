namespace Controller
{
    public abstract class BuildingManagerBase
    {
        public abstract bool Build(Model.TroopBase troop, Model.BuildingBase building);

        public abstract bool Attack(Model.TroopBase troop, Model.BuildingBase building);
    }
}