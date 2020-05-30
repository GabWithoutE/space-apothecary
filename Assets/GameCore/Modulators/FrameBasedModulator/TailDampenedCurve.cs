using GameCore.Functions;
using GameCore.Variables.Primitives;
using UnityEngine;

namespace GameCore.Modulators.FrameBasedModulator
{
    [CreateAssetMenu(menuName = "Modulators/FrameBased/SustainDecayEnvelope")]
    public class TailDampenedCurve : FloatModulator
    {
        public FloatReference maxValue;
        public IntReference sustainLength;
        public IntReference decayLength;
        public override float Output(float currentFrame)
        {
            // if not yet reached the decay stage, return the sustained value
            if (currentFrame <= sustainLength)
                return maxValue;

            // number of frames that have been computed past the sustain length
            float currentDecayFrame = Mathf.Clamp(currentFrame - sustainLength, 0, decayLength);
            return maxValue * (1 - (currentDecayFrame / decayLength));
        }
    }
}
