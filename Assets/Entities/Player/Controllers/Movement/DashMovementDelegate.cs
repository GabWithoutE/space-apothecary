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
        public Vector3Reference dashDirection;

        public DetectCollisionDelegate xObstacleDetector;
        public DetectCollisionDelegate groundObstacleDetector;
        public DetectCollisionDelegate ceilingObstacleDetector;

        public void Move(Transform entityTransform, float currentDashingTime)
        {
            float dashSpeed = dashSpeedModulator.Output(currentDashingTime);
            Vector2 movementVector = dashDirection.Value * dashSpeed;

            float xDashVelocity = movementVector.x;
            float yDashVelocity = movementVector.y;

            movementVector.x = SpeedToNotClipObstacle(
                entityTransform,
                xObstacleDetector,
                xDashVelocity < 0,
                xDashVelocity);

            movementVector.y = SpeedToNotClipObstacle(
                entityTransform,
                movementVector.y < 0 ? groundObstacleDetector : ceilingObstacleDetector,
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
