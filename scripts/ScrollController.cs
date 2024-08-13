using Godot;
using System;

public partial class ScrollController : PositionLock
{
    [Export]
    Vector2 ScrollSpeed;
    [Export]
    Vector2 MaxOffset;
    [Export]
    public Vector2 MinOffset;
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.ButtonIndex == MouseButton.WheelUp)
            {
                offset += ScrollSpeed;
            }
            else if (mouseEvent.ButtonIndex == MouseButton.WheelDown)
            {
                offset -= ScrollSpeed;
            }
        }
        if (offset.Y > MaxOffset.Y)
        {
            offset.Y = MaxOffset.Y;
        }
        if (offset.Y < MinOffset.Y)
        {
            offset.Y = MinOffset.Y;
        }
        if (offset.X > MaxOffset.X)
        {
            offset.X = MaxOffset.X;
        }
        if (offset.X < MinOffset.X)
        {
            offset.X = MinOffset.X;
        }
    }
}
