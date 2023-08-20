using Godot;
using System;

namespace ArrowPlatformer.Player;

public abstract class PlayerBaseState
{
    protected bool IsRootState = false;
    protected PlayerStateMachine Ctx;
    protected PlayerStateFactory Factory;
    protected PlayerBaseState CurrentSuperState;
    protected PlayerBaseState CurrentSubState;

    public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) { 
        Ctx = currentContext;
        Factory = playerStateFactory;
    }


    public virtual void EnterState() { }
    public virtual void ProcessState(float delta) { }
    public virtual void PhysicsProcessState(float delta) { }
    public virtual void ExitState() { }
    public virtual void CheckSwitchStates() { }


    public void ProcessStates(float delta)
    {
        if(CurrentSubState != null )
        {
            CurrentSubState.ProcessState(delta);
            CurrentSubState.CheckSwitchStates();
        }

        ProcessState(delta);
        CheckSwitchStates();
    }
    public void PhysicsProcessStates(float delta)
    {
        PhysicsProcessState(delta);
        if (CurrentSubState != null)
        {
            CurrentSubState.PhysicsProcessState(delta);
        }
    }
    private void ExitStates()
    {
        ExitState();
        if (CurrentSubState != null ) 
        {
            CurrentSubState.ExitState();
        }
    }


    protected void SwitchState(PlayerBaseState newState) 
    {
        ExitStates();

        if(newState != null )
            newState.EnterState();

        if (IsRootState)
        {
            Ctx.CurrentState = newState;
        }
        else if(CurrentSuperState != null)
        {
            CurrentSuperState.SetSubState(newState);
        }
    }
    protected void SetSuperState(PlayerBaseState newSuperState) 
    {
        CurrentSuperState = newSuperState;
    }
    protected void SetSubState(PlayerBaseState newSubState) 
    {
        CurrentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}
