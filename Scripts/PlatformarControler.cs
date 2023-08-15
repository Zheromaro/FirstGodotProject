using Godot;
using System;

public partial class PlatformarControler : CharacterBody2D
{
    private AnimatedSprite2D _sprite;

    #region Initialize

    public override void _Ready()
    {
        GatherRequirements();

        _jumpForce = ((2 * _jumpHeight) / _jumpTimeToPeak) * -1;
        _jumpGravity = ((-2 * _jumpHeight) / Mathf.Pow(_jumpTimeToPeak, 2)) * -1;
        _fallGravity = ((-2 * _jumpHeight) / Mathf.Pow(_jumpTimeToDesand, 2)) * -1;
    }

    private void GatherRequirements()
    {
        _sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    #endregion

    #region Process

    public override void _Process(double delta)
    {
        GatherInput();
    }

    public override void _PhysicsProcess(double delta)
    {
        CalculateVelocity((float)delta);

        MoveAndSlide();
    }

    #endregion

    #region Input

    [Export] private bool _useRawInput = true;
    private Vector2 _input;
    private bool _didJump = false;

    /// <summary>
    /// Gather input from the player
    /// </summary>
    private void GatherInput()
    {
        _input = Input.GetVector(InputNames.Left, InputNames.Right, InputNames.Up, InputNames.Down);
        if (_useRawInput) _input = _input.GetRaw();

        if (Input.IsActionJustPressed(InputNames.Jump))
        {
            _didJump = true;
        }
    }

    #endregion

    #region Velocity

    private Vector2 _velocity;
    private float _targetVelocityX;
    private float _previousVelocityY, _newVelocityY;

    [Export] private float _moveSpeed = 200f;
    [Export] private float _acceleration = 10f;
    [Export] private float _decceleration = 15f;

    /// <summary>
    /// Calculate the bi-direction velocity and apply it to the character body 
    /// </summary>
    /// <param name="delta"></param>
    private void CalculateVelocity(float delta)
    {
        _velocity = Velocity;
        CalculateVelocityY(ref _velocity, delta);
        CalculateVelocityX(ref _velocity);
        Velocity = _velocity;
    }

    private void CalculateVelocityY(ref Vector2 vel, float delta)
    {
        ApplyGravity(ref vel, delta);
        HandleJump(ref vel);
    }

    private void CalculateVelocityX(ref Vector2 vel)
    {
        _targetVelocityX = _input.X * _moveSpeed;
        vel.X = Mathf.MoveToward(vel.X, _targetVelocityX,
            Mathf.Sign(_input.X) == Mathf.Sign(_velocity.X) ? _acceleration : _decceleration);
    }

    #endregion

    #region Gravity

    [Export] private float _terminalVelocity = 1000; // normaly the more he fall the faster he becomes, so this is the speed limits
    private float _gravity(ref Vector2 vel) 
    {
        return vel.Y < 0 ? _jumpGravity : _fallGravity;
    }

    /// <summary>
    /// Aplly gravity to the character body velocity
    /// </summary>
    /// <param name="vel"></param>
    /// <param name="delta"></param>
    private void ApplyGravity(ref Vector2 vel, float delta)
    {
        if (IsOnFloor()) return;

        _previousVelocityY = vel.Y;
        _newVelocityY = vel.Y + _gravity(ref vel) * delta;
        _newVelocityY = Mathf.Clamp(_newVelocityY, -Mathf.Inf, _terminalVelocity);

        vel.Y = (_previousVelocityY + _newVelocityY) * 0.5f;
    }

    #endregion

    #region Jump

    [Export] private float _jumpHeight = 15;
    [Export] private float _jumpTimeToPeak = 0.1f;
    [Export] private float _jumpTimeToDesand = 0.1f;

    private float _jumpForce;
    private float _jumpGravity;
    private float _fallGravity;

    private void HandleJump(ref Vector2 vel)
    {
        if (!_didJump || !IsOnFloor())
        {
            _didJump = false;
            return;
        }

        _didJump = false;
        vel.Y = _jumpForce;
    }

    #endregion

}
