using Godot;
using System;

namespace Scripts.Player.States
{
    public partial class ComponentAirBoundIdel : Node
    {

        [ExportGroup("EventNames")]
        [Export] private string _toJump;
        [Export] private string _toFall;

        public void _StateProcessing()
        {
            if (Input.IsActionJustPressed(InputNames.Jump))
                PlayerInstance.Player.SwitchState(_toJump);
            else if (PlayerInstance.Player.IsOnFloor() == false)
                PlayerInstance.Player.SwitchState(_toFall);
        }

    }
}