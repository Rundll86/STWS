using Godot;
using System;
using System.Linq;
using Newtonsoft.Json;
public partial class Common : Node2D
{
    static public EntityController PlayerSprite;
    static public Font Font;
    static public Texture2D FoodIcon;
    static public Node2D WorldController;
    static public SceneTree SceneTree;
    static public Label MoneyLabel;
    static public int[] RandomFoodLengthList;
    static public Food[][] RandomFoodList;
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
    static public T[] PickRandomElements<T>(T[] array, int count)
    {
        Random random = new Random();
        return array.OrderBy(x => random.Next()).Take(count).ToArray();
    }
    static public void PrintJson(object obj)
    {
        GD.Print(JsonConvert.SerializeObject(obj, Formatting.Indented));
    }
    public override void _Ready()
    {
        PlayerSprite = GetNode<EntityController>("Map/Player");
        Font = GD.Load<Font>("res://resources/syht.ttf");
        FoodIcon = GD.Load<Texture2D>("res://resources/food-icon.png");
        WorldController = this;
        SceneTree = WorldController.GetTree();
        MoneyLabel = GetNode<Label>("Map/Player/MoneyLabel");
        RandomFoodLengthList = new int[] { 8, 6, 6, 6, 4, 4 };
        RandomFoodList = new Food[RandomFoodLengthList.Length][];
        for (int i = 0; i < RandomFoodLengthList.Length; i++)
        {
            RandomFoodList[i] = PickRandomElements(Food.FoodList, RandomFoodLengthList[i]);
        }
    }
    public override void _Process(double delta)
    {
        MoneyLabel.Text = "余额：" + UserData.Money.ToString() + "-" + UserData.TotalPrice + " G";
    }
}
