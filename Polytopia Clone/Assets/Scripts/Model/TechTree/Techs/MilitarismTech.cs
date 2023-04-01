namespace Model
{
    public class MilitarismTech : TechTreeItemBase
    {
        private readonly int offensiveDamageBonus = 1;
        public MilitarismTech(Cost cost) : base(cost)
        {
            HashCode = "Militarism";
        }

        public override void ActivateEffect(Player player)
        {
            player.BonusProperty.OffensiveDamageBonus += offensiveDamageBonus;
            foreach (TroopBase troop in player.Troops)
            {
                if (troop.TroopProperty.Damage != 0)
                    troop.TroopProperty.Damage += offensiveDamageBonus;
            }
        }
    }
}
