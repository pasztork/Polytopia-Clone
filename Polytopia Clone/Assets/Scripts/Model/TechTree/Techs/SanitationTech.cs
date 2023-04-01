namespace Model
{
    public class SanitationTech : TechTreeItemBase
    {
        private readonly int healAmount = 1;
        public SanitationTech(string name, Cost cost, string description) : base(name, cost, description)
        {
            HashCode = "Sanitation";
        }

        public override void ActivateEffect(Player player)
        {
            player.BonusProperty.HealAmount = healAmount;
            foreach(TroopBase troop in player.Troops)
            {
                GameManager.Get<TurnManagerBase>().OnTurnStarted += troop.Heal;
            }
        }
    }
}
