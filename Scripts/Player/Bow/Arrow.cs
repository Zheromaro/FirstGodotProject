using Godot;
using System;

public partial class Arrow : Node
{
	[Export] private float _mass = 0.25f;
	[Export] private float _gravity = 9f;
	private bool lanched = false;
	private Vector2 velocity = Vector2.Zero;


	public void launch(Vector2 initialVelocity)
	{
		lanched = true;
		velocity = initialVelocity;
	}
}
