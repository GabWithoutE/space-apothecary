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

    private float totalTimeRun = 0;

    public override void Move(Transform entityTransform)
    {
        if (XDirection.Value != 0)
        {
            totalTimeRun += Time.fixedDeltaTime;
            entityTransform.position += new Vector3(XDirection.Value * XSpeedModulator.Output(totalTimeRun), 0, 0);
        }
        else
        {
            totalTimeRun = 0;
        }
    }
}
