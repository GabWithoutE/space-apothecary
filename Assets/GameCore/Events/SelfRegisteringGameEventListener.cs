using UnityEngine;
using UnityEngine.Events;

namespace GabriellChen.SpaceApothecary.Events
{
    public abstract class SelfRegisteringGameEventListener : ScriptableObject, IGameEventListener
    {
        public GameEvent Event;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public abstract void OnEventRaised();
    }
}
