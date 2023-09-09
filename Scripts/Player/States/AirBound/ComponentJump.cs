using Godot;
using System;

namespace Scripts.Player.States
{
    public partial class ComponentJump : Node
    {

        [Export] public float _jumpHeight = 15;
        [Export] private float _jumpTimeToPeak = 0.1f;
        [Export] private float _jumpTimeToDesand = 0.1f;
        [Export] private float _terminalVelocity = 1000; // normaly the more he fall the faster he becomes, so this is the speed limits
        private float _jumpForce;
        private float _jumpGravity;
        private float _previousVelocityY, _newVelocityY;
        private Vector2 _calculateVelocity;

        [ExportGroup("EventNames")]
        [Export] private string _toFall;

        public override void _Ready()
        {
            _jumpForce = 2 * _jumpHeight / _jumpTimeToPeak * -1;
            _jumpGravity = -2 * _jumpHeight / Mathf.Pow(_jumpTimeToPeak, 2) * -1;
        }

        public void _StateEntered()
        {
            _calculateVelocity = PlayerInstance.Player.Velocity;
            HandleJump(ref _calculateVelocity);
            PlayerInstance.Player.Velocity = _calculateVelocity;
        }

        public void _StatePhysicsProcessing(float delta)
        {
            _calculateVelocity = PlayerInstance.Player.Velocity;
            ApplyJumpGravity(ref _calculateVelocity, delta);
            PlayerInstance.Player.Velocity = _calculateVelocity;

            if (PlayerInstance.Player.Velocity.Y > 0f)
                PlayerInstance.Player.SwitchState(_toFall);
        }


        private void ApplyJumpGravity(ref Vector2 vel, float delta)
        {
            _previousVelocityY = vel.Y;
            _newVelocityY = vel.Y + _jumpGravity * delta;
            _newVelocityY = Mathf.Clamp(_newVelocityY, -Mathf.Inf, _terminalVelocity);

            vel.Y = (_previousVelocityY + _newVelocityY) * 0.5f;
        }
        private void HandleJump(ref Vector2 vel)
        {
            vel.Y = _jumpForce;
        }
    }
}