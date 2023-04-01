namespace Model
{
    public class MathematicsTech : TechTreeItemBase
    {
        private readonly float buildingCostDiscount = 0.2f;
        public MathematicsTech(string name, Cost cost, string description) : base(name, cost, description)
        {
            HashCode = "Mathematics";
        }

        public override void ActivateEffect(Player player)
        {
            player.BonusProperty.BuildingDiscount += buildingCostDiscount;
        }
    }
}
