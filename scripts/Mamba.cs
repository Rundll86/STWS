using Godot;
using System;
using FallingShrimp.FrontBridge;
public partial class Mamba : TextureRect
{
	private static Action _savedCallback;
	public static TextureRect MambaBox;
	public static RichTextLabel MambaText;
	public static AnimationPlayer MambaTextAnimator;
	public static TextureRect MambaTexture;
	public static Button MambaButtonSubstance;
	public static Button MambaButtonDuplicated;
	public static TextureButton ExampleOption;
	public static AnimationPlayer Animator;
	public static Label MambaCharacter;
	public static string[] savedOptions;
	public override void _Ready()
	{
		MambaBox = this;
		Animator = GetNode<AnimationPlayer>("Animator");
		MambaText = GetNode<RichTextLabel>("MambaText");
		MambaTextAnimator = MambaText.GetNode<AnimationPlayer>("Animator");
		MambaTexture = GetNode<TextureRect>("MambaTexture");
		MambaButtonSubstance = GetNode<Button>("Button");
		MambaButtonSubstance.Visible = true;
		MambaButtonDuplicated = (Button)MambaButtonSubstance.Duplicate();
		MambaButtonSubstance.QueueFree();
		ExampleOption = GetNode<TextureButton>("/root/WorldController/InitilizatorC/ExampleOption");
		ExampleOption.GetParent().RemoveChild(ExampleOption);
		MambaCharacter = GetNode<Label>("MambaCharacter");
	}
	public static Promise<int> WhatCanISayAsync(string CharacterName, string Message, string[] options = null)
	{
		return new Promise<int>((resolve, reject) =>
		{
			int leaveTime = 350;
			Animator.Play("join");
			Blocker.PauseTime();
			if (CharacterName == "")
			{
				MambaBox.Texture = GD.Load<Texture2D>("res://resources/talkingbox/system.png");
				MambaTextAnimator.Play("big");
				MambaCharacter.Visible = false;
				MambaTexture.Visible = false;
			}
			else
			{
				MambaBox.Texture = GD.Load<Texture2D>("res://resources/talkingbox/character.png");
				MambaTextAnimator.Play("small");
				MambaCharacter.Visible = true;
				MambaCharacter.Text = CharacterName;
				MambaTexture.Visible = true;
				MambaTexture.Texture = GD.Load<Texture2D>("res://resources/characters/" + CharacterName + ".png");
			};
			MambaText.Text = Message;
			if (options == null)
			{
				Button Helicopter = (Button)MambaButtonDuplicated.Duplicate();
				_savedCallback = () =>
				{
					Animator.Play("leave");
					ThreadSleep.SleepAsync(leaveTime).Then(e =>
					{
						Blocker.ResumeTime();
						resolve(-1);
						return null;
					});
					Helicopter.QueueFree();
				};
				Helicopter.Pressed += _savedCallback;
				MambaBox.AddChild(Helicopter);
			}
			else
			{
				savedOptions = options;
				for (int i = 0; i < options.Length; i++)
				{
					int savedI = i;
					TextureButton currentOption = (TextureButton)ExampleOption.Duplicate();
					currentOption.GetNode<Label>("Text").Text = options[i];
					currentOption.Name = "Option" + savedI.ToString();
					currentOption.Position -= new Vector2(0, 69 * i);
					_savedCallback = () =>
					{
						Animator.Play("leave");
						ThreadSleep.SleepAsync(leaveTime).Then(e =>
						{
							Blocker.ResumeTime();
							resolve(savedI);
							return null;
						});
						for (int j = 0; j < i; j++)
						{
							MambaBox.RemoveChild(MambaBox.GetNode("Option" + j.ToString()));
							GD.Print("removed option " + j.ToString());
						}
					};
					currentOption.Pressed += _savedCallback;
					MambaBox.AddChild(currentOption);
				}
			}
		});
	}
}
