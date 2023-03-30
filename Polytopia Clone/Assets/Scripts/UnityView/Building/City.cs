using UnityEngine;

namespace View
{
    public class City : BuildingBase
    {
        [SerializeField] private int range;

        public override Model.BuildingBase ToModel(Model.Player player)
        {
            Model.BuildingBase city = new Model.City(range);
            city.OnDamageTaken += TakeDamage;
            city.BuildingProperty = new Model.BuildingProperty(buildingProperties.Health);
            city.Producers.Add(new Model.MoneyProducer(player.ResourceContainer, productionRate));
            city.Producers.Add(new Model.MaterialProducer(player.ResourceContainer, productionRate));
            city.Producers.Add(new Model.FoodProducer(player.ResourceContainer, productionRate));
            city.Cost = new Model.Cost(cost.MoneyCost, cost.MaterialCost, cost.FoodCost);
            return city;
        }
    }
}