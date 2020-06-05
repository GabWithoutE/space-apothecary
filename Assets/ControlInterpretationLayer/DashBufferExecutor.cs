using ControlInterpretationLayer.MonoBehaviours;
using GameCore.Variables.Primitives;
using GameCore.Variables.Unity;
using InputLayer;
using UnityEngine;

namespace ControlInterpretationLayer
{
    [CreateAssetMenu(menuName = "BufferExecutor/SecondaryAbility")]
    public class DashBufferExecutor : InputInterpreter
    {
        public InputBuffer secondaryAbilityBuffer;

        public BoolVariable dashing;
        public BoolReference grounded;
        public FloatReference blockingTime;
        public BoolReference ceilingHit;

        public Vector3Reference playerPosition;
        public Vector3Variable secondaryAbilityDirection;
        public CameraVariable cameraVariable;

        private bool _dashAvailable = false;

        public override void Initialize() { }

        public override void Update()
        {
            if (grounded.Value)
                _dashAvailable = true;

            if (secondaryAbilityBuffer.IsBlocked())
            {
                if (ceilingHit.Value && dashing.Value)
                {
                    _dashAvailable = false;
                    dashing.SetValue(false);
                }
                return;
            }

            if (_dashAvailable && secondaryAbilityBuffer.IsBufferedInputAvailable(state => state.state == 1))
            {
                secondaryAbilityBuffer.BlockExecution(blockingTime);
                secondaryAbilityBuffer.ExecuteBufferOnCondition(state => state.state == 1);

                dashing.SetValue(true);

                // Here if hit an npc, don't do primary ability
                Vector2 playerToMouseVector = GetPlayerToMouseVector();
                _dashAvailable = false;

                if (grounded.Value)
                {
                    secondaryAbilityDirection.SetValue(
                        GetNearestHorizontalDirection(playerToMouseVector.x)
                    );
                    return;
                }

                secondaryAbilityDirection.SetValue(playerToMouseVector.normalized);
                return;
            }

            dashing.SetValue(false);
        }

        public override void FixedUpdate() { }

        private Vector2 GetPlayerToMouseVector()
        {
            return Input.mousePosition - cameraVariable.Value.WorldToScreenPoint(playerPosition.Value);
        }

        private Vector2 GetNearestHorizontalDirection(float x)
        {
            return new Vector2(x > 0 ? 1 : -1, 0);
        }


    }
}
