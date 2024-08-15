using Godot;
using System;

public partial class PositionLimiter : PositionLock
{
	[Export]
	Rect2 Limit;
	public override void _Ready()
	{
	}
	public override void _Process(double delta)
	{
		base._Process(delta);
		if (Position.X < Limit.Position.X)
		{
			Position = new Vector2(Limit.Position.X, Position.Y);
		}
		if (Position.X > Limit.End.X)
		{
			Position = new Vector2(Limit.End.X, Position.Y);
		}
		if (Position.Y < Limit.Position.Y)
		{
			Position = new Vector2(Position.X, Limit.Position.Y);
		}
		if (Position.Y > Limit.End.Y)
		{
			Position = new Vector2(Position.X, Limit.End.Y);
		}
	}
}
