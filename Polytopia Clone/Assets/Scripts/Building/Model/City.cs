namespace Model
{
    public class City : BuildingBase
    {
        public override bool TrainTroop(TroopBase troop)
        {
            return Tile.TrainTroop(troop);
        }
    }
}