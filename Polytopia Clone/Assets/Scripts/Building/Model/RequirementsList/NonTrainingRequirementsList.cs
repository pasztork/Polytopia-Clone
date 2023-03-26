using System.Collections.Generic;

namespace Model
{
    public class NonTrainingRequirementsList : RequirementsListBase
    {
        public override bool RequirementsMet(ResourceContainer resourceContainer, ISet<TileBase> tiles, TileBase tile)
        {
            return resourceContainer.HasEnoughFor(Cost) && NonTrainingBuilderFound && tiles.Contains(tile);
        }
    }
}
