namespace ReplayView
{
    public class Supplier : BuildingBase
    {
        public override Model.BuildingBase ToModel(Model.Player player)
        {
            Model.BuildingBase supplier = new Model.Supplier(player);
            supplier.OnDamageTaken += TakeDamage;
            return supplier;
        }
    }
}