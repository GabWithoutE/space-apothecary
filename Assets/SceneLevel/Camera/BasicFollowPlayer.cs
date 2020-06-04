using System;
using GameCore.Variables.Unity;
using UnityEngine;

namespace SceneLevel.Camera
{
    public class BasicFollowPlayer : MonoBehaviour
    {
        public Vector3Reference playerPosition;

        private void FixedUpdate()
        {
            Transform cameraTransform = transform;
            cameraTransform.position = new Vector3(playerPosition.Value.x, playerPosition.Value.y, cameraTransform.position.z);
        }
    }
}
