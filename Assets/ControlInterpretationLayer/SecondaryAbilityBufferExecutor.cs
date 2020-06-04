using ControlInterpretationLayer.MonoBehaviours;
using GameCore.Variables.Primitives;
using GameCore.Variables.Unity;
using InputLayer;
using UnityEngine;

namespace ControlInterpretationLayer
{
    [CreateAssetMenu(menuName = "BufferExecutor/SecondaryAbility")]
    public class SecondaryAbilityBufferExecutor : InputInterpreter
    {
        public InputBuffer secondaryAbilityBuffer;
        public InputBuffer leftBuffer;
        public InputBuffer rightBuffer;

        public BoolVariable dashing;
        public BoolReference grounded;
        public FloatReference blockingTime;

        public Vector3Reference playerPosition;
        public Vector3Variable secondaryAbilityDirection;
        public CameraVariable cameraVariable;

        private bool _jumpAvailable = false;

        public override void Initialize() { }

        public override void Update()
        {
            if (grounded.Value)
                _jumpAvailable = true;

            if (secondaryAbilityBuffer.IsBlocked())
            {
                if (grounded.Value)
                {
                    Vector2 playerToMouseVector = GetPlayerToMouseVector();
                    secondaryAbilityDirection.SetValue(
                        GetNearestHorizontalDirection(playerToMouseVector.x)
                    );
                }
                // TODO: Here if hit an npc, don't do primary ability
                return;
            }

            if (_jumpAvailable && secondaryAbilityBuffer.IsBufferedInputAvailable(state => state.state == 1))
            {
                secondaryAbilityBuffer.BlockExecution(blockingTime);
                secondaryAbilityBuffer.ExecuteBufferOnCondition(state => state.state == 1);

                rightBuffer.BlockExecution(blockingTime);
                leftBuffer.BlockExecution(blockingTime);

                dashing.SetValue(true);

                // Here if hit an npc, don't do primary ability
                Vector2 playerToMouseVector = GetPlayerToMouseVector();
                _jumpAvailable = false;

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
