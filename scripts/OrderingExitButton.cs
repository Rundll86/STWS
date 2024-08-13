using Godot;
using System;

public partial class OrderingExitButton : Button
{
	public override void _Ready()
	{
		Pressed += OnPressed;
	}
	void OnPressed()
	{
		Ordering.Close();
	}
}
