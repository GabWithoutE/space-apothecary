using GameCore.Variables.Unity;
using UnityEngine;

namespace Entities.Player.Controllers.Movement.PlayerState
{
    public class UpdatePlayerPosition : MonoBehaviour
    {
        public Vector3Variable playerPosition;

        void FixedUpdate()
        {
            playerPosition.SetValue(transform.position);
        }

    }
}
