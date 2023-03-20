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
    }
}
