using GameCore.Collision;
using GameCore.Functions;
using GameCore.Variables.Primitives;
using GameCore.Variables.Unity;
using UnityEngine;

namespace Entities.Player.Controllers.Movement
{
    [CreateAssetMenu(menuName = "Movement/DashMovement")]
    public class DashMovementDelegate : ScriptableObject
    {
        public FloatModulator dashSpeedModulator;
        public FloatReference slowestXSpeed;
        public FloatReference gravitySpeed;
        public Vector3Reference dashDirection;

        public DetectCollisionDelegate xObstacleDetector;
        public DetectCollisionDelegate yObstacleDetector;

        public void Move(Transform entityTransform, float currentDashingTime)
        {
            float dashSpeed = dashSpeedModulator.Output(currentDashingTime);
            Vector2 movementVector = dashDirection.Value * dashSpeed;

            // TODO: make it so that dash transitions smoothly into horizontal movement and otherwise.
            // if (Mathf.Abs(movementVector.x) < slowestXSpeed)
            //     if (movementVector.x < 0)
            //         movementVector.x = -slowestXSpeed;
            //     else
            //         movementVector.x = slowestXSpeed;
            //
            // if (movementVector.y < 0 && movementVector.y > gravitySpeed)
            //     movementVector.y = gravitySpeed;


            float xDashVelocity = movementVector.x;
            float yDashVelocity = movementVector.y;

            movementVector.x = SpeedToNotClipObstacle(
                entityTransform,
                xObstacleDetector,
                xDashVelocity < 0,
                xDashVelocity);

            movementVector.y = SpeedToNotClipObstacle(
                entityTransform,
                yObstacleDetector,
                false,
                yDashVelocity);

            entityTransform.position += (Vector3) movementVector;
        }

        private float SpeedToNotClipObstacle(
            Transform entityTransform,
            DetectCollisionDelegate collisionDelegate,
            bool flipDirectionOfDelegate,
            float velocityInCollisionDirection
            )
        {
            RaycastHit2D obstacleHit = collisionDelegate.DetectCollisionRaycast(
                entityTransform,
                flipDirectionOfDelegate
                );

            bool hasHit = GameCorePhysics2D.HasHit(obstacleHit);

            if (hasHit && obstacleHit.distance < Mathf.Abs(velocityInCollisionDirection))
                return obstacleHit.distance;

            return velocityInCollisionDirection;
        }
    }
}
