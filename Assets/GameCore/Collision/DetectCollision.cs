using System;
using System.Collections;
using System.Collections.Generic;
using GabriellChen.SpaceApothecary.Events;
using GameCore.Tags;
using UnityEngine;

namespace GameCore.Collision
{
   [CreateAssetMenu(menuName = "Collision/DetectCollision")]
   public class DetectCollision : ScriptableObject
   {
#if UNITY_EDITOR
      [Multiline]
      public string Description;
#endif

      // These rays should be relative to the object from which they are being cast.
      //   First is origin, second is direction with distance.
      public List<RayPackage2D> CollisionDetectionRays = new List<RayPackage2D>();
      // Tags for type of objects that want to be detected.
      public List<TagObject> CollisionTags = new List<TagObject>();

      // Event for sending that collision has been detected.
      public GameEvent CollisionEvent;

      private bool useEvent;

      private void OnEnable()
      {
         useEvent = CollisionEvent != null;
      }

      public RaycastHit2D DetectCollisionRaycast(Transform casterTransform)
      {
         RaycastHit2D hit = CastRays(casterTransform);

         if (useEvent &&  GameCorePhysics2D.HasHit(hit))
            CollisionEvent.Raise();

         return CastRays(casterTransform);
      }

      public bool IsColliding(Transform casterTransform)
      {
         bool hit = GameCorePhysics2D.HasHit(CastRays(casterTransform));

         if (useEvent && hit)
            CollisionEvent.Raise();

         return hit;
      }

      private RaycastHit2D CastRays(Transform casterTransform)
      {
         float nearestHitDistance = Mathf.Infinity;
         RaycastHit2D outputHit = new RaycastHit2D();

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
#endif

            // If the hit is legit, and it is the shortest hit, make it returnable
            //    All casts are important to prevent biases based on the order of raycasting.
            if (GameCorePhysics2D.HasHit(hit) && hit.distance < nearestHitDistance)
            {
               nearestHitDistance = hit.distance;
               outputHit = hit;
            }
         }

         return outputHit;
      }
   }
}
