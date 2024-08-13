using Godot;
using System;

public partial class UserData : Node
{
    public static Food[] Ordered;
    public static Food[] HadFoods;
    public static long Money;
    public static long TotalPrice
    {
        get
        {
            long total = 0;
            foreach (Food f in Ordered)
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
            foreach (Food f in Ordered)
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
            foreach (Food f in HadFoods)
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
            foreach (Food f in Ordered)
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
            foreach (Food f in HadFoods)
            {
                if (f != null)
                    result += f.name + ",";
            }
            return result[..^1];
        }
    }
    public override void _Ready()
    {
        Ordered = new Food[10];
        HadFoods = new Food[100];
        Money = 30;
    }
}
