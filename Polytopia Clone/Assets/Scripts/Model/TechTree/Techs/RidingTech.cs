namespace Model
{
    public class RidingTech : TechTreeItemBase
    {
        private readonly int offensiveLandMoveBonus = 1;
        public RidingTech(string name, Cost cost, string description) : base(name, cost, description)
        {
            HashCode = "Riding";
        }

        public override void ActivateEffect(Player player)
        {
            player.BonusProperty.OffensiveLandMoveBonus += offensiveLandMoveBonus;
            foreach(TroopBase troop in player.Troops)
            {
                troop.OffensiveLandMovementRangeBonus(player);
            }
        }
    }
}
