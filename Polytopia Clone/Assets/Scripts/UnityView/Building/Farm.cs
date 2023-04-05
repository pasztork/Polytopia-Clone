namespace View
{
    public class Farm : BuildingBase
    {
        public override Model.BuildingBase ToModel(Model.Player player)
        {
            Model.BuildingBase farm = new Model.Farm(player);
            farm.OnDamageTaken += TakeDamage;
            return farm;
        }
    }
}