using Godot;
using System;

namespace Scripts.StateMachine
{
	[Tool]
	public partial class _State : Node
	{
		[Signal] public delegate void _StateEnteredEventHandler();
		[Signal] public delegate void _StateExitedEventHandler();
		[Signal] public delegate void _EventReceivedEventHandler(StringName @event);
		[Signal] public delegate void _StateProcessingEventHandler(float delta);
		[Signal] public delegate void _StatePhysicsProcessingEventHandler(float delta);
		[Signal] public delegate void _StateInputEventHandler(InputEvent @event);
		[Signal] public delegate void _StateUnhandledInputEventHandler(InputEvent @event);

		public bool Active { get; private set; } = false;

        private float _pendingTransitionCountdown = 0;
		private _Transition _activePendingTransition = null;
        private _Transition[] _transitions;
		private Chart _stateChart;

        public override void _Ready()
        {
			_stateChart = findChart(GetParent());

            Chart findChart(Node parent)
            {
                if (parent is Chart)
                    return parent as Chart;

                return findChart(parent.GetParent());

            }
        }

		// Runs a transition either immediately or delayed depending on the 
		// transition settings.
        private void _run_transition(_Transition transition)
		{
            if (transition.delay_seconds > 0)
                _queue_transition(transition);
            else
                _stateChart.RunTransition(transition, this);
        }

		// Queues the transition to be triggered after the delay.
		// Executes the transition immediately if the delay is 0.
        private void _queue_transition(_Transition transition)
		{
            //// queue the transition for the delay time (0 means next frame)
            //_pending_transition = transition;
            //_pending_transition_time = transition.delay_seconds;
			//
            //// enable processing when we have a transition
            //set_process(true);
        }

        public void StateEvent(StringName @event)
		{

		}

        public void StateInitialize()
		{

		}

        public void StateEnter()
		{

		}

		public void HandleTransition(_Transition transition , _State stateToTransition)
		{

		}

    }
}