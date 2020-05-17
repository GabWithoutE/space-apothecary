using GameCore.Variables.Primitives;
using UnityEngine;

[CreateAssetMenu(menuName = "Movement/MetroidVaniaMoveEntity")]
public class MetroidVaniaMoveEntity : MoveEntityDelegate
{
    public BoolVariable IsJumping;
    public BoolVariable IsGrounded;

    public IntVariable XDirection;
    public IntVariable YDirection;

    public GravityDelegate GravityDelegate;
    public HorizontalMovementDelegate HorizontalMovementDelegate;

    public override void Move(Transform entityTransform)
    {
        GravityDelegate.Move(entityTransform);
        HorizontalMovementDelegate.Move(entityTransform);
    }
}
