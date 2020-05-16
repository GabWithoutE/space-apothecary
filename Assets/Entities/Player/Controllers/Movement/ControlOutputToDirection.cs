using System.Collections;
using System.Collections.Generic;
using GameCore.Variables.Primitives;
using UnityEngine;

public class ControlOutputToDirection : MonoBehaviour
{
    public BoolReference UpIn;
    public BoolReference DownIn;
    public BoolReference LeftIn;
    public BoolReference RightIn;

    public IntVariable XDirection;
    public IntVariable YDirection;

    // Update is called once per frame
    void Update()
    {
        if (LeftIn && RightIn)
        {
            XDirection.SetValue(0);
        } else if (LeftIn)
        {
            XDirection.SetValue(-1);
        } else if (RightIn)
        {
            XDirection.SetValue(1);
        }
        else
        {
            XDirection.SetValue(0);
        }

        if (UpIn && DownIn)
        {
            YDirection.SetValue(0);
        } else if (DownIn)
        {
            YDirection.SetValue(-1);
        } else if (UpIn)
        {
            YDirection.SetValue(1);
        }
        else
        {
            YDirection.SetValue(0);
        }
    }
}
