using System.Collections.Generic;
using UnityEngine;

namespace InputLayer.MonoBehaviours
{
    // InputbufferEngineHook is a MonoBehaviour that allows the input buffers to "hook" into the GameEngine's
    //     events.
    public class InputBufferEngineHook : MonoBehaviour
    {

        public List<InputBuffer> inputBuffers;

        void Update()
        {
            foreach (InputBuffer buffer in inputBuffers)
            {
                buffer.UpdateInputBuffer();
            }
        }

        void FixedUpdate()
        {
            foreach (InputBuffer buffer in inputBuffers)
            {
                buffer.FixedUpdateInputBuffer();
            }
        }
    }
}
