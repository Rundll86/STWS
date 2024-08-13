using Godot;
using System;

public partial class EntityController : CharacterBody2D
{
	[Export]
	bool PlayerControllable;
	public AnimatedSprite2D texture;
	public CollisionShape2D hitbox;
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
		if (Common.PlayerSprite != this && Common.PlayerSprite.Position.Y < texture.GlobalPosition.Y)
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
				texture.Scale = new Vector2(2, 2);
				moved = true;
			}
			if (Input.IsActionPressed("move_right"))
			{
				KinematicCollision2D result = MoveAndCollide(new Vector2(5, 0));
				if (result != null) { collided = result; }
				texture.Scale = new Vector2(-2, 2);
				moved = true;
			}
			if (collided != null)
			{
				CharacterBody2D result = collided.GetCollider() as CharacterBody2D;
				string resultName = result.Name.ToString();
				int index = int.Parse(resultName[^1].ToString());
				if (resultName.StartsWith("Window"))
				{
					Message.ShowMessage(
						"order",
						"要在" + index + "号窗口点餐吗？",
						new string[] { "开始点餐", "再等等" },
						index - 1
					);
				}
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
