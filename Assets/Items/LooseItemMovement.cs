﻿using System.Collections;
using System.Collections.Generic;
using GameCore.Collision;
using UnityEngine;

[CreateAssetMenu(menuName = "Movement/LooseItem")]
public class LooseItemMovement : MoveEntityDelegate
{
    public MoveEntityOnAxisDelegate GravityMovementDelegate;
    public DetectCollisionDelegate GroundLocationDelegate;
    public DetectCollisionDelegate OnGroundDetectionDelegate;
    public override void Move(Transform entityTransform, float timeModifier)
    {
        float time = 0.1f;
        GravityMovementDelegate.Move(entityTransform, time, GroundLocationDelegate, Vector2.down);
    }
}
