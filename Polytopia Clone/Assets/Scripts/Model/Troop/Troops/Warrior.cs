namespace Model
{
    public class Warrior : OffensiveLandTroop
    {
        public Warrior(Player player) : base()
        {
            initialValues = TroopBase.TroopProperties["Warrior"];
            Init(player);
        }

        public override string ToString()
        {
            return "Warrior";
        }
    }
}