using GameCore.Variables.Primitives;
using UnityEngine;

namespace Entities.Player.Controllers.Combat
{
    public class AbilityGenerator : ScriptableObject
    {
        public BoolReference abilityTrigger;
        public IAbilityDelegate abilityDelegate;
        public float abilityCooldown;

        // returns null if not triggered, and not available...
        public IAbilityDelegate GenerateAbilityDelegate(float totalTimePassed)
        {
            bool abilityAvailable = totalTimePassed < abilityCooldown;
            if (abilityAvailable && abilityTrigger.Value)
                return abilityDelegate;

            return null;
        }
    }
}
