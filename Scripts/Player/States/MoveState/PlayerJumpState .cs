using ArrowPlatformer.Player;
using Godot;
using System;

namespace ArrowPlatformer.Scripts.Player.States
{
    public class PlayerJumpState : PlayerBaseState
    {
        private bool _jumped = false;
        private float _jumpForce;
        private float _jumpGravity;
        private float _previousVelocityY, _newVelocityY;

        public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory)
        {
            _jumpForce = 2 * Ctx.JumpHeight / Ctx.JumpTimeToPeak * -1;
            _jumpGravity = -2 * Ctx.JumpHeight / Mathf.Pow(Ctx.JumpTimeToPeak, 2) * -1;
        }


        public override void PhysicsProcessState(float delta)
        {
            if (!_jumped)
            {
                HandleJump(ref Ctx.NewVelocity);
                _jumped = true;
            }
            ApplyJumpGravity(ref Ctx.NewVelocity, delta);

        }

        public override void ExitState() => _jumped = false;

        public override void CheckSwitchStates()
        {
            if (Ctx.Velocity.Y > 0f)
                SwitchState(Factory.FallState());
        }


        private void ApplyJumpGravity(ref Vector2 vel, float delta)
        {
            _previousVelocityY = vel.Y;
            _newVelocityY = vel.Y + _jumpGravity * delta;
            _newVelocityY = Mathf.Clamp(_newVelocityY, -Mathf.Inf, Ctx.TerminalVelocity);

            vel.Y = (_previousVelocityY + _newVelocityY) * 0.5f;
        }
        private void HandleJump(ref Vector2 vel)
        {
            vel.Y = _jumpForce;
        }
    }
}
