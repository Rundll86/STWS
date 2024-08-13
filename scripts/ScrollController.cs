using Godot;
using System;

public partial class ScrollController : PositionLock
{
    ScrollController InstanceMe;
    [Export]
    float ScrollSpeed;
    public void SetInstanceMe(ScrollController scrollController)
    {
        InstanceMe = scrollController;
    }
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.ButtonIndex == MouseButton.WheelUp)
            {
                GD.Print("ScrollDown:" + this);
                InstanceMe.Position += new Vector2(0, InstanceMe.ScrollSpeed);
            }
            else if (mouseEvent.ButtonIndex == MouseButton.WheelDown)
            {
                GD.Print("ScrollUp:" + this);
                InstanceMe.Position += new Vector2(0, -InstanceMe.ScrollSpeed);
            }
        }
    }
}
