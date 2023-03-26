using System.Collections.Generic;

namespace Model
{
    public class TrainingRequirementsList : RequirementsListBase
    {
        public override bool RequirementsMet(ResourceContainer resourceContainer, ISet<TileBase> tiles, TileBase tile)
        {
            return resourceContainer.HasEnoughFor(Cost) && TrainingBuilderFound && !tiles.Contains(tile);
        }
    }
}