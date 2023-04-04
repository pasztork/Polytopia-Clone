namespace Model
{
    public class Cost
    {
        public int MoneyCost { get; set; }
        public int MaterialCost { get; set; }
        public int FoodCost { get; set; }

        public Cost(int moneyCost, int materialCost, int foodCost)
        {
            MoneyCost = moneyCost;
            MaterialCost = materialCost;
            FoodCost = foodCost;
        }

        public static Cost operator *(Cost c, float f)
        {
            return new Cost((int)(c.MoneyCost * f), (int)(c.MaterialCost * f), (int)(c.FoodCost * f));
        }

        public static Cost CreateNewFromJsonCost(JsonCost cost)
        {
            return new Cost(cost.Money, cost.Material, cost.Food);
        }
    }
}
