using Godot;
using System;

public partial class Ordering : ScrollController
{
	Label BoughtFoodLabel;
	static ScrollController OrderUI;
	static Food[] LastOpenCards;
	public override void _Ready()
	{
		OrderUI = this;
		BoughtFoodLabel = GetNode<Label>("BoughtFoodList");
	}
	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (Visible)
		{
			for (int i = 0; i < LastOpenCards.Length; i++)
			{
				if (
					@event is InputEventMouseButton mouseButton &&
					mouseButton.ButtonIndex == MouseButton.Left &&
					mouseButton.Pressed &&
					GetNode<ColorRect>("FoodCard" + i.ToString()).GetRect().HasPoint(GetLocalMousePosition())
				)
				{
					GD.Print("Clicked: " + LastOpenCards[i].name);
					if (UserData.TotalPrice + LastOpenCards[i].price > UserData.Money)
					{
						Message.ShowMessage("notEnoughMoney", "你钱不够了！");
						return;
					}
					if (UserData.RealOrderedLength == 10)
					{
						Message.ShowMessage("tooMuchItems", "你点的菜太多了！");
						return;
					}
					UserData.Ordered[UserData.RealOrderedLength] = LastOpenCards[i];
					BoughtFoodLabel.Text = "已购买：" + UserData.BoughtFoodNames;
					GD.Print("Ordered: " + LastOpenCards[i].name);
				}
			}
		}
	}
	public static void Close()
	{
		OrderUI.Visible = false;
		Blocker.Unblock();
		Common.MoneyLabel.Position = new Vector2(-56, 53);
	}
	public static void Open(int index)
	{
		Common.MoneyLabel.Position = new Vector2(-510, -332);
		Blocker.Block();
		OrderUI.Visible = true;
		OrderUI.MinOffset.Y = -360 - (index - 2) * 330;
		Godot.Collections.Array<Node> children = OrderUI.GetChildren();
		for (int i = 0; i < 3; i++)
		{
			children.RemoveAt(0);
		}
		foreach (Node child in children)
		{
			child.QueueFree();
		}
		Food[] foods = Common.RandomFoodList[index];
		GD.Print(foods.Length);
		LastOpenCards = foods;
		for (int i = 0; i < foods.Length; i++)
		{
			ColorRect container = new()
			{
				Color = new Color(1, 1, 1),
				Size = new Vector2(400, 300),
				Position = new Vector2(440, 60 + 330 * i),
				Name = "FoodCard" + i.ToString()
			};
			Node2D infos = new()
			{
				Position = new Vector2(200, 138),
				Name = "FoodInfos"
			};
			Label nameLabel = new()
			{
				Text = foods[i].name,
				Position = new Vector2(-200, -36),
				Size = new Vector2(400, 65),
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center,
				Name = "FoodName"
			};
			nameLabel.AddThemeColorOverride("font_color", new Color(0, 0, 0));
			nameLabel.AddThemeFontSizeOverride("font_size", 30);
			Label subNameLabel = new()
			{
				Text = Food.GetFoodSubName(foods[i].name),
				Position = new Vector2(0, 42),
				Size = new Vector2(400, 28),
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center,
				Name = "FoodSubName"
			};
			subNameLabel.AddThemeColorOverride("font_color", new Color(90 / 255, 90 / 255, 90 / 255));
			subNameLabel.AddThemeFontSizeOverride("font_size", 15);
			Label contentLabel = new()
			{
				Text = Food.GetFoodContent(foods[i].name),
				Position = new Vector2(-200, 30),
				Size = new Vector2(400, 72),
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center,
				Name = "FoodContent"
			};
			contentLabel.AddThemeColorOverride("font_color", new Color(64 / 255, 64 / 255, 64 / 255));
			contentLabel.AddThemeFontSizeOverride("font_size", 15);
			Label priceLabel = new()
			{
				Text = foods[i].price.ToString() + "G",
				Position = new Vector2(-200, 100),
				Size = new Vector2(400, 42),
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center,
				Name = "FoodPrice"
			};
			priceLabel.AddThemeColorOverride("font_color", new Color(0, 0, 0));
			priceLabel.AddThemeFontSizeOverride("font_size", 20);
			nameLabel.AddChild(subNameLabel);
			infos.AddChild(nameLabel);
			infos.AddChild(contentLabel);
			infos.AddChild(priceLabel);
			container.AddChild(infos);
			container.AddChild(
				new TextureRect()
				{
					Size = new Vector2(100, 100),
					ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize,
					Position = new Vector2(150, 38),
					Texture = Common.FoodIcon,
					Name = "FoodIcon"
				}
			);
			OrderUI.AddChild(container);
		}
		children = OrderUI.GetChildren();
	}
}