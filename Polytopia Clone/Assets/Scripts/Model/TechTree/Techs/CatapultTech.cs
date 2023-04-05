namespace Model
{
    public class CatapultTech : TechTreeItemBase
    {
        public CatapultTech() : base()
        {
            Init(Cost.CreateNewFromJsonCost(TechTreeItemBase.ItemCosts["Catapult"]));
            HashCode = "Catapult";
        }

        public override void ActivateEffect(Player player)
        {
            player.AvailableTroops.Add("Catapult");
        }
    }
}
