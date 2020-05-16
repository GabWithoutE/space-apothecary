using System;
using System.Collections.Generic;
using GameCore.Variables.Primitives;
using GameCore.Variables.Unity;
using GameCore.Sets;
using GameCore.Sets.Primitives;
using UnityEngine;

public class KeyCodeCheckLoop : MonoBehaviour
{
#if UNITY_EDITOR
    public BoolReference Debug;
    [SerializeField]
    public RuntimeSet<KeyCodeVariable> DebugInputs;
    [SerializeField]
    public BoolRuntimeSet DebugOutputs;
#endif
    public KeyCodeReference UpIn;
    public KeyCodeReference DownIn;
    public KeyCodeReference LeftIn;
    public KeyCodeReference RightIn;

    public BoolVariable UpOut;
    public BoolVariable DownOut;
    public BoolVariable LeftOut;
    public BoolVariable RightOut;

    void Start()
    {
        if (Debug)
        {
            // DebugInputs.Add(UpIn.Variable);
            // DebugInputs.Add(DownIn.Variable);
            // DebugInputs.Add(LeftIn.Variable);
            // DebugInputs.Add(RightIn.Variable);

            DebugOutputs.Add(UpOut);
            DebugOutputs.Add(DownOut);
            DebugOutputs.Add(LeftOut);
            DebugOutputs.Add(RightOut);
        }
    }
    // Update is called once per frame
    void Update()
    {
        UpOut.SetValue(Input.GetKey(UpIn));
        DownOut.SetValue(Input.GetKey(DownIn));
        LeftOut.SetValue(Input.GetKey(LeftIn));
        RightOut.SetValue(Input.GetKey(RightIn));
    }
}
