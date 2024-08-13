using Godot;
using System;

public partial class Message : Sprite2D
{
    PositionLock target;
    static Sprite2D MsgBox;
    static RichTextLabel TextRenderer;
    static bool isShowing = false;
    static int lastShowingCount;
    static string lastId;
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
            for (int i = 0; i < lastShowingCount; i++)
            {
                if (
                    @event is InputEventMouseButton mouseButton &&
                    mouseButton.ButtonIndex == MouseButton.Left &&
                    mouseButton.Pressed &&
                    GetNode<ColorRect>("Option" + i.ToString()).GetRect().HasPoint(GetLocalMousePosition())
                )
                {
                    HideMessage();
                    ProcessSelectedOption(i);
                }
            }
        }
    }
    public static void ShowMessage(string id, string message, string[] optionsOld = null)
    {
        string[] options = optionsOld ?? (new string[] { "确定" });
        TextRenderer.Text = message;
        MsgBox.Visible = true;
        for (int i = 0; i < options.Length; i++)
        {
            ColorRect currentRect = new()
            {
                Size = new Vector2(300, 40),
                Color = new Color(0, 0, 0),
                Position = new Vector2(-500, -150) - new Vector2(0, 50 * i),
                Name = "Option" + i.ToString()
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
        }
        Blocker.Block();
        isShowing = true;
        lastId = id;
        lastShowingCount = options?.Length ?? 0;
        GD.Print("Message:" + message);
    }
    public static void HideMessage()
    {
        GD.Print("HideMessage:" + TextRenderer.Text);
        TextRenderer.Text = "";
        MsgBox.Visible = false;
        Blocker.Unblock();
        Godot.Collections.Array<Node> children = MsgBox.GetChildren();
        children.RemoveAt(0);
        for (int i = 0; i < children.Count; i++)
        {
            children[i].QueueFree();
        }
        isShowing = false;
        lastShowingCount = 0;
    }
    public static void ProcessSelectedOption(int selected)
    {
        if (lastId == "order")
        {
            if (selected == 0)
            {
                GD.Print("开始点菜");
                Common.WorldController.GetNode<Node2D>("OrderUI").Visible = true;
                Blocker.Block();
            }
            else if (selected == 1)
            {
                GD.Print("取消点菜");
            }
        }
        else
        {
            GD.Print("LastID:" + lastId);
            GD.Print("ProcessSelectedOption:" + selected);
        }
    }
}
