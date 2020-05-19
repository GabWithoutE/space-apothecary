using System.Collections.Generic;
using System.Linq;
using GameCore.Tags;
using UnityEngine;

namespace GameCore.Collision
{
    public static class GameCorePhysics2D
    {
        // Returns list of RaycastHit2D that match the input hitTags
        public static RaycastHit2D RayCast(
            Vector2 origin,
            Vector2 direction,
            float distance,
            LayerMask layerMask,
            List<TagObject> hitTags
        )
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, layerMask);
            // If there's no hit, stop here and return the empty hit.
            if (!HasHit(hit))
                return hit;

            // If not hitTags are specified, then just return anything that is hit.
            if (hitTags == null || hitTags.Count == 0)
                return hit;

            // Basically try to prevent getting to here if not needed, because this is expensive.
            TagsComponent tagsComponent = hit.transform.GetComponent<TagsComponent>();

            // If there is no tag component in the hit, then return an empty RaycastHit2D
            if (!tagsComponent || tagsComponent.Tags.Count == 0)
                return new RaycastHit2D();

            // If there tag matches in the hit, then return an empty RaycastHit2D
            if (!HasAnyOfTags(hitTags, tagsComponent.Tags))
                return new RaycastHit2D();

            return hit;
        }

        public static bool HasHit(RaycastHit2D raycastHit)
        {
            try
            {
                string _ = raycastHit.transform.name;
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool HasAnyOfTags(List<TagObject> reference, List<TagObject> test)
        {
            return reference.Intersect(test).Count() > 0;
        }
    }
}
