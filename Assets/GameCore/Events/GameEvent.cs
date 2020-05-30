using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GabriellChen.SpaceApothecary.Events
{
    [CreateAssetMenu(menuName = "Game Event")]
    public class GameEvent : ScriptableObject
    {
        public delegate void GameEventResponse();

        private event GameEventResponse OnEventTriggered;

        public void Raise()
        {
            if (OnEventTriggered != null)
            {
                OnEventTriggered();
            }
        }

        public void RegisterListener(GameEventResponse eventResponse)
        {
            OnEventTriggered += eventResponse;
        }

        public void UnregisterListener(GameEventResponse eventResponse)
        {
            OnEventTriggered += eventResponse;
        }
    }
}
