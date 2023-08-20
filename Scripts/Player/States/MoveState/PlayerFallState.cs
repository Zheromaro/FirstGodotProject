using ArrowPlatformer.Player;
using Godot;
using System;

namespace ArrowPlatformer.Scripts.Player.States
{
    public class PlayerFallState : PlayerBaseState
    {
        private float _fallGravity;
        private float _previousVelocityY, _newVelocityY;

        public PlayerFallState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory)
        {
            _fallGravity = -2 * Ctx.JumpHeight / Mathf.Pow(Ctx.JumpTimeToDesand, 2) * -1;
        }

        public override void PhysicsProcessState(float delta)
        {
            ApplyFallGravity(ref Ctx.NewVelocity, delta);
        }
        public override void CheckSwitchStates()
        {
            if (Ctx.IsOnFloor())
                SwitchState(null);
        }


        private void ApplyFallGravity(ref Vector2 vel, float delta)
        {
            _previousVelocityY = vel.Y;
            _newVelocityY = vel.Y + _fallGravity * delta;
            _newVelocityY = Mathf.Clamp(_newVelocityY, -Mathf.Inf, Ctx.TerminalVelocity);

            vel.Y = (_previousVelocityY + _newVelocityY) * 0.5f;
        }
    }
}
