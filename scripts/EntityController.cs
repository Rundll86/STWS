using Godot;
using System;
using FallingShrimp.FrontBridge;
public partial class EntityController : CharacterBody2D
{
	public enum State
	{
		WALK, STAND, SIT_ON_DOWN, SIT_ON_UP
	}
	[Export]
	public bool PlayerControllable;
	[Export]
	public float Speed;
	public AnimatedSprite2D texture;
	public CollisionShape2D hitbox;
	public State state;
	public bool eating;
	public bool autoState = true;
	public Promise<int> Move(Vector2 offset, int stepTime = 20)
	{
		GD.Print("moving to (" + offset.X + ", " + offset.Y + ")");
		autoState = false;
		int stepTimes = (int)Math.Ceiling(offset.Length() / Speed);
		Vector2 stepOffset = new(offset.X / stepTimes, offset.Y / stepTimes);
		GD.Print(stepOffset, stepTimes);
		return TimeCalc.MethodCirculator(() =>
		{
			GD.Print("test");
			MoveAndCollide(stepOffset);
		}, stepTime, stepTimes).Then(() => { autoState = true; });
	}
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
			if (autoState) state = State.STAND;
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
			if (Blocker.IsBlocked()) { return; };
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
					Mamba.WhatCanISayAsync(
						"",
						"要在" + index + "号窗口（" + Common.ShopTypes[index - 1] + "类）点餐吗？",
						new string[] { "开始点餐", "再等等" }
					).Then(selected =>
					{
						if (selected == 0)
						{
							if (Queues.GetQueueObject(index - 1).entity == Common.PlayerQueueObject.entity)
							{
								GD.Print("开始点菜");
								Ordering.Open(index - 1);
							}
							else
							{
								Mamba.WhatCanISayAsync("食堂大妈", "还没到你呢，后面排队去！");
							};
						}
						else if (selected == 1)
						{
							GD.Print("取消点菜");
						}
						return;
					});
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
					Mamba.WhatCanISayAsync(
						"",
						"要在" + numberA + "号餐桌的座位" + numberB + "用餐吗？",
						new string[] { "开始用餐", "换一个座位" }
					).Then(selected =>
					{
						if (selected == 0)
						{
							if (UserData.RealHadFoodsLength == 0)
							{
								Mamba.WhatCanISayAsync("", "你还没买饭呢，打算吃别人的？");
								return;
							}
							GD.Print("开始用餐");
							ThreadSleep.SleepAsync(100).Then((e) => { UserData.ShowEatingMessageBox(); return; });
							Common.PlayerSprite.Position = Common.LastChair.GlobalPosition;
							Common.PlayerSprite.state = State.SIT_ON_DOWN;
							if ("ABCD".Contains(Common.LastChairType))
							{
								Common.PlayerSprite.Position = new Vector2(
									Common.PlayerSprite.Position.X,
									Common.LastChair.GetParent().GetParent<Node2D>().Position.Y + 10
								);
								Common.PlayerSprite.texture.Position += Common.EatingPositionOffset;
								Common.PlayerSprite.state = State.SIT_ON_UP;
								Common.LastChair.GetParent<Node2D>().GetNode<AnimatedSprite2D>("Texture").ZIndex = 1;
							}
						}
						else if (selected == 1)
						{
							GD.Print("取消用餐");
						}
						return;
					});
				}
				else if (resultName == "QueueDetector")
				{
					EntityController npc = result.GetParent<EntityController>();
					string npcName = npc.Name.ToString();
					int npcID = int.Parse(npcName[^3..].TrimStart('0'));
					int windowID = int.Parse(npc.GetParent().GetParent().Name.ToString()[^1..]);
					Mamba.WhatCanISayAsync(npcName, "喂，你想干嘛？", new string[] { "插队", "借过一下", "没啥" }).Then(selected =>
					{
						GD.Print(selected);
						if (selected == 0)
						{
							GD.Print("在窗口" + windowID + "的NPC" + npcID + "的位置插队");
							npc.Move(new Vector2(0, 42));
						}
						else if (selected == 1)
						{
							Mamba.WhatCanISayAsync("Kobe Bryant", "What can I say?Mamba out!");
						}
					});
				}
			}
		}
	}
}
