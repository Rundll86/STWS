using Godot;
using System;
using Newtonsoft.Json;
using System.Linq;
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
    public bool haveAvatar { get; set; }
    public int classify { get; set; }
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
    public static FoodObject[][] FoodListByClassify;
    public static void Init()
    {
        FoodList = JsonConvert.DeserializeObject<FoodObject[]>(Json.Stringify(GD.Load<Json>("res://level/foods-generated.json").Data));
        int maxClassify = 0;
        for (int i = 0; i < FoodList.Length; i++)
        {
            int classify = FoodList[i].classify;
            if (classify > maxClassify)
            {
                maxClassify = classify;
            }
        }
        FoodListByClassify = new FoodObject[maxClassify + 1][];
        for (int i = 0; i < FoodList.Length; i++)
        {
            int classify = FoodList[i].classify;
            if (FoodListByClassify[classify] == null)
            {
                FoodListByClassify[classify] = new FoodObject[] { FoodList[i] };
            }
            else
            {
                Array.Resize(ref FoodListByClassify[classify], FoodListByClassify[classify].Length + 1);
                FoodListByClassify[classify][FoodListByClassify[classify].Length - 1] = FoodList[i];
            }
        }
        FoodListByClassify = Common.RemoveItemFromArray(FoodListByClassify, null);
        Common.PrintJson(FoodListByClassify.Length);
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
