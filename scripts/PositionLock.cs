using Godot;
using System;

public partial class PositionLock : Node2D
{
	[Export]
	Node2D target;
	[Export]
	Vector2 offset;
	[Export]
	bool enabledPositionLock;
	public override void _Process(double delta)
	{
		if (target != null && enabledPositionLock)
		{
			Position = target.Position + offset;
		}
	}
}
