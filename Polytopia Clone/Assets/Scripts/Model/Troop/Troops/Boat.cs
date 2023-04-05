namespace Model
{
    public class Boat : OffensiveWaterTroop
    {
        public Boat(Player player) : base()
        {
            initialValues = TroopBase.TroopProperties["Boat"];
            Init(player);
        }

        public override string ToString()
        {
            return "Boat";
        }
    }
}
