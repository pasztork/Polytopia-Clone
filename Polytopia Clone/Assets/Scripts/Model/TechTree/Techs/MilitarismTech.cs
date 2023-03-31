namespace Model
{
    public class MilitarismTech : TechTreeItemBase
    {
        private int offensiveDamageBonus = 1;
        public MilitarismTech(string name, Cost cost, string description) : base(name, cost, description)
        {
            HashCode = "Militarism";
        }

        public override void ActivateEffect(Player player)
        {
            player.TroopBonus.OffensiveDamageBonus += offensiveDamageBonus;
            foreach (TroopBase troop in player.Troops)
            {
                if (troop.TroopProperty.Damage != 0)
                    troop.TroopProperty.Damage += offensiveDamageBonus;
            }
        }
    }
}
