namespace Controller
{
    public abstract class BuildingManagerBase
    {
        public abstract bool Build(Model.TroopBase troop, Model.BuildingBase building);

        public abstract void Attack(Model.TroopBase troop, Model.BuildingBase building);
    }
}