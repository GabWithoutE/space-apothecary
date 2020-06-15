using System;
using System.Collections;
using System.Collections.Generic;
using GabriellChen.SpaceApothecary.Events;
using GameCore.Tags;
using UnityEngine;

namespace GameCore.Collision
{
   [CreateAssetMenu(menuName = "Collision/DetectCollisionDelegate")]
   public class DetectCollisionDelegate : ScriptableObject
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
      private bool useEvent;
      public GameEvent CollisionEvent;


      private void OnEnable()
      {
         useEvent = CollisionEvent != null;
      }

      public RaycastHit2D DetectCollisionRaycast(Transform casterTransform, bool flipDirection, string maskName = "Raycast")
      {
         RaycastHit2D hit = CastRays(casterTransform, flipDirection, maskName);

         if (useEvent &&  GameCorePhysics2D.HasHit(hit))
            CollisionEvent.Raise();

         return hit;
      }

      public bool IsColliding(Transform casterTransform, bool flipDirection, string maskName = "Raycast")
      {
         bool hit = GameCorePhysics2D.HasHit(CastRays(casterTransform, flipDirection, maskName));

         if (useEvent && hit)
            CollisionEvent.Raise();

         return hit;
      }

      private RaycastHit2D CastRays(Transform casterTransform, bool flipDirection, string maskName)
      {
         float nearestHitDistance = Mathf.Infinity;
         RaycastHit2D outputHit = new RaycastHit2D();

         for (int i = 0; i < CollisionDetectionRays.Count; i++)
         {
            RayPackage2D ray;
            if (flipDirection)
               ray = CollisionDetectionRays[i].FlipAcrossDirectionAxis();
            else
               ray = CollisionDetectionRays[i];

            RaycastHit2D hit = GameCorePhysics2D.RayCast(
               (Vector2) casterTransform.position + ray.OriginPosition,
               ray.Direction,
               ray.Distance,
               LayerMask.GetMask(maskName),
               CollisionTags
            );

            // TODO: Use layers instead of tags? or use RayCast all?

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
