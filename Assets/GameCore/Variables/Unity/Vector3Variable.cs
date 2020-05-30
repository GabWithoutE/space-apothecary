using UnityEngine;

namespace GameCore.Variables.Unity
{
    [CreateAssetMenu(menuName = "Variables/Unity/Vector3")]
    public class Vector3Variable : Variable<Vector3>
    {
        public override void SetValue(IVariable<Vector3> value)
        {
            Value = value.GetValue();
        }

        public override void ApplyChange(Vector3 amount)
        {
            Value += amount;
        }

        public override void ApplyChange(IVariable<Vector3> amount)
        {
            Value += amount.GetValue();
        }
    }
}
