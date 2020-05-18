using System.Collections;
using System.Collections.Generic;
using GameCore.Functions;
using GameCore.Variables.Primitives;
using UnityEngine;

[CreateAssetMenu(menuName = "Modulators/ReverseDampenedCurve")]
public class ReverseDampenedCurve : FloatModulator
{
    // Maximum speed that can be jumped.
    public FloatReference MaxJumpSpeed;
    // Time able to jump at max speed.
    public FloatReference MaxSpeedTime;
    // Amount of time to slow down.
    public FloatReference SlowdownTime;

    public override float Output(float timeSpentJumping)
    {
        if (timeSpentJumping < MaxSpeedTime)
            return MaxJumpSpeed;

        float timeSpentSlowingDown = Mathf.Clamp(timeSpentJumping - MaxSpeedTime, 0, SlowdownTime);
        return MaxJumpSpeed * (1 - (timeSpentSlowingDown / SlowdownTime));
    }
}
