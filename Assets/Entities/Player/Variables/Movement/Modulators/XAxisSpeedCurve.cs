using System;
using System.Collections;
using System.Collections.Generic;
using GameCore.Functions;
using GameCore.Variables.Primitives;
using UnityEngine;

[CreateAssetMenu(fileName="XAxisSpeedCurve", menuName="Modulators/XAxisSpeedCurve", order=0)]
public class XAxisSpeedCurve : FloatModulator
{
    public FloatReference SecondsToMaxSpeed;
    public FloatReference MaxSpeed;

    private float TotalTimeRun;

    public override float Output(float totalTimeRun)
    {
        if (totalTimeRun < 0)
            return 0;

        if (totalTimeRun > SecondsToMaxSpeed)
            return MaxSpeed;

        return MaxSpeed.Value * totalTimeRun / SecondsToMaxSpeed;
    }
}
