using ControlInterpretationLayer.MonoBehaviours;
using GameCore.Variables.Primitives;
using GameCore.Variables.Unity;
using InputLayer;
using UnityEngine;

namespace ControlInterpretationLayer
{
    [CreateAssetMenu(menuName = "BufferExecutor/PrimaryAbility")]
    public class PrimaryAbilityBufferExecutor : BufferExecutor
    {
        public InputBuffer primaryAbilityBuffer;
        public InputBuffer rightMovementBuffer;
        public InputBuffer leftMovementBuffer;

        public BoolVariable attacking;
        public BoolReference grounded;
        public IntReference numberOfFramesForAbility;

        public Vector3Reference playerPosition;
        public Vector3Variable primaryAbilityDirection;
        public CameraVariable cameraVariable;

        public override void Initialize() { }

        public override void Update()
        {
            // TODO: Potential issue to solve here is that if second attack is retriggered too quickly, maybe the
            //     it's downtime wont be detected by dependent listeners?
            // also, don't actually want to block this way. example, dash can cancel attack animation?
            if (primaryAbilityBuffer.IsBlocked())
                return;

            if (primaryAbilityBuffer.IsBufferedInputAvailable(state => state.state == 1))
            {
                // if the player is grounded, block right and left movement input, to allow the ability's movement
                //     control to take over unimpeded.
                if (grounded.Value)
                {
                    rightMovementBuffer.BlockExecution(numberOfFramesForAbility);
                    leftMovementBuffer.BlockExecution(numberOfFramesForAbility);
                }

                // block self for specified number of frames to prevent re-triggering before desired
                primaryAbilityBuffer.BlockExecution(numberOfFramesForAbility);
                primaryAbilityBuffer.ExecuteBufferOnCondition(state => state.state == 1);

                Vector2 playerToMouseVector =
                    Input.mousePosition -
                    cameraVariable.Value.WorldToScreenPoint(playerPosition.Value);
                float x = playerToMouseVector.x;
                float y = playerToMouseVector.y;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                    primaryAbilityDirection.SetValue(new Vector2(x > 0 ? 1 : -1, 0));
                else
                    primaryAbilityDirection.SetValue(new Vector2(0, y > 0 ? 1 : -1));

                attacking.SetValue(true);
                return;
            }

            attacking.SetValue(false);
        }

        private RaycastHit2D CastRayFromMouseToWorld()
        {
            Ray ray = cameraVariable.Value.ScreenPointToRay(playerPosition.Value);
            return Physics2D.GetRayIntersection(
                ray,
                Mathf.Abs(cameraVariable.Value.transform.position.z * 2),
                LayerMask.GetMask("NPC")
            );
        }
    }
}
