using UnityEngine;

namespace GameCore.Variables.Primitives
{
    [CreateAssetMenu]
    public class IntVariable : Variable<int>
    {
        public override void SetValue(IVariable<int> value)
        {
            Value = value.GetValue();
        }

        public override void ApplyChange(int amount)
        {
            Value += amount;
        }

        public override void ApplyChange(IVariable<int> amount)
        {
            Value += amount.GetValue();
        }
    }
}
