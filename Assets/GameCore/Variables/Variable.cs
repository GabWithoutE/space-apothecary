using System;
using UnityEngine;

namespace GameCore.Variables
{
    public abstract class Variable<TType> : ScriptableObject, IVariable<TType>
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public TType Value;

        public TType GetValue()
        {
            return Value;
        }

        public void SetValue(TType value)
        {
            Value = value;
        }
        public abstract void SetValue(IVariable<TType> value);
        public abstract void ApplyChange(TType amount);
        public abstract void ApplyChange(IVariable<TType> amount);
    }
}
