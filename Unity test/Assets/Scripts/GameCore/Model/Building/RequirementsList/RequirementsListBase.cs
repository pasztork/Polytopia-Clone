using System.Collections.Generic;

namespace Model
{
    public abstract class RequirementsListBase
    {
        public bool TrainingBuilderFound { protected get; set; } = false;
        public bool NonTrainingBuilderFound { protected get; set; } = false;
        public bool WaterBuilderFound { protected get; set; } = false;
        public Cost Cost { protected get; set; } = null;

        public abstract bool RequirementsMet(
            ResourceContainer resourceContainer,
            float buildingDiscount,
            ISet<TileBase> occupiedTiles,
            ISet<TileBase> playerTiles,
            TileBase tile);
    }
}