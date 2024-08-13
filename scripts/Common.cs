using Godot;
using System;

public partial class Common : Node2D
{
    static public CharacterBody2D PlayerSprite;
    static public Font Font;
    static public Texture2D FoodIcon;
    static public Node2D WorldController;
    static public SceneTree SceneTree;
    static public T[] CreateArrayBy3Items<T>(T v1, T v2, T v3)
    {
        return new T[] { v1, v2, v3 };
    }
    static public T[] CreateArrayBy2Items<T>(T v1, T v2)
    {
        return new T[] { v1, v2 };
    }
    static public bool IsPointInRect(Vector2 point, Rect2 rect)
    {
        return point.X >= rect.Position.X && point.X <= rect.Position.X + rect.Size.X && point.Y >= rect.Position.Y && point.Y <= rect.Position.Y + rect.Size.Y;
    }
    public override void _Ready()
    {
        PlayerSprite = GetNode<EntityController>("Map/Player");
        Font = GD.Load<Font>("res://resources/syht.ttf");
        FoodIcon = GD.Load<Texture2D>("res://resources/food-icon.png");
        WorldController = this;
        SceneTree = WorldController.GetTree();
    }
}
