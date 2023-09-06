using Godot;

namespace Scripts.Player
{
    public partial class PlayerInstance : CharacterBody2D
    {
        public static PlayerInstance Player;

        [Export] private NodePath _stateChartPath;
        private Node _stateChart;

        public override void _Ready()
        {
            if (Player != null && Player != this)
            {
                QueueFree();
            }
            else
            {
                Player = this;
            }

            _stateChart = GetNode<Node>(_stateChartPath);
        }

        public void SwitchState(string eventName)
        {
            _stateChart.Call("send_event", eventName);
        }
    }
}
