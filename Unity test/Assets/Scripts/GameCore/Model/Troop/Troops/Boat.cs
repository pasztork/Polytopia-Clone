namespace Model
{
    public class Boat : OffensiveWaterTroop
    {
        public Boat(Player player) : base()
        {
            initialValues = TroopBase.TroopProperties["Boat"];
            Init(player);
        }

        public Boat() : base()
        {
            initialValues = TroopProperties["Boat"];
            Init(GameManager.Get<TurnManagerBase>().CurrentPlayer);
        }

        public override string ToString()
        {
            return "Boat";
        }
    }
}
