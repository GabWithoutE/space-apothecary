using System.Collections;
using System.Collections.Generic;
using GameCore.Tags;
using UnityEngine;

namespace GameCore.Collision
{
   [CreateAssetMenu(menuName = "Collision/DetectCollision")]
   public class DetectCollision : ScriptableObject
   {
      // These rays should be relative to the object from which they are being cast.
      //   First is origin, second is direction with distance.
      public List<RayPackage2D> CollisionDetectionRays = new List<RayPackage2D>();
      // Tags for type of objects that want to be detected.
      public List<TagObject> CollisionTags = new List<TagObject>();

      public RaycastHit2D DetectCollisionRaycast(Transform casterTransform)
      {
         return CastRays(casterTransform);
      }

      public bool IsColliding(Transform casterTransform)
      {
         return GameCorePhysics2D.HasHit(CastRays(casterTransform));
      }

      protected RaycastHit2D CastRays(Transform casterTransform)
      {
         for (int i = 0; i < CollisionDetectionRays.Count; i++)
         {
            RayPackage2D ray = CollisionDetectionRays[i];

            RaycastHit2D hit = GameCorePhysics2D.RayCast(
               (Vector2) casterTransform.position + ray.OriginPosition,
               ray.Direction,
               ray.Distance,
               LayerMask.GetMask("Raycast"),
               CollisionTags
            );

#if UNITY_EDITOR
            Debug.DrawRay(
               (Vector2) casterTransform.position + ray.OriginPosition,
               ray.Direction * ray.Distance,
               Color.red
            );

            // Debug.Log(hit.transform.name);
#endif

            // check if the hit is legit, if it is, return it.
            if (GameCorePhysics2D.HasHit(hit))
            {
               return hit;
            }
         }

         return new RaycastHit2D();
      }
   }
}
