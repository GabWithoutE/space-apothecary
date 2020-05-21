using System;
using System.Collections.Generic;
using GabriellChen.SpaceApothecary.Events;
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
    public KeyCodeReference ItemMenuIn;

    public BoolVariable UpOut;
    public BoolVariable DownOut;
    public BoolVariable LeftOut;
    public BoolVariable RightOut;
    public BoolVariable JumpOut;
    public GameEvent ItemMenuOut;

    // Update is called once per frame
    void Update()
    {
        UpOut.SetValue(Input.GetKey(UpIn.Value));
        DownOut.SetValue(Input.GetKey(DownIn.Value));
        LeftOut.SetValue(Input.GetKey(LeftIn.Value));
        RightOut.SetValue(Input.GetKey(RightIn.Value));
        JumpOut.SetValue(Input.GetKey(JumpIn.Value));

        if (Input.GetKeyDown(ItemMenuIn))
        {
            ItemMenuOut.Raise();
        }
    }
}
