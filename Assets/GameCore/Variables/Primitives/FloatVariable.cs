using UnityEngine;
using UnityEngine.UIElements.Experimental;

namespace GameCore.Variables.Primitives
{
    [CreateAssetMenu]
    public class FloatVariable : Variable<float>
    {
        public override void SetValue(IVariable<float> value)
        {
            Value = value.GetValue();
        }

        public override void ApplyChange(float amount)
        {
            Value += amount;
        }

        public override void ApplyChange(IVariable<float> amount)
        {
            Value += amount.GetValue();
        }
    }
}
