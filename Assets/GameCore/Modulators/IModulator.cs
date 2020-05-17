using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.Modulators
{
    public interface IModulator<T>
    {
        T Output(T input);
    }
}
