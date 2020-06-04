using GameCore.Variables.Primitives;
using UnityEngine;

namespace GameCore.InterpolatedMovement
{
    /**
     * Computes and manages the interpolation factor--the current time at the frame render, relative to the time
     *     of the current FixedUpdate, and the previous FixedUpdate. How far is the current Update in within the
     *     scale of the fixed time interval between each FixedUpdate.
     */
    public class InterpolationController : MonoBehaviour
    {
        private float[] _lastUpdateTimes;
        private int _newTimeIndex;
        private int _oldTimeIndex => _newTimeIndex == 0 ? 1 : 0;

        public FloatVariable interpolationFactor;

        public void Start()
        {
            _lastUpdateTimes = new float[2];
            _newTimeIndex = 0;
        }

        public void FixedUpdate()
        {
            _newTimeIndex = _oldTimeIndex;
            _lastUpdateTimes[_newTimeIndex] = Time.fixedTime;
        }

        public void Update()
        {
            float targetFixedUpdateTime = _lastUpdateTimes[_newTimeIndex];
            float startingFixedUpdateTime = _lastUpdateTimes[_oldTimeIndex];

            if (targetFixedUpdateTime != startingFixedUpdateTime)
                interpolationFactor.SetValue(
                    (Time.time - targetFixedUpdateTime) / (targetFixedUpdateTime - startingFixedUpdateTime)
                );
            else
                interpolationFactor.SetValue(1);

        }
    }
}
