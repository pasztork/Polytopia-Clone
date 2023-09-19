namespace Controller
{
    public class BuildingManager : BuildingManagerBase
    {
        public override bool Build(Model.TroopBase troop, Model.BuildingBase building)
        {
            return Model.GameManager.Get<Model.BuildManagerBase>().Build(troop, building);
        }

        public override bool Attack(Model.TroopBase troop, Model.BuildingBase building)
        {
            return Model.GameManager.Get<Model.TurnManagerBase>().CurrentPlayer.Attack(troop, building);
        }
    }
}