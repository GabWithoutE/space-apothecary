using System;
using ControlInterpretationLayer.MonoBehaviours;
using GabriellChen.SpaceApothecary.Events;
using GameCore.Variables.Primitives;
using InputLayer;
using UnityEngine;

namespace ControlInterpretationLayer {
    [CreateAssetMenu(menuName = "BufferExecutor/Jump")]
    public class JumpBufferExecutor : BufferExecutor
    {
        public InputBuffer jumpBuffer;
        public BoolReference grounded;
        public FloatReference maxJumpTime;
        public FloatReference jumpSlowdownTime;

        public BoolVariable jumpAvailable;
        public BoolVariable jumpInstruction;

        private float _uninteruptedJumptime = 0;

        public override void Initialize() { }

        public override void Update()
        {
            UpdateJumpAvailability();
            UpdateJumpInstruction();
        }

        private void UpdateJumpAvailability()
        {
            bool jumpTimeAvailable = _uninteruptedJumptime < maxJumpTime.Value + jumpSlowdownTime.Value;
            bool releasedJump = jumpBuffer.IsBufferedInputAvailable(state => state.state == -1);
            bool notPressedJump = jumpBuffer.IsBufferedInputAvailable(state => state.state == 0);

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
                _uninteruptedJumptime += Time.fixedDeltaTime;
                jumpInstruction.SetValue(true);
                jumpBuffer.ExecuteBufferOnCondition(state => state.state > 0);
                return;
            }

            jumpInstruction.SetValue(false);
        }
    }
}
