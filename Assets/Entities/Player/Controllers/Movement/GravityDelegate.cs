using System;
using System.Collections;
using System.Collections.Generic;
using GameCore.Collision;
using GameCore.Functions;
using GameCore.Variables.Primitives;
using UnityEngine;

[CreateAssetMenu(menuName = "Movement/GravityDelegate")]
public class GravityDelegate : MoveEntityDelegate
{
    public FloatModulator DampenedSpeedCurve;
    public DetectCollision GroundLocationDetector;

    public override void Move(Transform entityTransform, float timeSpentFalling)
    {
        float fallSpeed = DampenedSpeedCurve.Output(timeSpentFalling);

        // If the groundLocation is closer than the fallSpeed, don't move by fallspeed, otherwise the entity
        //     will go through the ground. Instead move by the fall distance.
        RaycastHit2D groundLocationHit = GroundLocationDetector.DetectCollisionRaycast(entityTransform);
        if (GameCorePhysics2D.HasHit(groundLocationHit) && groundLocationHit.distance < fallSpeed)
            entityTransform.position += new Vector3(0, -groundLocationHit.distance);
        else
            entityTransform.position += new Vector3(0, -fallSpeed);
    }
}
