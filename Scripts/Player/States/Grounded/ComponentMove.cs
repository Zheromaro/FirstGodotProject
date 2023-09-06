using Godot;
using System;

namespace Scripts.Player.States
{
    public partial class ComponentMove : Node
    {

        [Export] private NodePath GroundedPath;
        private ComponentGrounded Grounded;

        [Export] private float MoveSpeed = 200f;
        [Export] private float Acceleration = 10f;
        [Export] private float Decceleration = 15f;
        private float _targetVelocityX;
        private Vector2 _calculateVelocity;

        [ExportCategory("EventNames")]
        [Export] private string _toIdel;

        public override void _Ready()
        {
            Grounded = GetNode<ComponentGrounded>(GroundedPath);
        }

        public void _StatePhysicsProcessing(float delta)
        {
            _calculateVelocity = PlayerInstance.Player.Velocity;
            Move(ref _calculateVelocity);
            PlayerInstance.Player.Velocity = _calculateVelocity;
        }

        public void _StateProcessing(float delta)
        {
            if (Grounded.GatherInput().X == 0 && PlayerInstance.Player.Velocity == Vector2.Zero)
            {
                PlayerInstance.Player.SwitchState(_toIdel);
            }
        }

        private void Move(ref Vector2 vel)
        {
            _targetVelocityX = Grounded.GatherInput().X * MoveSpeed;
            vel.X = Mathf.MoveToward(vel.X, _targetVelocityX,
                Mathf.Sign(Grounded.GatherInput().X) == Mathf.Sign(_calculateVelocity.X) ? Acceleration : Decceleration);
        }
    }
}