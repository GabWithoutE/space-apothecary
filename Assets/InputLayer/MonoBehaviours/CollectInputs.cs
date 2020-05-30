using System;
using System.Collections.Generic;
using System.Linq;
using GameCore.Variables.Primitives;
using GameCore.Variables.Unity;
using UnityEngine;

namespace InputLayer.MonoBehaviours
{
    public class CollectInputs : MonoBehaviour
    {
        public Inputs inputs;
        public Vector3Variable secondDirectionalControl;
        private List<KeyCodeKeyControlPair> _inputPairs;

        void Start()
        {
            _inputPairs = typeof(Inputs)
                .GetFields()
                .Select(fieldInfo => (KeyCodeKeyControlPair) fieldInfo.GetValue(inputs))
                .ToList();
        }

        void Update()
        {
            _inputPairs.ForEach(inputPair =>
            {
                inputPair.controlVariable.SetValue(InputConverter.InputToIntValue(inputPair.keyCode));
            });
            secondDirectionalControl.SetValue(Input.mousePosition);
        }
    }

    [Serializable]
    public struct Inputs
    {
        public KeyCodeKeyControlPair primaryAbility;
        public KeyCodeKeyControlPair secondaryAbility;
        public KeyCodeKeyControlPair tertiaryAbility;

        public KeyCodeKeyControlPair up;
        public KeyCodeKeyControlPair down;
        public KeyCodeKeyControlPair left;
        public KeyCodeKeyControlPair right;

        public KeyCodeKeyControlPair itemMenu;
    }

    [Serializable]
    public struct KeyCodeKeyControlPair
    {
        public KeyCodeReference keyCode;
        // 1 is up, 0 is neutral, -1 is down
        public IntVariable controlVariable;
    }
}
