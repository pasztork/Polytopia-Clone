using System.Collections.Generic;

namespace Model
{
    public class WaterTrainingRequirementsList : RequirementsListBase
    {
        public override bool RequirementsMet(ResourceContainer resourceContainer, float buildingDiscount, ISet<TileBase> occupiedTiles, ISet<TileBase> playerTiles, TileBase tile)
        {
            return resourceContainer.HasEnoughFor(Cost * (1f - buildingDiscount)) && WaterBuilderFound && playerTiles.Contains(tile);
        }
    }
}