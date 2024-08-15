using Godot;
using System;

public partial class EntityController : CharacterBody2D
{
	public enum State
	{
		WALK, STAND, SIT_ON_DOWN, SIT_ON_UP
	}
	[Export]
	bool PlayerControllable;
	[Export]
	float Speed;
	public AnimatedSprite2D texture;
	public CollisionShape2D hitbox;
	public State state;
	public bool eating;
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
		if (state == State.WALK)
		{
			texture.Play("walk");
			state = State.STAND;
		}
		else if (state == State.STAND)
		{
			texture.Play("stand");
		}
		else if (state == State.SIT_ON_DOWN)
		{
			texture.Play("sitDown" + (eating ? "-Eat" : ""));
		}
		else if (state == State.SIT_ON_UP)
		{
			texture.Play("sitUp" + (eating ? "-Eat" : ""));
		}
		if (PlayerControllable)
		{
			Blocker.CheckBlockedAndThrow();
			KinematicCollision2D collided = null;
			if (Input.IsActionPressed("move_up"))
			{
				KinematicCollision2D result = MoveAndCollide(new Vector2(0, -Speed));
				if (result != null) { collided = result; };
				state = State.WALK;
			}
			if (Input.IsActionPressed("move_down"))
			{
				KinematicCollision2D result = MoveAndCollide(new Vector2(0, Speed));
				if (result != null) { collided = result; }
				state = State.WALK;
			}
			if (Input.IsActionPressed("move_left"))
			{
				KinematicCollision2D result = MoveAndCollide(new Vector2(-Speed, 0));
				if (result != null) { collided = result; }
				texture.Scale = new Vector2(Math.Abs(texture.Scale.X), texture.Scale.Y);
				state = State.WALK;
			}
			if (Input.IsActionPressed("move_right"))
			{
				KinematicCollision2D result = MoveAndCollide(new Vector2(Speed, 0));
				if (result != null) { collided = result; }
				texture.Scale = new Vector2(-Math.Abs(texture.Scale.X), texture.Scale.Y);
				state = State.WALK;
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
					Common.LastChair = (StaticBody2D)result;
					Node2D resultParent = (Node2D)result.GetParent().GetParent();
					string resultParentName = resultParent.Name.ToString();
					GD.Print(resultParentName);
					string numberA = resultParentName[^3..].TrimStart('0');
					string numberB = resultName[^1].ToString();
					Common.LastChairType = numberB;
					Message.ShowMessage(
						"eat",
						"要在" + numberA + "号餐桌的座位" + numberB + "用餐吗？",
						new string[] { "开始用餐", "换一个座位" }
					);
				}
				else if (resultName == "Tester")
				{
					Mamba.WhatCanISay("", "测试对话框（同步函数）", null, (_, _) =>
					{
						Mamba.WhatCanISay("", "测试可选回答的对话框", new string[] { "选项一", "オプション II", "Option3" }, (_, name) =>
						{
							Mamba.WhatCanISay("", "你刚才选择了选项[" + name + "]！", null, (_, _) =>
							{
								Mamba.WhatCanISay("", "你吃饭了吗？", new string[] { "吃了", "没吃" }, (selected, _) =>
								{
									Mamba.WhatCanISay("", "你" + (selected == 0 ? "吃饭了" : "没吃饭") + "");
								});
							});
						});
					});
				}
				else if (resultName == "Tester2")
				{
					Mamba.WhatCanISayAsync("", "测试对话框（异步函数-原生async）").Then((e) =>
					{
						return Mamba.WhatCanISayAsync("", "Man!What can i say???");
					}).Then((e) =>
					{
						return Mamba.WhatCanISayAsync("", "Where's my HELICOPTER???", new string[] { "In the sky.", "In the ground." });
					}).Then((e) =>
					{
						GD.Print(e);
						return Mamba.WhatCanISayAsync("", "No!Your mother is dead!!!");
					}).Then((e) =>
					{
						return Mamba.WhatCanISayAsync("", "测试对话框（异步函数-Promise）");
					}).Then((e) =>
					{
						return Mamba.WhatCanISayAsync("", "选择一个选项", new string[] { "选项一", "选项二", "选项三" });
					}).Then((e) =>
					{
						return Mamba.WhatCanISayAsync("", "你刚才选择了选项[" + (e + 1).ToString() + "]");
					});
				}
			}
		}
	}
}
