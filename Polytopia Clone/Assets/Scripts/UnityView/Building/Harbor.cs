namespace View
{
    public class Harbor : BuildingBase
    {
        public override Model.BuildingBase ToModel(Model.Player player)
        {
            Model.BuildingBase harbor = new Model.Harbor(player);
            harbor.OnDamageTaken += TakeDamage;
            return harbor;
        }
    }
}
