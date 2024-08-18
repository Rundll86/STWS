using Godot;
using System;

public partial class PositionLock : Node2D
{
	[Export]
	public Node2D target;
	[Export]
	public Vector2 offset;
	[Export]
	public bool enabledPositionLock;
	public override void _Process(double delta)
	{
		if (target != null && enabledPositionLock)
		{
			Position = target.Position + offset;
		}
	}
}
