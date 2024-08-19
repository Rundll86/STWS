using Godot;
using System;

public partial class Test : Node2D
{
	bool speeding = false;
	public override void _Ready()
	{
	}
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("test"))
		{
			speeding = !speeding;
			Common.PlayerSprite.Speed = speeding ? 20 : Common.savedPlayerDefaultSpeed;
		}
	}
}
