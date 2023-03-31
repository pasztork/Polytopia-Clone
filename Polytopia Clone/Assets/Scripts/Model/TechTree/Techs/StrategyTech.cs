namespace Model
{
    public class StrategyTech : TechTreeItemBase
    {
        private double dodgeBonus = 0.1;
        public StrategyTech(string name, Cost cost, string description) : base(name, cost, description) 
        {
            HashCode = "Strategy";
        }

        public override void ActivateEffect(Player player)
        {
            player.TroopBonus.DodgeBonus = dodgeBonus;
            foreach(TroopBase troop in player.Troops)
            {
                troop.TroopProperty.DodgeRate += dodgeBonus;
            }
        }
    }
}
