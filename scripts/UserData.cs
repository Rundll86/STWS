using Godot;
using System;

public partial class UserData : Node
{
    public static FoodObject[] Ordered;
    public static FoodObject[] HadFoods;
    public static long Money;
    public static long TotalPrice
    {
        get
        {
            long total = 0;
            foreach (FoodObject f in Ordered)
            {
                if (f != null)
                    total += f.price;
            }
            return total;
        }
    }
    public static int RealOrderedLength
    {
        get
        {
            int result = 0;
            foreach (FoodObject f in Ordered)
            {
                if (f != null)
                    result++;
            }
            return result;
        }
    }
    public static int RealHadFoodsLength
    {
        get
        {
            int result = 0;
            foreach (FoodObject f in HadFoods)
            {
                if (f != null)
                    result++;
            }
            return result;
        }
    }
    public static string BoughtFoodNames
    {
        get
        {
            if (RealOrderedLength == 0)
            {
                return "未选择任何食物";
            }
            string result = "";
            foreach (FoodObject f in Ordered)
            {
                if (f != null)
                    result += f.name + ",";
            }
            return result[..^1];
        }
    }
    public static string HadFoodNames
    {
        get
        {
            if (RealHadFoodsLength == 0)
            {
                return "未购买任何食物";
            }
            string result = "";
            foreach (FoodObject f in HadFoods)
            {
                if (f != null)
                    result += f.name + ",";
            }
            return result[..^1];
        }
    }
    public static void ShowEatingMessageBox()
    {
        if (RealHadFoodsLength == 0)
        {
            PauseEating();
            return;
        }
        string[] options = new string[RealHadFoodsLength + 1];
        options[0] = "暂停用餐";
        for (int i = 0; i < options.Length - 1; i++)
        {
            options[i + 1] = "吃掉「" + HadFoods[i].name + "」";
        }
        Message.ShowMessage("eat", "享受美食！", options, 1);
    }
    public static void PauseEating()
    {
        Common.PlayerSprite.state = EntityController.State.STAND;
        if ("ABCD".Contains(Common.LastChairType))
        {
            Common.PlayerSprite.texture.Position -= Common.EatingPositionOffset;
            Common.PlayerSprite.Position -= new Vector2(0, 100);
            Common.LastChair.GetParent<Node2D>().GetNode<AnimatedSprite2D>("Texture").ZIndex = 0;
        }
        else
        {
            Common.PlayerSprite.Position += new Vector2(0, 100);
        }
    }
    public override void _Ready()
    {
        Ordered = new FoodObject[10];
        HadFoods = new FoodObject[0];
        Money = 30;
    }
}
