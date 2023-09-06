using Godot;
using System;

namespace Scripts.StateMachine
{
	public partial class _Transition : Stat
	{
		public float delay_seconds;

        public NodePath To;

	}
}
