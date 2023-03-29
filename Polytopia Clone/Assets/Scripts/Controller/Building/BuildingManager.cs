namespace Controller
{
    public class BuildingManager : BuildingManagerBase
    {
        public override bool Build(Model.TroopBase troop, Model.BuildingBase building)
        {
            return Model.DependencyContainer.Get<Model.BuildManagerBase>().Build(troop, building);
        }

        public override void Attack(Model.TroopBase troop, Model.BuildingBase building)
        {
            Model.DependencyContainer.Get<Model.TurnManagerBase>().CurrentPlayer.Attack(troop, building);
        }
    }
}