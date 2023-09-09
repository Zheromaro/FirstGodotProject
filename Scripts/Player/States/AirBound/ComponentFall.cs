using Godot;
using System;

namespace Scripts.Player.States
{
    public partial class ComponentFall : Node
    {

        [Export] private float _jumpHeight = 15;
        [Export] private float _jumpTimeToDesand = 0.1f;
        [Export] private float _terminalVelocity = 1000; // normaly the more he fall the faster he becomes, so this is the speed limits
        private float _fallGravity;
        private float _previousVelocityY, _newVelocityY;
        private Vector2 _calculateVelocity;

        [ExportGroup("EventNames")]
        [Export] private string _toIdel;

        public override void _Ready()
        {
            _fallGravity = -2 * _jumpHeight / Mathf.Pow(_jumpTimeToDesand, 2) * -1;
        }

        public void _StatePhysicsProcessing(float delta)
        {
            _calculateVelocity = PlayerInstance.Player.Velocity;
            ApplyJumpGravity(ref _calculateVelocity ,delta);
            PlayerInstance.Player.Velocity = _calculateVelocity;

            if (PlayerInstance.Player.IsOnFloor())
                PlayerInstance.Player.SwitchState(_toIdel);
        }


        private void ApplyJumpGravity(ref Vector2 vel, float delta)
        {
            _previousVelocityY = vel.Y;
            _newVelocityY = vel.Y + _fallGravity * delta;
            _newVelocityY = Mathf.Clamp(_newVelocityY, -Mathf.Inf, _terminalVelocity);

            vel.Y = (_previousVelocityY + _newVelocityY) * 0.5f;
        }
    }
}