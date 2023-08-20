using ArrowPlatformer.Player;
using Godot;
using System;

namespace ArrowPlatformer.Scripts.Player.States;

public class PlayerMoveState : PlayerBaseState
{
    private float _targetVelocityX;

    public PlayerMoveState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
        : base(currentContext, playerStateFactory) { IsRootState = true; }

    public override void PhysicsProcessState(float delta)
    {
        Move(ref Ctx.NewVelocity);
    }

    public override void CheckSwitchStates() 
    {

        if (!Ctx.IsOnFloor() && CurrentSubState != Factory.JumpState())
        {
            SetSubState(Factory.FallState());
            return;
        }
        else if (Input.IsActionJustPressed(InputNames.Jump))
        {
            SetSubState(Factory.JumpState());
            return;
        }

        if (Ctx.XYInput.X == 0 && Ctx.Velocity == Vector2.Zero)
        {
            SwitchState(Factory.IdelState());
        }
    }

    private void Move(ref Vector2 vel)
    {
        _targetVelocityX = Ctx.XYInput.X * Ctx.MoveSpeed;
        vel.X = Mathf.MoveToward(vel.X, _targetVelocityX,
            Mathf.Sign(Ctx.XYInput.X) == Mathf.Sign(Ctx.NewVelocity.X) ? Ctx.Acceleration : Ctx.Decceleration);
    }


}
