namespace Model
{
    public class StrategyTech : TechTreeItemBase
    {
        private readonly double dodgeBonus = 0.1;

        public StrategyTech() : base()
        {
            Init(Cost.CreateNewFromJsonCost(TechTreeItemBase.ItemCosts["Strategy"]));
            HashCode = "Strategy";
        }

        public override void ActivateEffect(Player player)
        {
            player.BonusProperty.DodgeBonus = dodgeBonus;
            foreach (TroopBase troop in player.Troops)
            {
                troop.TroopProperty.DodgeRate += dodgeBonus;
            }
        }
    }
}
