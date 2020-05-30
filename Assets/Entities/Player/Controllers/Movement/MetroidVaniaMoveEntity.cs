using System.IO.IsolatedStorage;
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
    public BoolVariable attacking;

    public IntVariable XDirection;
    public IntVariable YDirection;
    public BoolVariable jumpInstruction;

    public MoveEntityOnAxisDelegate jumpDelegate;

    public MoveEntityOnAxisDelegate gravityDelegate;
    public DetectCollisionDelegate groundCollisionDetector;

    public MoveEntityOnAxisDelegate RunningDelegate;
    public MoveEntityOnAxisDelegate PrimaryAttackMovementDelegate;
    public Vector3Reference primaryAttackDirection;

    private float uninteruptedRunTime = 0;
    private float uninteruptedFallTime = 0;
    private float _uninteruptedJumptime = 0;
    private float _currentAttackFrame = 0;

    public override void Move(Transform entityTransform, float timeModifier)
    {
        SetGroundedState(entityTransform);

        ComputeTimeModifiers();

        if (!jumpInstruction.Value)
            gravityDelegate.Move(entityTransform, uninteruptedFallTime, false);
        else
            jumpDelegate.Move(entityTransform, _uninteruptedJumptime, false);

        if (XDirection.Value != 0)
            RunningDelegate.Move(entityTransform, uninteruptedRunTime, XDirection.Value == -1);

        if (grounded.Value && attacking.Value)
        {
            Debug.Log((Vector2)primaryAttackDirection.Value);

            // do the movement for attacking
            PrimaryAttackMovementDelegate.Move(
                entityTransform,
                _currentAttackFrame,
                primaryAttackDirection.Value.x < 0
                );
        }
    }

    private void ComputeTimeModifiers()
    {
        if (XDirection.Value != 0)
            uninteruptedRunTime += Time.deltaTime;
        else
            uninteruptedRunTime = 0;

        if (!grounded.Value && !jumpInstruction.Value)
            uninteruptedFallTime += Time.deltaTime;
        else
            uninteruptedFallTime = 0;

        if (jumpInstruction.Value)
            _uninteruptedJumptime += Time.deltaTime;
        else
            _uninteruptedJumptime = 0;

        if (attacking.Value)
            _currentAttackFrame++;
        else
            _currentAttackFrame = 0;
    }

    private void SetGroundedState(Transform entityTransform)
    {
        bool collidingWithGround = groundCollisionDetector.IsColliding(entityTransform, false);
        grounded.SetValue(groundCollisionDetector.IsColliding(entityTransform, false));

        if (collidingWithGround)
        {
            groundedEvent.Raise();
        }
    }
}
