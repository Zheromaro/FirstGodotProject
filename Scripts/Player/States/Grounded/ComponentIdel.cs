using Godot;
using System;

namespace Scripts.Player.States
{
    public partial class ComponentIdel : Node
    {

        [Export] private NodePath GroundedPath;
        private ComponentGrounded Grounded;

        [ExportCategory("EventNames")]
        [Export] private string _toMove;

        public override void _Ready()
        {
            Grounded = GetNode<ComponentGrounded>(GroundedPath);
        }

        public void _StateProcessing(float delta)
        {
            if (Grounded.GatherInput().X != 0)
            {
                PlayerInstance.Player.SwitchState(_toMove);
            }
        }
    }
}