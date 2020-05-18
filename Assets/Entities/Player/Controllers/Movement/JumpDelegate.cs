using System.Collections;
using System.Collections.Generic;
using GameCore.Functions;
using UnityEngine;

[CreateAssetMenu(menuName = "Movement/JumpDelegate")]
public class JumpDelegate : MoveEntityDelegate
{
    public FloatModulator JumpSpeedModulator;

    public override void Move(Transform entityTransform, float timeSpentJumping)
    {
        entityTransform.position += new Vector3(0, JumpSpeedModulator.Output(timeSpentJumping), 0);
    }
}
