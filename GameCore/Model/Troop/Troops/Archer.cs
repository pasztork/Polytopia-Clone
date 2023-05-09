namespace Model
{
    public class Archer : OffensiveLandTroop
    {
        public Archer(Player player) : base()
        {
            initialValues = TroopBase.TroopProperties["Archer"];
            Init(player);
        }

        public Archer() : base()
        {
            initialValues = TroopProperties["Archer"];
            Init(GameManager.Get<TurnManagerBase>().CurrentPlayer);
        }

        public override string ToString()
        {
            return "Archer";
        }
    }
}