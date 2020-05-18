using System;
using System.Collections;
using System.Collections.Generic;
using GameCore.Collision;
using GameCore.Functions;
using UnityEngine;

// Used to move along a particular axis.
[CreateAssetMenu(menuName = "Movement/GeneralMoveEntityDelegate")]
public class MoveEntityOnAxisDelegate : ScriptableObject
{
    public FloatModulator SpeedModulator;

    public void Move(Transform entityTransform, float timeSpentMovingInDirection, DetectCollisionDelegate obstacleDetector, Vector2 directionNormalAlongAxis)
    {
        if (!DirectionNormalAlongAxis(directionNormalAlongAxis))
        {
            throw new Exception("TestingGeneralMoveEntityDelegate: direction vector is not normal and along an axis");
        }

        float speed = SpeedModulator.Output(timeSpentMovingInDirection);

        // Used to make sure the collisions in direction of movement are accurate (ex. won't move through obstacles).
        RaycastHit2D obstacleHit = obstacleDetector.DetectCollisionRaycast(entityTransform);
        if (GameCorePhysics2D.HasHit(obstacleHit) && obstacleHit.distance < speed)
            entityTransform.position += (Vector3) directionNormalAlongAxis * obstacleHit.distance;
        else
            entityTransform.position += (Vector3) directionNormalAlongAxis * speed;
    }

    private bool DirectionNormalAlongAxis(Vector2 direction)
    {
        return direction == Vector2.down ^
               direction == Vector2.left ^
               direction == Vector2.right ^
               direction == Vector2.up;
    }



}
