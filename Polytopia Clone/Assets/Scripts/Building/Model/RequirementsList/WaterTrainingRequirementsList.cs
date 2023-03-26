using System.Collections.Generic;

namespace Model
{
    public class WaterTrainingRequirementsList : RequirementsListBase
    {
        public override bool RequirementsMet(ResourceContainer resourceContainer, ISet<TileBase> tiles, TileBase tile)
        {
            return resourceContainer.HasEnoughFor(Cost) && WaterBuilderFound && tiles.Contains(tile);
        }
    }
}