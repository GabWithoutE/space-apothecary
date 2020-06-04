using UnityEngine;

namespace GameCore.InterpolatedMovement
{
    public class InterpolatedTransformUpdater : MonoBehaviour
    {
        private InterpolatedTransform _interpolatedTransform;

        void Start()
        {
            _interpolatedTransform = GetComponent<InterpolatedTransform>();
        }

        void FixedUpdate()
        {
            _interpolatedTransform.LateFixedUpdate();
        }
    }
}
