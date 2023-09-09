using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scripts.StateMachine
{
	public partial class Chart : Node
	{

		[Signal] public delegate void _before_transitionEventHandler(_Transition transition, _State sourceState);
		[Signal] public delegate void _EventReceivedEventHandler(StringName @event);

		private bool _isProcessingTransitions = false;
		private bool _isProcessingSignal = false;
		private Dictionary<_Transition, _State> _queuedTransitions;
		private StringName[] _queuedEvents;
		private _State _rootState = null;

		private Dictionary<StringName, StringName> _expressionGuardProperties;


		public override void _Ready()
		{
			if (Engine.IsEditorHint())
				return;

			if (GetChildCount() != 1) {
				GD.PushError("StateChart must have exactly one child");
				return;
			}

			var child = GetChild(0);
			if (child is _State == false) {
				GD.PushError("StateMachine's child must be a State");
				return;
			}


			_rootState = child as _State;

			_rootState.StateInitialize();
			_rootState.StateEnter();
		}

		public void SendEvent(StringName @event)
		{
			if (_rootState == null) {
				GD.PushError("StateMachine is not initialized");
				return;
			}

            _queuedEvents.Append(@event);

			if (_isProcessingSignal == true) 
				return;


            _isProcessingSignal = true;

            foreach (var next_event in _queuedEvents)
            {
                EmitSignal(SignalName._EventReceived, @event);
                _rootState.StateEvent(next_event);
            }
            _queuedEvents = null;

            _isProcessingSignal = false;

        }

		public void RunTransition(_Transition transition, _State sourceState)
		{
            _queuedTransitions.Add(transition, sourceState);

            if (_isProcessingTransitions == true) {
				return;
			}

            foreach (var next_transition in _queuedTransitions.Reverse())
			{
                var transition_Key = next_transition.Key;
                var sourceState_Value = next_transition.Value;

                if (sourceState_Value.Active)
                {
                    EmitSignal(SignalName._before_transition, transition_Key, sourceState_Value);
                    sourceState_Value.HandleTransition(transition_Key, sourceState_Value);
                }
                else
                {
                    GD.PushWarning("Ignoring request for transitioning from ", sourceState.Name,
                    " to ", transition.To, " as the source state is no longer active. " +
                    "Check whether your trigger multiple state changes within a single frame.");
                }
            }
		}

		private string[] GetConfigurationWarnings() 
		{
			string[] warnings = null;

			if (GetChildCount() != 1) {
				warnings.Append("StateChart must have exactly one child");
			}

            if (GetChild(0) is _State == false){
				warnings.Append("StateChart's child must be a State");
            }

            return warnings;
        }

		private void SetExpressionGuardProperty(StringName name, StringName value) {
            _expressionGuardProperties[name] = value;
		}


	}
}
