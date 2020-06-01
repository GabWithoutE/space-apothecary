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
        public FloatReference dashSpeed;
        public Vector3Reference dashDirection;
        public IntReference numberOfFrames;

        public DetectCollisionDelegate xObstacleDetector;
        public DetectCollisionDelegate yObstacleDetector;

        public void Move(Transform entityTransform, int currentFrame)
        {
            if (currentFrame < numberOfFrames)
            {
                Vector2 movementVector = dashDirection.Value * dashSpeed;
                float xDashSpeed = movementVector.x;
                float yDashSpeed = movementVector.y;

                movementVector.x = SpeedToNotClipObstacle(
                    entityTransform,
                    xObstacleDetector,
                    xDashSpeed < 0,
                    xDashSpeed);

                movementVector.y = SpeedToNotClipObstacle(
                    entityTransform,
                    yObstacleDetector,
                    false,
                    yDashSpeed);

                entityTransform.position += (Vector3) movementVector;
            }
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
            {
                return obstacleHit.distance;
            }

            return velocityInCollisionDirection;
        }
    }
}
