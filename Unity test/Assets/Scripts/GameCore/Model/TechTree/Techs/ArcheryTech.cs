namespace Model
{
    public class ArcheryTech : TechTreeItemBase
    {
        public ArcheryTech() : base()
        {
            Init(Cost.CreateNewFromJsonCost(TechTreeItemBase.ItemCosts["Archery"]));
            HashCode = "Archery";
        }

        public override void ActivateEffect(Player player)
        {
            player.AvailableTroops.Add("Archer");
        }
    }
}
