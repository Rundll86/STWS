using Godot;
using System;
using System.Linq;

public partial class Ordering : ScrollController
{
	static ScrollController OrderUI;
	static Food[] LastOpenCards;
	public override void _Ready()
	{
		OrderUI = this;
		LastOpenCards = Array.Empty<Food>();
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
						Message.ShowMessage("notEnoughMoney", "你可没有那么多钱！");
						return;
					}
					if (UserData.RealOrderedLength == 10)
					{
						Message.ShowMessage("tooMuchItems", "你选择的食物太多了！");
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
			Message.ShowMessage("noFood", "你还没有选择任何食物！");
			return;
		}
		UserData.Money -= UserData.TotalPrice;
		UserData.HadFoods = UserData.HadFoods.Concat(UserData.Ordered).ToArray();
		UserData.Ordered = new Food[10];
	}
	public static void Close()
	{
		OrderUI.Visible = false;
		Blocker.Unblock();
		Common.MoneyLabel.Position = Common.MoneyLabelOldPosition;
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
		Common.MoneyLabel.Position = new Vector2(-533, -300);
		Blocker.Block();
		OrderUI.Visible = true;
		Food[] foods = Common.RandomFoodList[index];
		OrderUI.MinOffset.Y = -360 - (foods.Length - 2) * 330;
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
	}
}