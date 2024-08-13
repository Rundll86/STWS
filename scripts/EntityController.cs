using Godot;
using System;

public partial class EntityController : CharacterBody2D
{
	[Export]
	bool PlayerControllable;
	AnimatedSprite2D texture;
	CollisionShape2D hitbox;
	public override void _Ready()
	{
		texture = GetNode<AnimatedSprite2D>("Texture");
		hitbox = GetNode<CollisionShape2D>("Hitbox");
	}
	public override void _Process(double delta)
	{
		if (Blocker.IsBlocked())
		{
			texture.Pause();
			return;
		}
		if (Common.PlayerSprite.Position.Y < Position.Y)
		{
			ZIndex = 1;
		}
		else
		{
			ZIndex = 0;
		}
		bool moved = false;
		if (PlayerControllable)
		{
			Blocker.CheckBlockedAndThrow();
			KinematicCollision2D collided = null;
			if (Input.IsActionPressed("move_up"))
			{
				KinematicCollision2D result = MoveAndCollide(new Vector2(0, -5));
				if (result != null) { collided = result; };
				moved = true;
			}
			if (Input.IsActionPressed("move_down"))
			{
				KinematicCollision2D result = MoveAndCollide(new Vector2(0, 5));
				if (result != null) { collided = result; }
				moved = true;
			}
			if (Input.IsActionPressed("move_left"))
			{
				KinematicCollision2D result = MoveAndCollide(new Vector2(-5, 0));
				if (result != null) { collided = result; }
				moved = true;
			}
			if (Input.IsActionPressed("move_right"))
			{
				KinematicCollision2D result = MoveAndCollide(new Vector2(5, 0));
				if (result != null) { collided = result; }
				moved = true;
			}
			if (collided != null)
			{
				CharacterBody2D result = collided.GetCollider() as CharacterBody2D;
				if (result.Name == "Window1")
				{
					Message.ShowMessage("order", "现在点菜吗？", Common.CreateArrayBy2Items("点菜", "等会再来"));
				};
			}
		}
		if (moved)
		{
			texture.Play("walk");
		}
		else
		{
			texture.Play("stand");
		}
	}
}
