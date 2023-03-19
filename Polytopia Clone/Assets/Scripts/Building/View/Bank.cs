namespace View
{
    public class Bank : BuildingBase
    {
        public override Model.BuildingBase ToModel(Model.Player player)
        {
            Model.BuildingBase bank = new Model.Bank();
            bank.Producers.Add(new Model.MoneyProducer(player.ResourceContainer, productionRate));
            return bank;
        }
    }
}