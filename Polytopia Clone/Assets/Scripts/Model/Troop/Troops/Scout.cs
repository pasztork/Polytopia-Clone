namespace Model
{
    public class Scout : OffensiveLandTroop
    {
        public Scout(Player player) : base()
        {
            initialValues = TroopBase.TroopProperties["Scout"];
            Init(player);
        }

        public override string ToString()
        {
            return "Scout";
        }
    }
}