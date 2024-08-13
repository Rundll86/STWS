using Godot;
using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
public partial class Common : Node2D
{
    static public EntityController PlayerSprite;
    static public Font Font;
    static public Texture2D FoodIcon;
    static public Node2D WorldController;
    static public SceneTree SceneTree;
    static public Label MoneyLabel;
    static public Label BoughtFoodLabel;
    static public int[] RandomFoodLengthList;
    static public Food[][] RandomFoodList;
    static public Vector2 MoneyLabelOldPosition;
    static public ProgressBar Timer;
    static public StyleBoxFlat TimerStyleBox;
    static float ColorR;
    static float ColorG;
    static float ColorB;
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
    static public float DoubleToFloat(double d)
    {
        return (float)d;
    }
    static public T[] RemoveItemFromArray<T>(T[] arr, T itemToRemove)
    {
        List<T> list = new List<T>(arr);
        list.Remove(itemToRemove);
        return list.ToArray();
    }
    public override void _Ready()
    {
        PlayerSprite = GetNode<EntityController>("Map/Player");
        Font = GD.Load<Font>("res://resources/syht.ttf");
        FoodIcon = GD.Load<Texture2D>("res://resources/food-icon.png");
        WorldController = this;
        SceneTree = WorldController.GetTree();
        MoneyLabel = GetNode<Label>("Map/Player/MoneyLabel");
        MoneyLabelOldPosition = MoneyLabel.Position;
        BoughtFoodLabel = MoneyLabel.GetNode<Label>("BoughtFoodList");
        RandomFoodLengthList = new int[] { 8, 6, 6, 6, 4, 4 };
        RandomFoodList = new Food[RandomFoodLengthList.Length][];
        for (int i = 0; i < RandomFoodLengthList.Length; i++)
        {
            RandomFoodList[i] = PickRandomElements(Food.FoodList, RandomFoodLengthList[i]);
        }
        Timer = GetNode<ProgressBar>("CameraMain/Timer");
    }
    public override void _Process(double delta)
    {
        MoneyLabel.Text = "余额：" + UserData.Money.ToString() + "-" + UserData.TotalPrice + " G";
        BoughtFoodLabel.Text = "将要购买：" + UserData.BoughtFoodNames + "\n已购买食物：" + UserData.HadFoodNames;
        BoughtFoodLabel.Position = new Vector2(
            ((BoughtFoodLabel.Size - new Vector2(112, 0)) / -2).X,
            BoughtFoodLabel.Position.Y
        );
        Timer.Value = 600 - TimeCalc.RunningTime;
    }
}
