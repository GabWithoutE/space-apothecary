using UnityEngine;

namespace GameCore.Variables.Primitives
{
    [CreateAssetMenu]
    public class BoolVariable : Variable<bool>
    {
        public override void SetValue(IVariable<bool> value)
        {
            Value = value.GetValue();
        }

        // Applies AND operator
        public override void ApplyChange(bool value)
        {
            Value = Value && value;
        }

        // Applies AND operator
        public override void ApplyChange(IVariable<bool> value)
        {
            Value = Value && value.GetValue();
        }
    }
}
