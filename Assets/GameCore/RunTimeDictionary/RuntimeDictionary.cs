using System.Collections.Generic;
using UnityEngine;

namespace GameCore.RunTimeDictionary
{
    public class RuntimeDictionary<TKey, TValue> : ScriptableObject
    {
        public Dictionary<TKey, TValue> Dictionary = new Dictionary<TKey,TValue>();
    }
}
