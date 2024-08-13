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
		if (Blocker.IsTimePaused())
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
				texture.Scale = new Vector2(Math.Abs(texture.Scale.X), texture.Scale.Y);
				moved = true;
			}
			if (Input.IsActionPressed("move_right"))
			{
				KinematicCollision2D result = MoveAndCollide(new Vector2(5, 0));
				if (result != null) { collided = result; }
				texture.Scale = new Vector2(-Math.Abs(texture.Scale.X), texture.Scale.Y);
				moved = true;
			}
			if (collided != null)
			{
				PhysicsBody2D result = collided.GetCollider() as PhysicsBody2D;
				string resultName = result.Name.ToString();
				if (resultName.StartsWith("Window"))
				{
					int index = int.Parse(resultName[^1].ToString());
					Message.ShowMessage(
						"order",
						"要在" + index + "号窗口点餐吗？",
						new string[] { "开始点餐", "再等等" },
						index - 1
					);
				}
				else if (resultName.StartsWith("Chair"))
				{
					string numberA = result.GetParent<Node2D>().Name.ToString()[^1].ToString();
					string numberB = resultName[^1].ToString();
					Message.ShowMessage(
						"eat",
						"要在座位" + numberB + numberA + "用餐吗？",
						new string[] { "开始用餐", "换一个座位" }
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
