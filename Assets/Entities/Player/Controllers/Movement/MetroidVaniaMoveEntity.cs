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

    public MoveEntityOnAxisDelegate JumpDelegate;
    public DetectCollisionDelegate CeilingCollisionDelegate;
    public DetectCollisionDelegate CeilingLocatorDelegate;

    public MoveEntityOnAxisDelegate GravityDelegate;
    public DetectCollisionDelegate GroundCollisionDetector;
    public DetectCollisionDelegate GroundLocatorDelegate;

    public MoveEntityOnAxisDelegate RunningDelegate;
    public DetectCollisionDelegate RightWallLocatorDelegate;
    public DetectCollisionDelegate LeftWallLocatorDelegate;

    private float uninteruptedRunTime = 0;
    private float uninteruptedFallTime = 0;
    private float uninteruptedJumptime = 0;
    private bool isJumpAvailable = false;
    private bool isJumping = false;

    public override void Move(Transform entityTransform, float timeModifier)
    {
        SetGroundedState(entityTransform);
        isJumpAvailable = IsGrounded.Value;

         if (JumpInputOn.Value && isJumpAvailable)
        {
            isJumping = true;
            isJumpAvailable = false;
        } else if (!JumpInputOn.Value || uninteruptedJumptime > MaxJumpTime.Value + JumpSlowdownTime.Value)
        {
            isJumping = false;
        }

        ComputeTimeModifiers();

        if (!isJumping)
            GravityDelegate.Move(entityTransform, uninteruptedFallTime, GroundLocatorDelegate, Vector2.down);
        else
            JumpDelegate.Move(entityTransform, uninteruptedJumptime, CeilingLocatorDelegate,Vector2.up);

        if (XDirection.Value != 0)
            RunningDelegate.Move(
                entityTransform,
                uninteruptedRunTime,
                XDirection.Value == 1 ? RightWallLocatorDelegate : LeftWallLocatorDelegate,
                new Vector2(XDirection.Value, 0)
            );
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
