using Godot;
using System;
using System.Linq;

public partial class Ordering : ScrollController
{
	static ScrollController OrderUI;
	static FoodObject[] LastOpenCards;
	public override void _Ready()
	{
		OrderUI = this;
		LastOpenCards = Array.Empty<FoodObject>();
	}
	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (Visible)
		{
			for (int i = 0; i < LastOpenCards.Length; i++)
			{
				Sprite2D target = GetNode<Sprite2D>("FoodCard" + i.ToString());
				if (
					@event is InputEventMouseButton mouseButton &&
					mouseButton.ButtonIndex == MouseButton.Left &&
					mouseButton.Pressed &&
					target.GetRect().HasPoint(target.ToLocal(GetGlobalMousePosition()))
				)
				{
					GD.Print("Clicked: " + LastOpenCards[i].name);
					if (UserData.TotalPrice + LastOpenCards[i].price > UserData.Money)
					{
						Mamba.WhatCanISayAsync("", "电话亭出门直走然后右转！");
						return;
					}
					if (UserData.RealOrderedLength == 10)
					{
						Mamba.WhatCanISayAsync("", "买这么多吃的完么？");
						return;
					}
					UserData.Ordered[UserData.RealOrderedLength] = LastOpenCards[i];
					GD.Print("Ordered: " + LastOpenCards[i].name);
				}
			}
		}
	}
	public static void Buy()
	{
		if (UserData.RealOrderedLength == 0)
		{
			Mamba.WhatCanISayAsync("", "你也没想好吃啥啊，再想想呢？");
			return;
		}
		UserData.Money -= UserData.TotalPrice;
		UserData.HadFoods = UserData.HadFoods.Concat(UserData.Ordered).ToArray();
		UserData.Ordered = new FoodObject[10];
	}
	public static void Close()
	{
		OrderUI.Visible = false;
		Blocker.Unblock();
		LastOpenCards = null;
		Godot.Collections.Array<Node> children = OrderUI.GetChildren();
		for (int i = 0; i < 3; i++)
		{
			children.RemoveAt(0);
		}
		foreach (Node child in children)
		{
			child.QueueFree();
		}
	}
	public static void Open(int index)
	{
		Blocker.Block();
		OrderUI.Visible = true;
		FoodObject[] foods = Common.RandomFoodList[index];
		OrderUI.MinOffset.Y = -360 - (foods.Length - 2) * 330;
		LastOpenCards = foods;
		for (int i = 0; i < foods.Length; i++)
		{
			Sprite2D container = (Sprite2D)Common.ExampleFoodCard.Duplicate();
			container.Name = "FoodCard" + i.ToString();
			container.Position = new Vector2(640, 260 + 330 * i);
			container.GetNode<TextureRect>("Avatar").Texture = foods[i].avatar;
			container.GetNode<Label>("FoodName").Text = foods[i].name;
			container.GetNode<Label>("FoodPrice").Text = foods[i].price.ToString() + "G";
			container.GetNode<Label>("FoodValue").Text = foods[i].value.ToString();
			container.GetNode<Label>("FoodVC").Text = foods[i].vc.ToString();
			container.GetNode<Label>("FoodDBZ").Text = foods[i].bdz.ToString();
			container.GetNode<Label>("FoodVB").Text = foods[i].vb.ToString();
			container.GetNode<Label>("FoodCell").Text = foods[i].cellulose.ToString();
			container.GetNode<Label>("Taste1").Text = foods[i].taste.甜.ToString();
			container.GetNode<Label>("Taste2").Text = foods[i].taste.酸.ToString();
			container.GetNode<Label>("Taste3").Text = foods[i].taste.辣.ToString();
			container.GetNode<Label>("Taste4").Text = foods[i].taste.咸.ToString();
			container.GetNode<Label>("Taste5").Text = foods[i].taste.清淡.ToString();
			container.GetNode<Label>("Taste6").Text = foods[i].taste.油.ToString();
			OrderUI.AddChild(container);
		}
	}
}