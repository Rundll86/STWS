using Godot;
using System;

public partial class Message : Sprite2D
{
    PositionLock target;
    static Sprite2D MsgBox;
    static RichTextLabel TextRenderer;
    static bool isShowing = false;
    static ColorRect[] lastShowingOptions;
    static string lastId;
    static int arg;
    public override void _Ready()
    {
        MsgBox = this;
        TextRenderer = GetNode<RichTextLabel>("TextRenderer");
        target = GetNode<PositionLock>("../CameraMain");
        HideMessage();
    }
    public override void _Process(double delta)
    {
        Position = target.Position + new Vector2(0, 230);
    }
    public override void _Input(InputEvent @event)
    {
        if (isShowing)
        {
            for (int i = 0; i < lastShowingOptions.Length; i++)
            {
                if (
                    @event is InputEventMouseButton mouseButton &&
                    mouseButton.ButtonIndex == MouseButton.Left &&
                    mouseButton.Pressed &&
                    lastShowingOptions[i].GetRect().HasPoint(GetLocalMousePosition())
                )
                {
                    HideMessage();
                    ProcessSelectedOption(i, arg);
                }
            }
        }
    }
    public static void ShowMessage(string id, string message, string[] optionsOld = null, int arg = 0)
    {
        Blocker.Block();
        string[] options = optionsOld ?? (new string[] { "确定" });
        TextRenderer.Text = message;
        MsgBox.Visible = true;
        lastShowingOptions = new ColorRect[options.Length];
        for (int i = 0; i < options.Length; i++)
        {
            ColorRect currentRect = new()
            {
                Size = new Vector2(300, 40),
                Color = new Color(0, 0, 0),
                Position = new Vector2(-500, -150) - new Vector2(0, 50 * i)
            };
            Label currentLabel = new()
            {
                Text = options[i],
                Position = new Vector2(20, 0),
                Size = currentRect.Size - new Vector2(20, 0),
                Name = "OptionLabel" + i.ToString(),
                VerticalAlignment = VerticalAlignment.Center,
            };
            currentLabel.AddThemeFontSizeOverride("font_size", 20);
            currentRect.AddChild(currentLabel);
            MsgBox.AddChild(currentRect);
            lastShowingOptions[i] = currentRect;
        }
        isShowing = true;
        lastId = id;
        Message.arg = arg;
        GD.Print("Message:" + message);
    }
    public static void HideMessage()
    {
        Blocker.Unblock();
        GD.Print("HideMessage:" + TextRenderer.Text);
        TextRenderer.Text = "";
        MsgBox.Visible = false;
        Godot.Collections.Array<Node> children = MsgBox.GetChildren();
        children.RemoveAt(0);
        for (int i = 0; i < children.Count; i++)
        {
            children[i].QueueFree();
        }
        isShowing = false;
        lastShowingOptions = Array.Empty<ColorRect>();
    }
    public static void ProcessSelectedOption(int selected, int arg)
    {
        GD.Print("Selected:" + selected + ",arg:" + arg);
        switch (lastId)
        {
            case "order":
                if (selected == 0)
                {
                    GD.Print("开始点菜");
                    Ordering.Open(arg);
                }
                else if (selected == 1)
                {
                    GD.Print("取消点菜");
                }
                break;
            case "eat":
                if (arg == 1)
                {
                    if (selected == 0)
                    {
                        GD.Print("暂停用餐");
                        UserData.PauseEating();
                        return;
                    }
                    FoodObject lastAte = UserData.HadFoods[selected - 1];
                    Common.FoodEatingAnimation.Texture = lastAte.avatar;
                    if ("ABCD".Contains(Common.LastChairType))
                    {
                        Common.FoodEatingAnimationPlayer.Play("down-to-up");
                    }
                    else
                    {
                        Common.FoodEatingAnimationPlayer.Play("up-to-down");
                    }
                    // Common.FoodEatingAnimationPlayer.Play("eat5-fly");
                    Common.PlayerSprite.eating = true;
                    TimeCalc.StepMultipiler = 6;
                    ThreadSleep.SleepAsync(5000).Then((_) =>
                    {
                        TimeCalc.StepMultipiler = 1;
                        Common.PlayerSprite.eating = false;
                        UserData.HadFoods = Common.RemoveItemFromArray(UserData.HadFoods, lastAte);
                        GD.Print("吃掉「" + lastAte.name + "」");
                        UserData.ShowEatingMessageBox();
                        return null;
                    });
                }
                else
                {
                    if (selected == 0)
                    {
                        if (UserData.RealHadFoodsLength == 0)
                        {
                            ShowMessage("warning", "你还没有购买任何食物！");
                            return;
                        }
                        GD.Print("开始用餐");
                        UserData.ShowEatingMessageBox();
                        Common.PlayerSprite.Position = Common.LastChair.GlobalPosition;
                        Common.PlayerSprite.state = EntityController.State.SIT_ON_DOWN;
                        if ("ABCD".Contains(Common.LastChairType))
                        {
                            Common.PlayerSprite.Position = new Vector2(
                                Common.PlayerSprite.Position.X,
                                Common.LastChair.GetParent().GetParent<Node2D>().Position.Y + 10
                            );
                            Common.PlayerSprite.texture.Position += Common.EatingPositionOffset;
                            Common.PlayerSprite.state = EntityController.State.SIT_ON_UP;
                            Common.LastChair.GetParent<Node2D>().GetNode<AnimatedSprite2D>("Texture").ZIndex = 1;
                        }
                    }
                    else if (selected == 1)
                    {
                        GD.Print("取消用餐");
                    }
                }
                break;
        }
    }
}