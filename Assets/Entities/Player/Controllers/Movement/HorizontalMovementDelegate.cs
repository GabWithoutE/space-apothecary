using System.Collections;
using System.Collections.Generic;
using GameCore.Functions;
using GameCore.Variables.Primitives;
using UnityEngine;

[CreateAssetMenu(menuName = "Movement/HorizontalMovementDelegate")]
public class HorizontalMovementDelegate : MoveEntityDelegate
{
    public FloatModulator XSpeedModulator;
    public IntVariable XDirection;

    public override void Move(Transform entityTransform, float totalTimeRun)
    {
        if (XDirection.Value != 0)
            entityTransform.position += new Vector3(XDirection.Value * XSpeedModulator.Output(totalTimeRun), 0, 0);
    }
}
