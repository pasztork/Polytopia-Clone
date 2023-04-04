namespace Model
{
    public class Bank : NonTrainingBuilding
    {

        public Bank(Player player) : base()
        {
            initialValues = BuildingProperties.Bank;
            Init(player);
        }

        public override void IncreaseMoneyProduction(int amount)
        {
            foreach (ProducerBase producer in Producers)
            {
                producer.IncreaseProduction(amount);
            }
        }

        public override string ToString()
        {
            return "Bank";
        }

    }
}
