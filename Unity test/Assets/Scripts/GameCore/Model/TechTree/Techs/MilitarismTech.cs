namespace Model
{
    public class MilitarismTech : TechTreeItemBase
    {
        private readonly int offensiveDamageBonus = 1;

        public MilitarismTech() : base()
        {
            Init(Cost.CreateNewFromJsonCost(TechTreeItemBase.ItemCosts["Militarism"]));
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
