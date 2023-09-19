namespace Model
{
    public class Scout : OffensiveLandTroop
    {
        public Scout(Player player) : base()
        {
            initialValues = TroopBase.TroopProperties["Scout"];
            Init(player);
        }

        public Scout() : base()
        {
            initialValues = TroopProperties["Scout"];
            Init(GameManager.Get<TurnManagerBase>().CurrentPlayer);
        }

        public override string ToString()
        {
            return "Scout";
        }
    }
}