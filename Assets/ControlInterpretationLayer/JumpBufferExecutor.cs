using System;
using ControlInterpretationLayer.MonoBehaviours;
using GabriellChen.SpaceApothecary.Events;
using GameCore.Variables.Primitives;
using InputLayer;
using UnityEngine;

namespace ControlInterpretationLayer {
    [CreateAssetMenu(menuName = "BufferExecutor/Jump")]
    public class JumpBufferExecutor : InputInterpreter
    {
        public InputBuffer jumpBuffer;
        public BoolReference grounded;
        public BoolReference ceilingHit;
        public FloatReference maxJumpTime;
        public FloatReference jumpSlowdownTime;

        public BoolVariable jumpAvailable;
        public BoolVariable jumpInstruction;

        private float _uninteruptedJumptime = 0;

        public override void Initialize() { }

        public override void Update()
        {
            UpdateJumpInstruction();
        }

        public override void FixedUpdate()
        {
            UpdateJumpAvailability();
        }

        // TODO: fix bug where you can walk off a platform then jump
        private void UpdateJumpAvailability()
        {
            _uninteruptedJumptime += Time.fixedDeltaTime;
            bool jumpTimeAvailable = _uninteruptedJumptime < maxJumpTime.Value + jumpSlowdownTime.Value;
            bool releasedJump = jumpBuffer.IsBufferedInputAvailable(state => state.state == -1);
            bool notPressedJump = jumpBuffer.IsBufferedInputAvailable(state => state.state == 0);

            if (ceilingHit.Value)
            {
                jumpAvailable.SetValue(false);
                return;
            }

            if (grounded.Value && (releasedJump || notPressedJump))
            {
                jumpAvailable.SetValue(true);
                _uninteruptedJumptime = 0;
            }

            if (!jumpTimeAvailable)
                jumpAvailable.SetValue(false);

            if (jumpAvailable.Value && releasedJump)
                jumpAvailable.SetValue(false);
        }

        private void UpdateJumpInstruction()
        {
            if (jumpAvailable.Value && jumpBuffer.IsBufferedInputAvailable(state => state.state > 0))
            {
                jumpInstruction.SetValue(true);
                jumpBuffer.ExecuteBufferOnCondition(state => state.state > 0);
                return;
            }

            jumpInstruction.SetValue(false);
        }
    }
}
