using System;

namespace Model
{
    public class ResourceContainer
    {
        public event Action Produce;

        public int MoneyCount { get; set; }
        public int MaterialCount { get; set; }
        public int FoodCount { get; set; }

        public int BaseMoneyProduction { set => new MoneyProducer(this, value); }
        public int BaseMaterialProduction { set => new MaterialProducer(this, value); }
        public int BaseFoodProduction { set => new FoodProducer(this, value); }

        public void StartTurn() =>
            Produce?.Invoke();

        public bool HasEnoughFor(Cost cost)
        {
            return
                MoneyCount >= cost.MoneyCost &&
                MaterialCount >= cost.MaterialCost &&
                FoodCount >= cost.FoodCost;
        }

        public static ResourceContainer operator -(ResourceContainer resourceContainer, Cost cost)
        {
            resourceContainer.MoneyCount -= cost.MoneyCost;
            resourceContainer.MaterialCount -= cost.MaterialCost;
            resourceContainer.FoodCount -= cost.FoodCost;
            return resourceContainer;
        }
    }
}
