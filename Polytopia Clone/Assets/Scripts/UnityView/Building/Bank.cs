namespace View
{
    public class Bank : BuildingBase
    {
        public override Model.BuildingBase ToModel(Model.Player player)
        {
            Model.BuildingBase bank = new Model.Bank(player);
            bank.OnDamageTaken += TakeDamage;
            return bank;
        }
    }
}