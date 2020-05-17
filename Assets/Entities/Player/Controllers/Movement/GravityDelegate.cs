using System;
using System.Collections;
using System.Collections.Generic;
using GameCore.Variables.Primitives;
using UnityEngine;

[CreateAssetMenu(menuName = "Movement/GravityDelegate")]
public class GravityDelegate : MoveEntityDelegate
{
    public FloatReference GravityAcceleration;
    public FloatReference MaxFallSpeed;

    private float fallSpeed = 0;

    public void ResetFallSpeed()
    {
        fallSpeed = 0;
    }

    public override void Move(Transform entityTransform)
    {
        fallSpeed = Mathf.Clamp(fallSpeed + GravityAcceleration * Time.fixedDeltaTime,0, MaxFallSpeed);
        entityTransform.position += new Vector3(0, -fallSpeed);
    }
}
