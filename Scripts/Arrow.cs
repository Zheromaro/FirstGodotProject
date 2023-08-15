using Godot;
using System;

public partial class Arrow : Area2D
{
	[Export] private float mass = 0.25f;
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
			velocity += GravityDirection * (Gravity * 0.01f) * mass;
			Position += velocity * (float)delta;
			Rotation = velocity.Angle();

			GD.Print(GravityDirection);
			GD.Print(Gravity);
			GD.Print(velocity);
        }
	}

	public void launch(Vector2 initialVelocity)
	{
		lanched = true;
		velocity = initialVelocity;
	}
}
