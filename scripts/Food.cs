using Godot;
using System;

public partial class Food : Node
{
    public static Food[] FoodList = new Food[]{
            new Food(){
            name="番茄年糕盖饭",
            value=9,
            protein=0,
            vc=3,
            vb=1,
            cellulose=1,
            price=10,
            count=1,
            tasteSuan=1,
            tasteTian=2,
            tasteKu=0,
            tasteLa=0,
            tasteXian=0,
            tasteMa=0,
            tasteYou=0,
            tasteQingDan=0,
            },
            new Food(){
            name="什锦炸货盖饭",
            value=7,
            protein=0,
            vc=0,
            vb=1,
            cellulose=3,
            price=13,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=0,
            tasteXian=1,
            tasteMa=2,
            tasteYou=0,
            tasteQingDan=0,
            },
            new Food(){
            name="红烧鸡块盖饭",
            value=7,
            protein=4,
            vc=0,
            vb=0,
            cellulose=0,
            price=15,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=0,
            tasteXian=3,
            tasteMa=0,
            tasteYou=0,
            tasteQingDan=0,
            },
            new Food(){
            name="红烧排骨盖饭",
            value=6,
            protein=2,
            vc=0,
            vb=0,
            cellulose=1,
            price=10,
            count=1,
            tasteSuan=0,
            tasteTian=1,
            tasteKu=0,
            tasteLa=0,
            tasteXian=2,
            tasteMa=0,
            tasteYou=0,
            tasteQingDan=0,
            },
            new Food(){
            name="麻辣豆芽鸭血盖饭",
            value=8,
            protein=1,
            vc=1,
            vb=0,
            cellulose=2,
            price=10,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=3,
            tasteXian=2,
            tasteMa=0,
            tasteYou=0,
            tasteQingDan=0,
            },
            new Food(){
            name="光面",
            value=5,
            protein=0,
            vc=0,
            vb=0,
            cellulose=0,
            price=3,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=0,
            tasteXian=0,
            tasteMa=0,
            tasteYou=0,
            tasteQingDan=1,
            },
            new Food(){
            name="青菜面",
            value=6,
            protein=0,
            vc=2,
            vb=0,
            cellulose=1,
            price=5,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=0,
            tasteXian=0,
            tasteMa=0,
            tasteYou=0,
            tasteQingDan=1,
            },
            new Food(){
            name="鸡腿面",
            value=7,
            protein=1,
            vc=1,
            vb=0,
            cellulose=1,
            price=10,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=0,
            tasteXian=1,
            tasteMa=0,
            tasteYou=0,
            tasteQingDan=0,
            },
            new Food(){
            name="牛肉面",
            value=7,
            protein=2,
            vc=0,
            vb=0,
            cellulose=0,
            price=10,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=0,
            tasteXian=1,
            tasteMa=0,
            tasteYou=0,
            tasteQingDan=0,
            },
            new Food(){
            name="雪菜肉丝面",
            value=7,
            protein=1,
            vc=1,
            vb=0,
            cellulose=2,
            price=13,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=1,
            tasteXian=2,
            tasteMa=0,
            tasteYou=0,
            tasteQingDan=0,
            },
            new Food(){
            name="牛肉粉丝面",
            value=6,
            protein=1,
            vc=0,
            vb=2,
            cellulose=0,
            price=9,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=2,
            tasteXian=2,
            tasteMa=0,
            tasteYou=0,
            tasteQingDan=0,
            },
            new Food(){
            name="手抓饼",
            value=7,
            protein=3,
            vc=0,
            vb=0,
            cellulose=0,
            price=10,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=0,
            tasteXian=2,
            tasteMa=0,
            tasteYou=3,
            tasteQingDan=0,
            },
            new Food(){
            name="汉堡",
            value=5,
            protein=1,
            vc=1,
            vb=0,
            cellulose=0,
            price=8,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=0,
            tasteXian=1,
            tasteMa=0,
            tasteYou=1,
            tasteQingDan=0,
            },
            new Food(){
            name="鸡肉卷",
            value=5,
            protein=1,
            vc=1,
            vb=0,
            cellulose=0,
            price=5,
            count=1,
            tasteSuan=0,
            tasteTian=1,
            tasteKu=0,
            tasteLa=0,
            tasteXian=1,
            tasteMa=0,
            tasteYou=0,
            tasteQingDan=0,
            },
            new Food(){
            name="鸡翅饭",
            value=4,
            protein=1,
            vc=0,
            vb=1,
            cellulose=0,
            price=10,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=1,
            tasteXian=2,
            tasteMa=0,
            tasteYou=0,
            tasteQingDan=0,
            },
            new Food(){
            name="肉松卷",
            value=6,
            protein=1,
            vc=0,
            vb=0,
            cellulose=0,
            price=6,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=0,
            tasteXian=1,
            tasteMa=0,
            tasteYou=0,
            tasteQingDan=0,
            },
            new Food(){
            name="豆沙面包",
            value=6,
            protein=0,
            vc=0,
            vb=1,
            cellulose=2,
            price=6,
            count=1,
            tasteSuan=0,
            tasteTian=2,
            tasteKu=0,
            tasteLa=0,
            tasteXian=0,
            tasteMa=0,
            tasteYou=0,
            tasteQingDan=0,
            },
            new Food(){
            name="巧克力卷",
            value=6,
            protein=0,
            vc=0,
            vb=0,
            cellulose=0,
            price=6,
            count=1,
            tasteSuan=0,
            tasteTian=4,
            tasteKu=0,
            tasteLa=0,
            tasteXian=0,
            tasteMa=0,
            tasteYou=0,
            tasteQingDan=0,
            },
            new Food(){
            name="肉松塔克",
            value=6,
            protein=1,
            vc=0,
            vb=1,
            cellulose=0,
            price=6,
            count=1,
            tasteSuan=0,
            tasteTian=1,
            tasteKu=0,
            tasteLa=0,
            tasteXian=1,
            tasteMa=0,
            tasteYou=0,
            tasteQingDan=0,
            },
            new Food(){
            name="蟹味饭团",
            value=7,
            protein=2,
            vc=1,
            vb=1,
            cellulose=0,
            price=10,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=1,
            tasteXian=2,
            tasteMa=0,
            tasteYou=0,
            tasteQingDan=0,
            },
            new Food(){
            name="三明治",
            value=7,
            protein=1,
            vc=0,
            vb=2,
            cellulose=0,
            price=8,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=0,
            tasteXian=1,
            tasteMa=0,
            tasteYou=0,
            tasteQingDan=0,
            },
            new Food(){
            name="蓝莓果酱面包",
            value=4,
            protein=0,
            vc=1,
            vb=0,
            cellulose=0,
            price=5,
            count=1,
            tasteSuan=0,
            tasteTian=2,
            tasteKu=0,
            tasteLa=0,
            tasteXian=0,
            tasteMa=0,
            tasteYou=0,
            tasteQingDan=0,
            },
            new Food(){
            name="年糕",
            value=3,
            protein=0,
            vc=0,
            vb=0,
            cellulose=1,
            price=2,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=0,
            tasteXian=0,
            tasteMa=0,
            tasteYou=2,
            tasteQingDan=0,
            },
            new Food(){
            name="烤冷面",
            value=1,
            protein=1,
            vc=0,
            vb=0,
            cellulose=0,
            price=2,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=0,
            tasteXian=0,
            tasteMa=0,
            tasteYou=3,
            tasteQingDan=0,
            },
            new Food(){
            name="炸土豆串",
            value=4,
            protein=0,
            vc=0,
            vb=0,
            cellulose=1,
            price=4,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=0,
            tasteXian=1,
            tasteMa=0,
            tasteYou=3,
            tasteQingDan=0,
            },
            new Food(){
            name="臭豆腐",
            value=1,
            protein=0,
            vc=0,
            vb=1,
            cellulose=1,
            price=4,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=0,
            tasteXian=1,
            tasteMa=0,
            tasteYou=2,
            tasteQingDan=0,
            },
            new Food(){
            name="炸鸡腿",
            value=2,
            protein=2,
            vc=0,
            vb=0,
            cellulose=0,
            price=4,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=0,
            tasteXian=2,
            tasteMa=0,
            tasteYou=2,
            tasteQingDan=0,
            },
            new Food(){
            name="蛋挞",
            value=1,
            protein=1,
            vc=0,
            vb=0,
            cellulose=1,
            price=3,
            count=1,
            tasteSuan=0,
            tasteTian=3,
            tasteKu=0,
            tasteLa=0,
            tasteXian=0,
            tasteMa=0,
            tasteYou=0,
            tasteQingDan=0,
            },
            new Food(){
            name="鸡块",
            value=1,
            protein=1,
            vc=0,
            vb=0,
            cellulose=0,
            price=5,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=0,
            tasteXian=2,
            tasteMa=0,
            tasteYou=1,
            tasteQingDan=0,
            },
            new Food(){
            name="油爆茄子",
            value=1,
            protein=0,
            vc=1,
            vb=0,
            cellulose=2,
            price=5,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=0,
            tasteXian=1,
            tasteMa=0,
            tasteYou=1,
            tasteQingDan=0,
            },
            new Food(){
            name="土豆牛肉",
            value=3,
            protein=2,
            vc=0,
            vb=0,
            cellulose=0,
            price=6,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=0,
            tasteXian=2,
            tasteMa=0,
            tasteYou=0,
            tasteQingDan=0,
            },
            new Food(){
            name="炒油花菜",
            value=1,
            protein=0,
            vc=1,
            vb=1,
            cellulose=1,
            price=5,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=0,
            tasteXian=1,
            tasteMa=0,
            tasteYou=1,
            tasteQingDan=0,
            },
            new Food(){
            name="红烧鸡块",
            value=2,
            protein=2,
            vc=0,
            vb=0,
            cellulose=0,
            price=5,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=0,
            tasteXian=2,
            tasteMa=0,
            tasteYou=0,
            tasteQingDan=0,
            },
            new Food(){
            name="油面筋",
            value=3,
            protein=2,
            vc=0,
            vb=0,
            cellulose=1,
            price=6,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=0,
            tasteXian=2,
            tasteMa=0,
            tasteYou=2,
            tasteQingDan=0,
            },
            new Food(){
            name="浇油辣鱼",
            value=2,
            protein=3,
            vc=0,
            vb=0,
            cellulose=0,
            price=7,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=2,
            tasteXian=0,
            tasteMa=0,
            tasteYou=1,
            tasteQingDan=0,
            },
            new Food(){
            name="清炒海带",
            value=1,
            protein=1,
            vc=0,
            vb=1,
            cellulose=2,
            price=5,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=0,
            tasteXian=1,
            tasteMa=0,
            tasteYou=0,
            tasteQingDan=0,
            },
            new Food(){
            name="胡萝卜炒木耳",
            value=1,
            protein=0,
            vc=0,
            vb=3,
            cellulose=1,
            price=6,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=0,
            tasteXian=0,
            tasteMa=0,
            tasteYou=0,
            tasteQingDan=1,
            },
            new Food(){
            name="白菜卷肉",
            value=2,
            protein=1,
            vc=1,
            vb=0,
            cellulose=1,
            price=6,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=0,
            tasteXian=1,
            tasteMa=0,
            tasteYou=0,
            tasteQingDan=0,
            },
            new Food(){
            name="时令小炒",
            value=1,
            protein=0,
            vc=2,
            vb=0,
            cellulose=1,
            price=4,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=0,
            tasteXian=0,
            tasteMa=0,
            tasteYou=0,
            tasteQingDan=1,
            },
            new Food(){
            name="米饭",
            value=4,
            protein=0,
            vc=0,
            vb=0,
            cellulose=0,
            price=0,
            count=1,
            tasteSuan=0,
            tasteTian=0,
            tasteKu=0,
            tasteLa=0,
            tasteXian=0,
            tasteMa=0,
            tasteYou=0,
            tasteQingDan=1
            }
        };
    public string name;
    public int value;
    public int protein;
    public int vc;
    public int vb;
    public int cellulose;
    public int price;
    public int count;
    public int tasteSuan;
    public int tasteTian;
    public int tasteKu;
    public int tasteLa;
    public int tasteXian;
    public int tasteMa;
    public int tasteYou;
    public int tasteQingDan;
    public static Food FindFoodByName(string name)
    {
        foreach (Food food in FoodList)
        {
            if (food.name == name)
            {
                return food;
            }
        }
        return null;
    }
    public static string GetFoodSubName(string name)
    {
        Food current = FindFoodByName(name);
        if (current == null)
        {
            return null;
        }
        string result = "";
        if (current.tasteSuan > 0)
        {
            result += "酸" + current.tasteSuan + ",";
        }
        if (current.tasteTian > 0)
        {
            result += "甜" + current.tasteTian + ",";
        }
        if (current.tasteKu > 0)
        {
            result += "苦" + current.tasteKu + ",";
        }
        if (current.tasteLa > 0)
        {
            result += "辣" + current.tasteLa + ",";
        }
        if (current.tasteXian > 0)
        {
            result += "咸" + current.tasteXian + ",";
        }
        if (current.tasteMa > 0)
        {
            result += "麻" + current.tasteMa + ",";
        }
        if (current.tasteYou > 0)
        {
            result += "油" + current.tasteYou + ",";
        }
        if (current.tasteQingDan > 0)
        {

            result += "清淡" + current.tasteQingDan + ",";
        }
        return result;
    }
    public static string GetFoodContent(string name)
    {
        Food current = FindFoodByName(name);
        if (current == null)
        {
            return null;
        }
        string result = "";
        result += "饱食度: " + current.value + ",\n";
        result += "蛋白质: " + current.protein + ",";
        result += "维生素C: " + current.vc + ",\n";
        result += "维生素B: " + current.vb + ",";
        result += "纤维素: " + current.cellulose;
        return result;
    }
}