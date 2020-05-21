using System.Collections;
using System.Collections.Generic;
using GameCore.Collision;
using UnityEngine;

[CreateAssetMenu(menuName = "Movement/LooseItem")]
public class LooseItemMovement : MoveEntityDelegate
{
    public MoveEntityOnAxisDelegate GravityMovementDelegate;
    public DetectCollisionDelegate GroundLocationDelegate;
    public override void Move(Transform entityTransform, float timeModifier)
    {
        GravityMovementDelegate.Move(entityTransform, 0.1f, GroundLocationDelegate, Vector2.down);
    }
}
