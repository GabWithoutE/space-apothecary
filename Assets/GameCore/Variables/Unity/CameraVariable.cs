using UnityEngine;

namespace GameCore.Variables.Unity
{
    [CreateAssetMenu]
    public class CameraVariable : Variable<Camera>
    {
        public override void SetValue(IVariable<Camera> value)
        {
            Value = value.GetValue();
        }

        public override void ApplyChange(Camera amount)
        {
            Value = amount;
        }

        public override void ApplyChange(IVariable<Camera> amount)
        {
            Value = amount.GetValue();
        }
    }
}
