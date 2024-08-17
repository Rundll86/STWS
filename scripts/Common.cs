using Godot;
using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
public partial class Common : Node2D
{
    static public EntityController PlayerSprite;
    static public Font SYHT;
    static public Font Unifont;
    static public Texture2D FoodIcon;
    static public Node2D WorldController;
    static public SceneTree SceneTree;
    static public Label MoneyLabel;
    static public Label BoughtFoodLabel;
    static public int[] RandomFoodLengthList;
    static public FoodObject[][] RandomFoodList;
    static public Vector2 MoneyLabelOldPosition;
    static public ProgressBar Timer;
    static public AnimationPlayer TimerAnimator;
    static public Node2D ExampleTableGroup;
    static public Sprite2D ExampleFoodCard;
    static public TextureRect ExampleFoodCardShow;
    static public Sprite2D ExampleProgressBlock;
    static public Node2D TableController;
    static public Vector2 TablePositionOffset;
    static public Vector2 TableArrangement;
    static public TileMap Map;
    static public StaticBody2D LastChair;
    static public string LastChairType;
    static public Vector2 EatingPositionOffset;
    static public Sprite2D FoodEatingAnimation;
    static public AnimationPlayer FoodEatingAnimationPlayer;
    static public string[] ShopTypes;
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
        return array.OrderBy(x => Guid.NewGuid()).Take(count).ToArray();
    }
    static public void PrintJson(object obj)
    {
        GD.Print(JsonConvert.SerializeObject(obj, Formatting.Indented));
    }
    static public float DoubleToFloat(double d)
    {
        return (float)d;
    }
    static public T[] RemoveItemFromArray<T>(T[] arr, T itemToRemove, bool all = true)
    {
        List<T> list = new(arr);
        do
        {
            list.Remove(itemToRemove);
        }
        while (all && list.Contains(itemToRemove));
        return list.ToArray();
    }
    static public T[] RemoveItemFromArray<T>(T[] arr, int index)
    {
        List<T> list = new(arr);
        list.RemoveAt(index);
        return list.ToArray();
    }
    static public T[] AppendItemToArray<T>(T[] arr, T itemToAdd)
    {
        List<T> list = new(arr)
        {
            itemToAdd
        };
        return list.ToArray();
    }
    static public bool IsNodeClicked(Sprite2D node, InputEventMouseButton mouseButtonEvent)
    {
        return node.GetRect().HasPoint(node.ToLocal(mouseButtonEvent.Position)) &&
            mouseButtonEvent.Pressed &&
            mouseButtonEvent.ButtonIndex == MouseButton.Left;
    }
    public override void _Ready()
    {
        Food.Init();
        ShopTypes = new string[] { "糕点", "自选", "自选", "自选", "面食", "饭食" };
        ExampleTableGroup = GetNode<Node2D>("/root/WorldController/InitilizatorC/ExampleTableGroup");
        ExampleFoodCard = GetNode<Sprite2D>("/root/WorldController/InitilizatorC/ExampleFoodCard");
        ExampleFoodCard.GetParent().RemoveChild(ExampleFoodCard);
        ExampleFoodCardShow = GetNode<TextureRect>("/root/WorldController/InitilizatorC/ExampleFoodCardShow");
        ExampleFoodCardShow.GetParent().RemoveChild(ExampleFoodCardShow);
        ExampleProgressBlock = GetNode<Sprite2D>("/root/WorldController/InitilizatorC/ExampleProgressBlock");
        ExampleProgressBlock.GetParent().RemoveChild(ExampleProgressBlock);
        PlayerSprite = GetNode<EntityController>("Map/Player");
        SYHT = GD.Load<Font>("res://resources/syht.ttf");
        Unifont = GD.Load<Font>("res://resources/unifont.ttf");
        FoodIcon = GD.Load<Texture2D>("res://resources/foods/food.png");
        WorldController = this;
        SceneTree = WorldController.GetTree();
        MoneyLabel = GetNode<Label>("CameraMain/MoneyLabel");
        BoughtFoodLabel = MoneyLabel.GetNode<Label>("BoughtFoodList");
        RandomFoodLengthList = new int[] { 8, 6, 6, 6, 4, 4 };
        RandomFoodList = new FoodObject[0][];
        for (int i = 0; i < RandomFoodLengthList.Length; i++)
        {
            GD.Print("LoadedWindow:" + i.ToString());
            RandomFoodList = AppendItemToArray(
                RandomFoodList,
                PickRandomElements(
                    Food.FoodListByClassify[(new int[] { 1, 2, 3 }).Contains(i) ? 6 : i],
                    RandomFoodLengthList[i]
                )
            );
            StaticBody2D currentWindow = GetNode<StaticBody2D>("Map/Window" + (i + 1).ToString());
            int currentLength = RandomFoodList[i].Length;
            for (int j = 0; j < currentLength; j++)
            {
                TextureRect currentFoodCardShow = (TextureRect)ExampleFoodCardShow.Duplicate();
                currentFoodCardShow.Name = "FoodCardShow" + j;
                currentFoodCardShow.Texture = RandomFoodList[i][j].avatar;
                currentFoodCardShow.Position = new Vector2(
                    j < currentLength / 2 ? -102 - 39 * j : 67 + 39 * (j - currentLength / 2),
                    -200
                );
                currentWindow.AddChild(currentFoodCardShow);
            }
        }
        Timer = GetNode<ProgressBar>("CameraMain/Timer");
        TimerAnimator = Timer.GetNode<AnimationPlayer>("Animator");
        TableController = GetNode<Node2D>("Map/TableController");
        Map = GetNode<TileMap>("Map");
        TablePositionOffset = new Vector2(700, 400);
        EatingPositionOffset = new Vector2(0, -50);
        TableArrangement = new Vector2(6, 14);
        int clonedCount = 0;
        Node2D cloned;
        for (int i = 0; i < TableArrangement.X; i++)
        {
            for (int j = 0; j < TableArrangement.Y; j++)
            {
                clonedCount++;
                cloned = (Node2D)ExampleTableGroup.Duplicate();
                cloned.Name = "Table" + clonedCount.ToString().PadLeft(3, '0');
                cloned.Position = new Vector2(
                    TablePositionOffset.X * i - TablePositionOffset.X * (TableArrangement.X / 2),
                    TablePositionOffset.Y * j - TablePositionOffset.Y * (TableArrangement.Y / 2)
                );
                Map.AddChild(cloned);
            }
        }
        ExampleTableGroup.QueueFree();
        FoodEatingAnimation = PlayerSprite.GetNode<Sprite2D>("Food/FoodEating");
        FoodEatingAnimationPlayer = FoodEatingAnimation.GetNode<AnimationPlayer>("Animator");
    }
    public override void _Process(double delta)
    {
        MoneyLabel.Text = "余额：" + UserData.Money.ToString() + "-" + UserData.TotalPrice.ToString() + " G";
        BoughtFoodLabel.Text = "将要购买：" + UserData.BoughtFoodNames + "\n已购买：" + UserData.HadFoodNames;
        Timer.Value = 600 - TimeCalc.RunningTime;
        TimerAnimator.Seek(TimeCalc.RunningTime);
        UserData.HadFoods = RemoveItemFromArray(UserData.HadFoods, null);
    }
}
