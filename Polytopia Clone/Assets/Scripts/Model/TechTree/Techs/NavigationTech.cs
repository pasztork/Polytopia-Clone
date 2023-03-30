namespace Model
{
    public class NavigationTech : TechTreeItemBase
    {
        public NavigationTech(string name, Cost cost, string description) : base(name, cost, description)
        {
            HashCode = "Navigation";
        }

        public override void ActivateEffect(Player player)
        {
            player.TroopBonus.WaterMoveBonus += 1;
            foreach(TroopBase troop in player.Troops)
            {
                troop.ApplyPropertyBonus(player);
            }
        }
    }
}
