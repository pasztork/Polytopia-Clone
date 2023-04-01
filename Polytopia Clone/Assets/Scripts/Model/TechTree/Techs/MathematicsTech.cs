namespace Model
{
    public class MathematicsTech : TechTreeItemBase
    {
        private readonly float buildingCostDiscount = 0.2f;
        public MathematicsTech(Cost cost) : base(cost)
        {
            HashCode = "Mathematics";
        }

        public override void ActivateEffect(Player player)
        {
            player.BonusProperty.BuildingDiscount += buildingCostDiscount;
        }
    }
}
