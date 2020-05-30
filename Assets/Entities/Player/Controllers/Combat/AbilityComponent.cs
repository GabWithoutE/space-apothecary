using UnityEngine;

namespace Entities.Player.Controllers.Combat
{
    public class AbilityComponent : MonoBehaviour
    {
        public AbilityGenerator primaryAbilityGenerator;
        public AbilityGenerator secondaryAbilityGenerator;
        public AbilityGenerator tertiaryAbilityGenerator;

        private float _primaryCooldown;
        private float _seondaryCooldown;
        private float _tertiaryCooldown;



        void Update()
        {
            addToCooldowns();

            primaryAbilityGenerator.GenerateAbilityDelegate(_primaryCooldown);
            secondaryAbilityGenerator.GenerateAbilityDelegate(_seondaryCooldown);
            tertiaryAbilityGenerator.GenerateAbilityDelegate(_tertiaryCooldown);
        }

        private void addToCooldowns()
        {
            _primaryCooldown += Time.fixedDeltaTime;
            _seondaryCooldown += Time.fixedDeltaTime;
            _tertiaryCooldown += Time.fixedDeltaTime;
        }

    }
}
