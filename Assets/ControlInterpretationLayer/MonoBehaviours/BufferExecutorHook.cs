using System;
using System.Collections.Generic;
using InputLayer;
using UnityEngine;

namespace ControlInterpretationLayer.MonoBehaviours
{
    // Monobehaviour for BufferExecutors to hook into Unity Engine
    public class BufferExecutorHook : MonoBehaviour
    {
        public List<BufferExecutor> timeBasedBufferExecutors = new List<BufferExecutor>();
        public List<BufferExecutor> frameBasedBufferExecutors = new List<BufferExecutor>();
        // public IBufferExecutor jumpBuffer;
        // public IBufferExecutor attackBuffer;
        // public IBufferExecutor rightBuffer;
        // public IBufferExecutor leftBuffer;

        // public IBufferExecutor primaryAbilityBuffer;
        // public IBufferExecutor secondaryAbilityBuffer;

        void Start()
        {
            foreach (BufferExecutor executor in timeBasedBufferExecutors)
                executor.Initialize();

            foreach (BufferExecutor executor in frameBasedBufferExecutors)
                executor.Initialize();
        }

        void FixedUpdate()
        {
            foreach (BufferExecutor executor in timeBasedBufferExecutors)
            {
                executor.Update();
            }
        }

        void Update()
        {
            foreach (BufferExecutor executor in frameBasedBufferExecutors)
            {
                executor.Update();
            }
        }
    }
}
