using GameCore.Collision;
using GameCore.Variables.Primitives;
using UnityEngine;

[CreateAssetMenu(menuName = "Movement/MetroidVaniaMoveEntity")]
public class MetroidVaniaMoveEntity : MoveEntityDelegate
{
    public BoolVariable IsDefyingGravity;
    public BoolVariable IsGrounded;
    public FloatReference MaxJumpTime;
    public FloatReference JumpSlowdownTime;

    public IntVariable XDirection;
    public IntVariable YDirection;
    public BoolVariable JumpInputOn;

    public GravityDelegate GravityDelegate;
    public HorizontalMovementDelegate HorizontalMovementDelegate;
    public JumpDelegate JumpDelegate;

    public DetectCollision GroundCollisionDetector;

    private float uninteruptedRunTime = 0;
    private float uninteruptedFallTime = 0;
    private float uninteruptedJumptime = 0;
    private bool isJumpAvailable = false;
    private bool isJumping;

    public override void Move(Transform entityTransform, float timeModifier)
    {
        // isJumping = false;
        SetGroundedState(entityTransform);
        isJumpAvailable = IsGrounded.Value;

        if (JumpInputOn.Value && isJumpAvailable)
        {
            isJumping = true;
            isJumpAvailable = false;
        } else if (!JumpInputOn.Value ^ uninteruptedJumptime > MaxJumpTime.Value + JumpSlowdownTime.Value)
        {
            isJumping = false;
        }

        ComputeTimeModifiers();

        if (!isJumping)
        // {
            GravityDelegate.Move(entityTransform, uninteruptedFallTime);
        // }
        else
            JumpDelegate.Move(entityTransform, uninteruptedJumptime);

        HorizontalMovementDelegate.Move(entityTransform, uninteruptedRunTime);
    }

    private void ComputeTimeModifiers()
    {
        if (XDirection.Value != 0)
            uninteruptedRunTime += Time.fixedDeltaTime;
        else
            uninteruptedRunTime = 0;

        if (!IsGrounded.Value && !isJumping)
            uninteruptedFallTime += Time.fixedDeltaTime;
        else
            uninteruptedFallTime = 0;

        if (isJumping)
            uninteruptedJumptime += Time.fixedDeltaTime;
        else
            uninteruptedJumptime = 0;
    }

    private void SetGroundedState(Transform entityTransform)
    {
        IsGrounded.SetValue(GroundCollisionDetector.IsColliding(entityTransform));
    }
}
