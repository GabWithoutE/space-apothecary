using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.Variables
{
    // Abstract class for ScriptableObject variables, allowing them to be overriden
    // in editor.
    public abstract class Reference<TType, TVariable> where TVariable : IVariable<TType>
    {
        public bool UseConstant = true;
        public TType ConstantValue;
        public TVariable Variable;

        public Reference()
        { }

        public Reference(TType value)
        {
            UseConstant = true;
            ConstantValue = value;
        }

        public TType Value
        {
            get { return UseConstant ? ConstantValue : Variable.GetValue(); }
        }

        public static implicit operator TType(Reference<TType, TVariable> reference)
        {
            return reference.Value;
        }
    }
}
