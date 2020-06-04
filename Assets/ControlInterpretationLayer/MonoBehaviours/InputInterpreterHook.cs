using System;
using System.Collections.Generic;
using InputLayer;
using UnityEngine;

namespace ControlInterpretationLayer.MonoBehaviours
{
    // Monobehaviour for BufferExecutors to hook into Unity Engine
    public class InputInterpreterHook : MonoBehaviour
    {
        public List<InputInterpreter> inputInterpreters = new List<InputInterpreter>();

        void Start()
        {
            foreach (InputInterpreter executor in inputInterpreters)
                executor.Initialize();
        }

        void Update()
        {
            foreach (InputInterpreter executor in inputInterpreters)
                executor.Update();
        }

        void FixedUpdate()
        {
            foreach (InputInterpreter executor in inputInterpreters)
                executor.FixedUpdate();
        }
    }
}
