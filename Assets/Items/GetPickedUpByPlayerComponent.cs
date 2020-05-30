using System;
using GameCore.Collision;
using UnityEngine;

public class GetPickedUpByPlayerComponent : MonoBehaviour
{
    public Item Item;

    void FixedUpdate()
    {
        RaycastHit2D hit = Item.CollisionDelegate.DetectCollisionRaycast(transform, false);
        if (GameCorePhysics2D.HasHit(hit))
        {
            Item.GetPickedUp(hit.transform);
            DestroyImmediate(gameObject);
        }
    }

}
