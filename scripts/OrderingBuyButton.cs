using Godot;
using System;

public partial class OrderingBuyButton : Button
{
	public override void _Ready()
	{
		Pressed += OnPressed;
	}
	void OnPressed()
	{
		Common.SceneTree.ChangeSceneToFile("res://main.tscn");
	}
}
