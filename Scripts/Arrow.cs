using Godot;
using System;

public partial class Arrow : Area2D
{
	[Export] private float _mass = 0.25f;
	[Export] private float _gravity = 9f;
	private bool lanched = false;
	private Vector2 velocity = Vector2.Zero;

    public override void _Ready()
    {
		TopLevel = true;
    }

    public override void _Process(double delta)
	{
		if (lanched == true)
		{
			velocity += GravityDirection * (_gravity * 0.01f) * _mass;
			Position += velocity * (float)delta;
			Rotation = velocity.Angle();
        }
	}

	public void launch(Vector2 initialVelocity)
	{
		lanched = true;
		velocity = initialVelocity;
	}
}
