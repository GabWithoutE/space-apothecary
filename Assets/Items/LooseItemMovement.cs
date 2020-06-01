using System.Collections;
using System.Collections.Generic;
using GameCore.Collision;
using UnityEngine;

[CreateAssetMenu(menuName = "Movement/LooseItem")]
public class LooseItemMovement : MoveEntityDelegate
{
    public MoveEntityOnAxisDelegate GravityMovementDelegate;
    public override void FixedMove(Transform entityTransform)
    {
        float time = 0.1f;
        GravityMovementDelegate.Move(entityTransform, time, false);
    }

    public override void Move(Transform entityTransform) { }
}
