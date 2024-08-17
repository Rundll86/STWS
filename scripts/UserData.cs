using Godot;
using System;
using System.Linq;

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
        for (int i = 0; i < options.Length - 1; i++)
        {
            options[i + 1] = "整「" + HadFoods[i].name + "」";
        }
        options = options.Distinct().ToArray();
        options[0] = "有点噎着了...稍微缓一缓...";
        Mamba.WhatCanISayAsync("", "不错，味儿正", options).Then(selected =>
        {
            if (selected == 0)
            {
                GD.Print("暂停用餐");
                PauseEating();
                return null;
            }
            FoodObject lastAte = HadFoods[selected - 1];
            GD.Print("lastAte:" + lastAte.name);
            Common.FoodEatingAnimation.Texture = lastAte.avatar;
            if ("ABCD".Contains(Common.LastChairType))
            {
                Common.FoodEatingAnimationPlayer.Play("show-up");
            }
            else
            {
                Common.FoodEatingAnimationPlayer.Play("show-down");
            }
            Sprite2D progressBlockBack = (Sprite2D)Common.ExampleProgressBlock.Duplicate();
            progressBlockBack.Texture = lastAte.avatar;
            GD.Print("cloned progress block");
            int i;
            for (i = 0; i < GetFoodCount(lastAte) - 1; i++)
            {
                Sprite2D currentProgressBlock = (Sprite2D)progressBlockBack.Duplicate();
                currentProgressBlock.Position += new Vector2(40 * i, 0);
                currentProgressBlock.Name = "ProgressBlock" + i;
                Common.FoodEatingAnimation.AddChild(currentProgressBlock);
                GD.Print("Added:" + currentProgressBlock.Name);
            }
            i--;
            Common.PlayerSprite.eating = true;
            TimeCalc.TimeAccelerates(4, 5000 * GetFoodCount(lastAte));
            TimeCalc.MethodCirculator(() =>
            {
                HadFoods = Common.RemoveItemFromArray(HadFoods, lastAte, false);
                if (i >= 0)
                {
                    Sprite2D currentProgressBlock = Common.FoodEatingAnimation.GetNode<Sprite2D>("ProgressBlock" + i);
                    currentProgressBlock.GetNode<AnimationPlayer>("Animator").Play("ate");
                    ThreadSleep.SleepAsync(500).Then(e =>
                    {
                        currentProgressBlock.QueueFree();
                        return null;
                    });
                    i--;
                }
                GD.Print("Ate:" + lastAte.name);
            }, 5000, GetFoodCount(lastAte), true);
            ThreadSleep.SleepAsync(5000 * GetFoodCount(lastAte) + 100).Then((_) =>
            {
                if ("ABCD".Contains(Common.LastChairType))
                {
                    Common.FoodEatingAnimationPlayer.Play("flyout-up");
                }
                else
                {
                    Common.FoodEatingAnimationPlayer.Play("flyout-down");
                }
                Common.PlayerSprite.eating = false;
                ShowEatingMessageBox();
                return null;
            });
            return null;
        });
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
    public static int GetFoodCount(FoodObject food)
    {
        int result = 0;
        foreach (FoodObject f in HadFoods)
        {
            if (f == food)
                result++;
        }
        return result;
    }
    public override void _Ready()
    {
        Ordered = new FoodObject[10];
        HadFoods = new FoodObject[0];
        Money = 30;
    }
}
