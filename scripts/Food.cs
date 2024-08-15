using Godot;
using System;
using Newtonsoft.Json;
public class FoodObject
{
    public string name { get; set; }
    public int value { get; set; }
    public int bdz { get; set; }
    public int vc { get; set; }
    public int vb { get; set; }
    public int cellulose { get; set; }
    public int price { get; set; }
    public int count { get; set; }
    public Taste taste { get; set; }
    public bool haveAvatar = false;
    Texture2D _avatarTemp;
    public Texture2D avatar
    {
        get
        {
            if (_avatarTemp == null)
            {
                if (haveAvatar)
                {
                    _avatarTemp = GD.Load<Texture2D>("res://resources/foods/" + name + ".png");
                }
                else
                {
                    _avatarTemp = Common.FoodIcon;
                };
            }
            return _avatarTemp;
        }
    }
    public string tasteContent
    {
        get
        {
            return "酸：" + taste.酸 + " 甜：" + taste.甜 + " 苦：" + taste.苦 + " 辣：" + taste.辣 + " 咸：" + taste.咸 + " 麻：" + taste.麻 + " 油：" + taste.油 + " 清淡：" + taste.清淡;
        }
    }
}
public class Taste
{
    public int 酸 { get; set; }
    public int 甜 { get; set; }
    public int 苦 { get; set; }
    public int 辣 { get; set; }
    public int 咸 { get; set; }
    public int 麻 { get; set; }
    public int 油 { get; set; }
    public int 清淡 { get; set; }
}
public class Food
{
    public static FoodObject[] FoodList;
    public static void Init()
    {
        FoodList = JsonConvert.DeserializeObject<FoodObject[]>(Json.Stringify(GD.Load<Json>("res://level/foods-generated.json").Data));
    }
    public static FoodObject FindFoodByName(string name)
    {
        foreach (FoodObject food in FoodList)
        {
            if (food.name == name)
            {
                return food;
            }
        }
        return null;
    }
}
