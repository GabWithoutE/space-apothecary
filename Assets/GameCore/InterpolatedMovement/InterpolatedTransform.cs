using System;
using GameCore.Variables.Primitives;
using UnityEngine;

namespace GameCore.InterpolatedMovement
{
    public class InterpolatedTransform : MonoBehaviour
    {
        public FloatReference interpolationFactor;
        private TransformData[] _lastTransforms;
        private int _newTransformIndex;
        private int _oldTransformIndex => _newTransformIndex == 0 ? 1 : 0;

        void OnEnable()
        {
            ForgetPreviousTransforms();
        }

        public void ForgetPreviousTransforms()
        {
            _lastTransforms = new TransformData[2];
            Transform transform1 = transform;
            TransformData t = new TransformData(
                transform1.localPosition, transform1.localRotation, transform1.localScale
            );

            _lastTransforms[0] = t;
            _lastTransforms[1] = t;
            _newTransformIndex = 0;
        }

        // Applies the actual value of the fixed transform according to the fixed time intervals, so that computations
        //     can be made on the up2date value.
        private void FixedUpdate()
        {
            Transform trans = transform;
            TransformData mostRecentTransform = _lastTransforms[_newTransformIndex];

            trans.localPosition = mostRecentTransform.position;
            trans.localRotation = mostRecentTransform.rotation;
            trans.localScale = mostRecentTransform.scale;
        }

        // Stores the newly computed transform location, so that it can be used in the interpolation step.
        public void LateFixedUpdate()
        {
            Transform trans = transform;
            _newTransformIndex = _oldTransformIndex; // Set new index to the older stored transform.
            _lastTransforms[_newTransformIndex] = new TransformData(
                trans.localPosition, trans.localRotation, trans.localScale
            );
        }

        // Applies the interpolated value to the transform, so that the value is accurate based on the frame's
        //     position relative to the fixed time interval.
        private void Update()
        {
            TransformData newestTransform = _lastTransforms[_newTransformIndex];
            TransformData olderTransform = _lastTransforms[_oldTransformIndex];

            transform.localPosition = Vector3.Lerp(olderTransform.position, newestTransform.position, interpolationFactor.Value);
            transform.localRotation = Quaternion.Slerp(olderTransform.rotation, newestTransform.rotation, interpolationFactor.Value);
            transform.localScale = Vector3.Lerp(olderTransform.scale, newestTransform.scale, interpolationFactor.Value);
        }

        private struct TransformData
        {
            public Vector3 position;
            public Quaternion rotation;
            public Vector3 scale;

            public TransformData(Vector3 position, Quaternion rotation, Vector3 scale)
            {
                this.position = position;
                this.rotation = rotation;
                this.scale = scale;
            }
        }
    }
}
