using Godot;
using System;

public partial class YSortor : StaticBody2D
{
	[Export]
	Node2D Target;
	AnimatedSprite2D texture;
	public override void _Ready()
	{
		texture = Target.GetNode<AnimatedSprite2D>("Texture");
	}
	public override void _Process(double delta)
	{
		if (Target.Position.Y < texture.GlobalPosition.Y)
		{
			ZIndex = 1;
		}
		else
		{
			ZIndex = 0;
		}
	}
}
