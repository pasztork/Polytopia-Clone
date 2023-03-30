namespace Model
{
    public class RidingTech : TechTreeItemBase
    {
        public RidingTech(string name, Cost cost, string description) : base(name, cost, description)
        {
            HashCode = "Riding";
        }

        public override void ActivateEffect(Player player)
        {
            player.TroopBonus.OffensiveLandMoveBonus += 1;
            foreach(TroopBase troop in player.Troops)
            {
                troop.ApplyPropertyBonus(player);
            }
        }
    }
}
