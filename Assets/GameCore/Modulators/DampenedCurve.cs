using System;
using System.Collections;
using System.Collections.Generic;
using GameCore.Functions;
using GameCore.Variables.Primitives;
using UnityEngine;

[CreateAssetMenu(menuName="Modulators/DampenedCurve")]
public class DampenedCurve : FloatModulator
{
    public FloatReference SecondsToMaxSpeed;
    public FloatReference MaxSpeed;

    public override float Output(float totalTimeRun)
    {
        if (totalTimeRun < 0)
            return 0;

        if (totalTimeRun > SecondsToMaxSpeed)
            return MaxSpeed;

        return MaxSpeed.Value * totalTimeRun / SecondsToMaxSpeed;
    }
}
