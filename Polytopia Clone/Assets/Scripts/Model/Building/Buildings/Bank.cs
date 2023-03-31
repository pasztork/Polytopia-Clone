namespace Model
{
    public class Bank : NonTrainingBuilding
    {
        public override void IncreaseMoneyProduction(int amount)
        {
            foreach(ProducerBase producer in Producers)
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
