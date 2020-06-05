using System.IO.IsolatedStorage;
using Entities.Player.Controllers.Movement;
using GabriellChen.SpaceApothecary.Events;
using GameCore.Collision;
using GameCore.Variables.Primitives;
using GameCore.Variables.Unity;
using UnityEngine;

[CreateAssetMenu(menuName = "Movement/MetroidVaniaMoveEntity")]
public class MetroidVaniaMoveEntity : MoveEntityDelegate
{
    public GameEvent groundedEvent;
    public BoolVariable grounded;
    public BoolVariable ceilingHit;
    public BoolVariable attacking;
    public BoolVariable dashing;

    public IntVariable XDirection;
    public IntVariable YDirection;
    public BoolVariable jumpInstruction;

    public MoveEntityOnAxisDelegate jumpDelegate;

    public MoveEntityOnAxisDelegate gravityDelegate;
    public DetectCollisionDelegate groundCollisionDetector;
    public DetectCollisionDelegate ceilingCollisionDetector;


    public MoveEntityOnAxisDelegate RunningDelegate;
    public MoveEntityOnAxisDelegate primaryAttackMovementDelegate;
    public Vector3Reference primaryAttackDirection;

    public DashMovementDelegate dashMovementDelegate;

    private float uninteruptedRunTime = 0;
    private float uninteruptedFallTime = 0;
    private float _uninteruptedJumptime = 0;
    private float _currentAttackTime = 0;
    private float _currentDashTime = 0;

    public override void FixedMove(Transform entityTransform)
    {
        SetGroundedState(entityTransform);
        SetCeilingHitState(entityTransform);

        ComputeTimeModifiers();

        if (!dashing.Value)
            if (!jumpInstruction.Value)
                gravityDelegate.Move(entityTransform, uninteruptedFallTime, false);
            else
                jumpDelegate.Move(entityTransform, _uninteruptedJumptime, false);

        if (XDirection.Value != 0)
            RunningDelegate.Move(entityTransform, uninteruptedRunTime, XDirection.Value == -1);

        if (grounded.Value && attacking.Value && primaryAttackDirection.Value.x != 0)
            // do the movement for attacking
            primaryAttackMovementDelegate.Move(
                entityTransform,
                _currentAttackTime,
                primaryAttackDirection.Value.x < 0
            );

        if (dashing.Value)
            dashMovementDelegate.Move(entityTransform, _currentDashTime);
    }

    private void ComputeTimeModifiers()
    {
        if (XDirection.Value != 0 || dashing.Value)
            uninteruptedRunTime += Time.fixedDeltaTime;
        else
            uninteruptedRunTime = 0;

        if (!grounded.Value && !jumpInstruction.Value && !dashing.Value)
            uninteruptedFallTime += Time.fixedDeltaTime;
        else
            uninteruptedFallTime = 0;

        if (jumpInstruction.Value)
            _uninteruptedJumptime += Time.fixedDeltaTime;
        else
            _uninteruptedJumptime = 0;

        if (attacking.Value)
            _currentAttackTime += Time.fixedDeltaTime;
        else
            _currentAttackTime = 0;

        if (dashing.Value)
            _currentDashTime += Time.fixedDeltaTime;
        else
            _currentDashTime = 0;
    }

    private void SetGroundedState(Transform entityTransform)
    {
        bool collidingWithGround = groundCollisionDetector.IsColliding(entityTransform, false);
        grounded.SetValue(collidingWithGround);

        // TODO: maybe only trigger events when the values are changed, not just when it's true...
        // if (collidingWithGround)
        // {
        //     groundedEvent.Raise();
        // }
    }

    private void SetCeilingHitState(Transform entityTransform)
    {
        ceilingHit.SetValue(ceilingCollisionDetector.IsColliding(entityTransform, false));
    }
}
