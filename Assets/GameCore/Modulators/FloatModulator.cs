using GameCore.Modulators;
using UnityEngine;

namespace GameCore.Functions
{
    public abstract class FloatModulator : ScriptableObject, IModulator<float>
    {
        public abstract float Output(float input);
    }
}

