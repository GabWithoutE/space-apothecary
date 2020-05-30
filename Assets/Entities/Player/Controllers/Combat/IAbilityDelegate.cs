using UnityEngine;

namespace Entities.Player.Controllers.Combat
{
    public interface IAbilityDelegate
    {
        void PerformAbility(Transform abilityPerformer);
    }
}
