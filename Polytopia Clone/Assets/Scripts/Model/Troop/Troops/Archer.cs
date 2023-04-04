namespace Model
{
    public class Archer : OffensiveLandTroop
    {
        public Archer(Player player) : base()
        {
            initialValues = TroopBase.TroopProperties.Archer;
            Init(player);
        }

        public override string ToString()
        {
            return "Archer";
        }
    }
}