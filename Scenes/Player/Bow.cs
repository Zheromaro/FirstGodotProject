using Godot;
using System;

public partial class Bow : Sprite2D
{
	[Export] private NodePath _arrowPath;
	private Arrow _arrow;
	[Export] private Vector2 Vel;

    public override void _Ready()
    {
		_arrow = GetNode<Arrow>(_arrowPath);
    }

    public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed(InputNames.Fire))
		{
			_arrow.GlobalPosition = GlobalPosition;
			_arrow.launch(Vel);
		}
	}
}
