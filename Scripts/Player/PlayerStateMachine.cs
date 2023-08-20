using Godot;
using System;

namespace ArrowPlatformer.Player;

public partial class PlayerStateMachine : CharacterBody2D
{
    #region states valus

    [ExportGroup("Movemet")]
    [Export] public bool UseRawInput = true;
    [Export] public float MoveSpeed = 200f;
    [Export] public float Acceleration = 10f;
    [Export] public float Decceleration = 15f;
    public Vector2 XYInput;

    [ExportGroup("Jump")]
    [Export] public float JumpHeight = 15;
    [Export] public float JumpTimeToPeak = 0.1f;
    [Export] public float JumpTimeToDesand = 0.1f;
    [Export] public float TerminalVelocity = 1000; // normaly the more he fall the faster he becomes, so this is the speed limits

    [ExportCategory("GetNodes")]
    [Export] private NodePath _animationTreeNodePath;
    private AnimationTree _animationTree;
    #endregion

    public Vector2 NewVelocity;

    public PlayerBaseState CurrentState;
    private PlayerStateFactory _states;

    public override void _Ready()
    {
        _states = new PlayerStateFactory(this);
        CurrentState = _states.MoveState();
        CurrentState.EnterState();

        _animationTree = GetNode<AnimationTree>(_animationTreeNodePath);
    }

    public override void _Process(double delta)
    {
        GatherInput();
        CurrentState.ProcessStates((float)delta);
    }

    public override void _PhysicsProcess(double delta)
    {
        NewVelocity = Velocity;
        CurrentState.PhysicsProcessStates((float)delta);
        Velocity = NewVelocity;

        MoveAndSlide();
    }
    private void GatherInput()
    {
        XYInput = Input.GetVector(InputNames.Left, InputNames.Right, InputNames.Up, InputNames.Down);
        if (UseRawInput) XYInput = XYInput.GetRaw();
    }
}
