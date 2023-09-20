using System.Collections.Generic;

namespace Model
{
    public class NonTrainingRequirementsList : RequirementsListBase
    {
        public override bool RequirementsMet(ResourceContainer resourceContainer, float buildingDiscount, ISet<TileBase> occupiedTiles, ISet<TileBase> playerTiles, TileBase tile)
        {
            return resourceContainer.HasEnoughFor(Cost * (1f - buildingDiscount)) && NonTrainingBuilderFound && playerTiles.Contains(tile);
        }
    }
}
