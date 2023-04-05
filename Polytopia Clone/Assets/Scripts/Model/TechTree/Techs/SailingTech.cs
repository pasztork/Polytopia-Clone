namespace Model
{
    public class SailingTech : TechTreeItemBase
    {
        public SailingTech() : base()
        {
            Init(Cost.CreateNewFromJsonCost(TechTreeItemBase.ItemCosts["Sailing"]));
            HashCode = "Sailing";
        }

        public override void ActivateEffect(Player player)
        {
            player.AvailableTroops.Add("Boat");
        }
    }
}
