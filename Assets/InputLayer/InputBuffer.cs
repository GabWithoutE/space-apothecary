using System;
using System.Collections.Generic;
using System.Linq;
using GameCore.Variables.Unity;
using UnityEngine;

namespace InputLayer
{
    [CreateAssetMenu(menuName = "Input/InputBuffer")]
    public class InputBuffer : ScriptableObject, IBufferExecutor, IBufferUpdater
    {
        public KeyCodeReference keyCode;
        public int bufferSize;
        [SerializeField]
        private List<InputState> _buffer;
        private bool _blocked = false;
        private float _blockForTime = 0;

        void OnEnable()
        {
            _buffer = new List<InputState>();
            for (int i = 0; i < bufferSize; i++)
            {
                _buffer.Add(new InputState());
            }
        }

        public void FixedUpdateInputBuffer()
        {
            if (_blocked)
                _blockForTime -= Time.fixedDeltaTime;
            _blocked = _blockForTime > 0;
        }

        public void UpdateInputBuffer()
        {
            // if this buffer has been told to block for frames, the _blocked boolean will be true, subtract the remaining
            //     frames.
            // if (_blocked)
            //     _blockForFrames--;
            //
            // _blocked = _blockForFrames > 0;

            // move all values forward, except for the last value, which stays in order to be used in update computation
            //     Ex. previous held button, continues to be held -> increment, or previous held button no longer held
            //     then indicate release.
            ProgressBuffer();
            if (Input.GetKey(keyCode.Value))
                _buffer[bufferSize - 1].HoldInput();
            else
                _buffer[bufferSize - 1].ReleaseInput();
        }

        public bool IsBlocked()
        {
            return _blockForTime > 0;
        }

        public void BlockExecution(float time)
        {
            _blockForTime = time;
        }

        public bool IsBufferedInputAvailable(Func<InputState, bool> stateCondition)
        {
            if (_blocked)
                return false;

            // filters for states that meets the condition
            List<InputState> meetsStateCondition = _buffer.Where(stateCondition).ToList();

            // filters for states that have not been executed yet
            List<InputState> executable = meetsStateCondition.Where(x => !x.used).ToList();

            // return whether or not there are any that fit condition, and have not been executed yet
            return executable.Count > 0;
        }

        /**
         * ExecuteBufferOnCondition:
         *     Description: As long as the executor can execute, the executor will execute
         *     Params: stateCondition, lambda function that returns true or false based on InputState's fields
         */
        public void ExecuteBufferOnCondition(Func<InputState, bool> stateCondition)
        {
            // state condition meaning x.State == something
            List<InputState> meetsStateCondition = _buffer.Where(stateCondition).ToList();

            // executable determines if it has already been used or not
            List<InputState> executable = meetsStateCondition.Where(x => !x.used).ToList();

            // finds the first available executable InputState, and "executes" it (sets Used to true), then returns true
            //     for the executor to be able to know that it can and should execute.
            if (executable.Count > 0)
                executable.Find(x => !x.used).Execute();
        }

        private void ProgressBuffer()
        {
            // scooch them down except for the last one
            for (int i = 0; i < bufferSize - 1; i++)
            {
                _buffer[i].SetState(_buffer[i + 1].state);
                _buffer[i].SetUsed(_buffer[i + 1].used);
            }

        }
    }

    public interface IBufferExecutor
    {
        void ExecuteBufferOnCondition(Func<InputState, bool> stateCondition);
        void BlockExecution(float time);
    }

    // Interface for components updating the buffer
    public interface IBufferUpdater
    {
        void UpdateInputBuffer();
    }

    [Serializable]
    public class InputState
    {
        // State =0 = neutral, >0 being held, -1 released
        // Used = whether the input has been used for something or not yet
        [SerializeField]
        public int state;
        public bool used;

        public InputState()
        {
            state = 0;
            used = false;
        }

        public void SetState(int state)
        {
            this.state = state;
        }

        public void SetUsed(bool used)
        {
            this.used = used;
        }

        public void HoldInput()
        {
            used = false;
            if (state < 0)
            {
                state = 1;
                return;
            }

            state++;
        }

        public void ReleaseInput()
        {
            used = false;
            // if input was previously held, set it to -1 which indicates the release frame
            //     otherwise, set it to 0, which is neutral
            if (state > 0)
                state = -1;
            else
                state = 0;
        }

        // Sets Used to true
        public void Execute()
        {
            used = true;
        }
    }
}
