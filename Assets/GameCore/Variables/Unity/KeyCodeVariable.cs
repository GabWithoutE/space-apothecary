using UnityEngine;

namespace GameCore.Variables.Unity
{
    [CreateAssetMenu]
    public class KeyCodeVariable : Variable<KeyCode>
    {
        public override void SetValue(IVariable<KeyCode> value)
        {
            Value = value.GetValue();
        }

        public override void ApplyChange(KeyCode value)
        {
            Value = value;
        }

        public override void ApplyChange(IVariable<KeyCode> value)
        {
            Value = value.GetValue();
        }
    }
}
