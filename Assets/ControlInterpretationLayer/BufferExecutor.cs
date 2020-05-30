using UnityEngine;

namespace ControlInterpretationLayer.MonoBehaviours
{
    public abstract class BufferExecutor : ScriptableObject
    {
        public abstract void Initialize();
        public abstract void Update();
    }
}
