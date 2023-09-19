namespace Model
{
    public class BankingTech : TechTreeItemBase
    {
        public BankingTech() : base()
        {
            Init(Cost.CreateNewFromJsonCost(TechTreeItemBase.ItemCosts["Banking"]));
            HashCode = "Banking";
        }

        public override void ActivateEffect(Player player)
        {
            player.AvailableBuildings.Add("Bank");
        }
    }
}
