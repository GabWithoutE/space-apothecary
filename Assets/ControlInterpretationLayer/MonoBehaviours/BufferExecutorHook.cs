using System;
using System.Collections.Generic;
using InputLayer;
using UnityEngine;

namespace ControlInterpretationLayer.MonoBehaviours
{
    // Monobehaviour for BufferExecutors to hook into Unity Engine
    public class BufferExecutorHook : MonoBehaviour
    {
        public List<BufferExecutor> bufferExecutors = new List<BufferExecutor>();
        // public IBufferExecutor jumpBuffer;
        // public IBufferExecutor attackBuffer;
        // public IBufferExecutor rightBuffer;
        // public IBufferExecutor leftBuffer;

        // public IBufferExecutor primaryAbilityBuffer;
        // public IBufferExecutor secondaryAbilityBuffer;

        void Start()
        {
            foreach (BufferExecutor executor in bufferExecutors)
            {
                executor.Initialize();
            }
        }

        void Update()
        {
            foreach (BufferExecutor executor in bufferExecutors)
            {
                executor.Update();
            }
        }
    }
}
