using Godot;
using System;

public partial class UserData : Node
{
    public static Food[] Ordered;
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
    public static string BoughtFoodNames
    {
        get
        {
            if (RealOrderedLength == 0)
            {
                return "未购买任何食物";
            }
            string result = "";
            foreach (Food f in Ordered)
            {
                if (f != null)
                    result += f.name + ",";
            }
            return result;
        }
    }
    public override void _Ready()
    {
        Ordered = new Food[10];
        Money = 30;
    }
}
