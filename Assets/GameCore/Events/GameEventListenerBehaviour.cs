using UnityEngine;
using UnityEngine.Events;

namespace GabriellChen.SpaceApothecary.Events
{
    public class GameEventListenerBehaviour : MonoBehaviour
    {
        [Tooltip("Event to register with.")]
        public GameEvent Event;

        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEvent Response;

        private void Start()
        {
            Event.RegisterListener(GameEventResponse);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(GameEventResponse);
        }

        private void GameEventResponse()
        {
            Response.Invoke();
        }
    }
}
