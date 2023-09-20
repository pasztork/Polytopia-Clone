using System.Collections.Generic;

namespace Model
{
    public class TrainingRequirementsList : RequirementsListBase
    {
        public override bool RequirementsMet(ResourceContainer resourceContainer, float buildingDiscount, ISet<TileBase> occupiedTiles, ISet<TileBase> playerTiles, TileBase tile)
        {
            return resourceContainer.HasEnoughFor(Cost * (1f - buildingDiscount)) && TrainingBuilderFound && !occupiedTiles.Contains(tile);
        }
    }
}