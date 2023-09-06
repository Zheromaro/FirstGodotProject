using Godot;
using System;

namespace Scripts.Player.States
{
    public partial class ComponentGrounded : Node
    {
        [Export] private bool UseRawInput;

        public Vector2 GatherInput()
        {
            var input = Input.GetVector(InputNames.Left, InputNames.Right, InputNames.Up, InputNames.Down);
            if (UseRawInput) input = input.GetRaw();
            return input;
        }
    }
}