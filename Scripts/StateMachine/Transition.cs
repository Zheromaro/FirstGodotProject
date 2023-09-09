using Godot;
using System;

namespace Scripts.StateMachine
{
	public partial class Transition : Node
	{
		public float delay_seconds;

        public NodePath To;

	}
}
