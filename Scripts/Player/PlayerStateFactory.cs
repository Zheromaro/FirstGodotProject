using ArrowPlatformer.Scripts.Player.States;
using Godot.Collections;

namespace ArrowPlatformer.Player;

public class PlayerStateFactory
{
    private PlayerStateMachine _context;
    //Dictionary<string, PlayerBaseState> _states = new Dictionary<string, PlayerBaseState>();
    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        _context = currentContext;
    }


    public PlayerMoveState MoveState()
    {
        return new PlayerMoveState(_context, this);
    } 
    public PlayerIdelState IdelState()
    {
        return new PlayerIdelState(_context, this);
    } 
    public PlayerFallState FallState()
    {
        return new PlayerFallState(_context, this);
    } 
    public PlayerJumpState JumpState()
    {
        return new PlayerJumpState(_context, this);
    } 

}
