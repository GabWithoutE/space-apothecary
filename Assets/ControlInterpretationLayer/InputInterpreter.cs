using UnityEngine;

namespace ControlInterpretationLayer.MonoBehaviours
{
    public abstract class InputInterpreter : ScriptableObject
    {
        public abstract void Initialize();
        public abstract void Update();
        public abstract void FixedUpdate();
    }
}
