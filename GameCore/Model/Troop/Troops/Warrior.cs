namespace Model
{
    public class Warrior : OffensiveLandTroop
    {
        public Warrior(Player player) : base()
        {
            initialValues = TroopBase.TroopProperties["Warrior"];
            Init(player);
        }

        public Warrior() : base()
        {
            initialValues = TroopProperties["Warrior"];
            Init(GameManager.Get<TurnManagerBase>().CurrentPlayer);
        }

        public override string ToString()
        {
            return "Warrior";
        }
    }
}