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
    public override void _Ready()
    {
        Ordered = new Food[10];
        Money = 30;
    }
}
