using Godot;
using System;

public partial class AnimationPlayStart : AnimationPlayer
{
	[Export]
	StringName Target;
	public override void _Ready()
	{
		Play(Target);
	}
	public override void _Process(double delta)
	{
	}
}
