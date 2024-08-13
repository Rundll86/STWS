using Godot;
using System;
using System.Linq;

public partial class Ordering : ScrollController
{
	public override void _Ready()
	{
		base.SetInstanceMe(this);
		for (int i = 0; i < Food.FoodList.Length; i++)
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
				Text = Food.FoodList[i].name,
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
				Text = Food.GetFoodSubName(Food.FoodList[i].name),
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
				Text = Food.GetFoodContent(Food.FoodList[i].name),
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
				Text = Food.FoodList[i].price.ToString() + "G",
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
			AddChild(container);
		}
	}
	public override void _Input(InputEvent @event)
	{
		GD.Print("OldPosition" + Position);
		base._Input(@event);
		GD.Print("NewPosition" + Position);
		if (Visible)
		{
			for (int i = 0; i < Food.FoodList.Length; i++)
			{
				if (
					@event is InputEventMouseButton mouseButton &&
					mouseButton.ButtonIndex == MouseButton.Left &&
					mouseButton.Pressed &&
					GetNode<ColorRect>("FoodCard" + i.ToString()).GetRect().HasPoint(GetLocalMousePosition())
				)
				{
					GD.Print("Clicked: " + Food.FoodList[i].name);
					if (UserData.TotalPrice + Food.FoodList[i].price > UserData.Money)
					{
						Message.ShowMessage("notEnoughMoney", "你钱不够了！");
						return;
					}
					if (UserData.Ordered.Length == 10)
					{
						Message.ShowMessage("tooMuchItems", "你点的菜太多了！");
						return;
					}
					UserData.Ordered.Append(Food.FoodList[i]);
					GD.Print("Ordered: " + Food.FoodList[i].name);
				}
			}
		}
	}
}