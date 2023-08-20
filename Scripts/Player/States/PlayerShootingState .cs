using ArrowPlatformer.Player;
using Godot;
using System;

namespace ArrowPlatformer.Scripts.Player.States
{
    public class PlayerShootingState : PlayerBaseState
    {
        public PlayerShootingState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
            : base(currentContext, playerStateFactory) { IsRootState = true; }



    }
}
