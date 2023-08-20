using ArrowPlatformer.Player;
using Godot;
using System;

namespace ArrowPlatformer.Scripts.Player.States
{
    public class PlayerIdelState : PlayerBaseState
    {
        public PlayerIdelState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory) { IsRootState = true; }

        public override void CheckSwitchStates()
        {

            if (Ctx.XYInput.X != 0)
            {
                SwitchState(Factory.MoveState());
            }
        }

    }
}
