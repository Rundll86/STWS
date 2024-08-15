using Godot;
using System;

public partial class Mamba : Button
{
	private static Action _savedCallback;
	public static Button MambaBox;
	public static RichTextLabel MambaText;
	public static Button MambaButton;
	public static Button MambaButtonDuplicated;
	public static Button ExampleOption;
	public override void _Ready()
	{
		MambaBox = this;
		MambaText = GetNode<RichTextLabel>("MambaText");
		MambaButton = GetNode<Button>("Button");
		MambaButtonDuplicated = (Button)MambaButton.Duplicate();
		MambaButton.QueueFree();
		ExampleOption = GetNode<Button>("/root/WorldController/InitilizatorC/ExampleOption");
		ExampleOption.GetParent().RemoveChild(ExampleOption);
		Visible = false;
	}
	public delegate void SayingCallback(int selected, string selectedName);
	public static void WhatCanISay(string CharacterName, string Message, string[] options = null, SayingCallback callback = null)
	{
		Blocker.PauseTime();
		MambaText.Text = Message;
		if (options == null)
		{
			MambaButton = (Button)MambaButtonDuplicated.Duplicate();
			_savedCallback = () =>
			{
				Blocker.ResumeTime();
				MambaButton.QueueFree();
				MambaBox.Visible = false;
				callback?.Invoke(-1, "");
			};
			MambaButton.Pressed += _savedCallback;
			MambaBox.AddChild(MambaButton);
		}
		else
		{
			for (int i = 0; i < options.Length; i++)
			{
				int savedI = i;
				Button currentOption = (Button)ExampleOption.Duplicate();
				currentOption.Text = options[i];
				currentOption.Name = "Option" + savedI.ToString();
				currentOption.Position += new Vector2(0, 69 * i);
				_savedCallback = () =>
				{
					Blocker.ResumeTime();
					MambaBox.Visible = false;
					for (int j = 0; j < i; j++)
					{
						MambaBox.RemoveChild(MambaBox.GetNode("Option" + j.ToString()));
						GD.Print("removed option " + j.ToString());
					}
					callback?.Invoke(savedI, options[savedI]);
				};
				currentOption.Pressed += _savedCallback;
				MambaBox.AddChild(currentOption);
			}
		}
		MambaBox.Visible = true;
	}
	public static Promise<int> WhatCanISayAsync(string CharacterName, string Message, string[] options = null)
	{
		return new Promise<int>((resolve, reject) =>
		{
			Blocker.PauseTime();
			MambaText.Text = Message;
			if (options == null)
			{
				MambaButton = (Button)MambaButtonDuplicated.Duplicate();
				_savedCallback = () =>
				{
					Blocker.ResumeTime();
					MambaButton.QueueFree();
					MambaBox.Visible = false;
					resolve(-1);
				};
				MambaButton.Pressed += _savedCallback;
				MambaBox.AddChild(MambaButton);
			}
			else
			{
				for (int i = 0; i < options.Length; i++)
				{
					int savedI = i;
					Button currentOption = (Button)ExampleOption.Duplicate();
					currentOption.Text = options[i];
					currentOption.Name = "Option" + savedI.ToString();
					currentOption.Position += new Vector2(0, 69 * i);
					_savedCallback = () =>
					{
						Blocker.ResumeTime();
						MambaBox.Visible = false;
						for (int j = 0; j < i; j++)
						{
							MambaBox.RemoveChild(MambaBox.GetNode("Option" + j.ToString()));
							GD.Print("removed option " + j.ToString());
						}
						resolve(savedI);
					};
					currentOption.Pressed += _savedCallback;
					MambaBox.AddChild(currentOption);
				}
			}
			MambaBox.Visible = true;
		});
	}
}
