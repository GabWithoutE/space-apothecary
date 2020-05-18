using System;
using System.Collections.Generic;
using GameCore.Variables.Primitives;
using GameCore.Variables.Unity;
using GameCore.Sets;
using GameCore.Sets.Primitives;
using UnityEngine;

public class KeyCodeCheckLoop : MonoBehaviour
{
    public KeyCodeReference UpIn;
    public KeyCodeReference DownIn;
    public KeyCodeReference LeftIn;
    public KeyCodeReference RightIn;
    public KeyCodeReference JumpIn;

    public BoolVariable UpOut;
    public BoolVariable DownOut;
    public BoolVariable LeftOut;
    public BoolVariable RightOut;
    public BoolVariable JumpOut;

    // Update is called once per frame
    void Update()
    {
        UpOut.SetValue(Input.GetKey(UpIn.Value));
        DownOut.SetValue(Input.GetKey(DownIn.Value));
        LeftOut.SetValue(Input.GetKey(LeftIn.Value));
        RightOut.SetValue(Input.GetKey(RightIn.Value));
        JumpOut.SetValue(Input.GetKey(JumpIn.Value));
    }
}
