namespace View
{
    public class City : BuildingBase
    {
        public override Model.BuildingBase ToModel(Model.Player player)
        {
            Model.BuildingBase city = new Model.City(player);
            city.OnDamageTaken += TakeDamage;
            return city;
        }
    }
}