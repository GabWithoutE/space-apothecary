using ControlInterpretationLayer.MonoBehaviours;
using GameCore.Variables.Primitives;
using InputLayer;
using UnityEngine;

namespace ControlInterpretationLayer
{
    [CreateAssetMenu(menuName = "BufferExecutor/DirectionAxisBufferExecutor")]
    public class MovementBufferExecutor : BufferExecutor
    {
        public InputBuffer negativeDirectionBuffer;
        public InputBuffer positiveDirectionBuffer;

        public IntVariable direction;

        public override void Initialize() { }

        public override void Update()
        {
            if (negativeDirectionBuffer.IsBufferedInputAvailable(state => state.state > 0) &&
                positiveDirectionBuffer.IsBufferedInputAvailable(state => state.state > 0))
            {
                direction.SetValue(0);
                negativeDirectionBuffer.ExecuteBufferOnCondition(state => state.state > 0);
                positiveDirectionBuffer.ExecuteBufferOnCondition(state => state.state > 0);
            }
            else if (negativeDirectionBuffer.IsBufferedInputAvailable(state => state.state > 0))
            {
                direction.SetValue(-1);
                negativeDirectionBuffer.ExecuteBufferOnCondition(state => state.state > 0);
            }
            else if (positiveDirectionBuffer.IsBufferedInputAvailable(state => state.state > 0))
            {
                direction.SetValue(1);
                positiveDirectionBuffer.ExecuteBufferOnCondition(state => state.state > 0);
            }
            else
            {
                direction.SetValue(0);
            }
        }
    }
}
