using UnityEngine;

public class Help : TargetedAction
{
    public static int friendlinessIncrease = 0;
    public Help(Character character, Character target) : base(character, target)
    {

    }
}

public class GiveCupOfWater : Help
{
    public static int friendlinessIncrease = 10;

    public GiveCupOfWater(Character character, Character target) : base(character, target)
    {
        this.title = $"Give a Cup of Water to {target.name}";
    }
}

public class PickUpItems : Help
{
    public static int friendlinessIncrease = 5;

    public PickUpItems(Character character, Character target) : base(character, target)
    {
        this.title = $"Pick Up {target.name}'s Dropped Items";
    }
}

public class FixComputer : Help
{
    public static int friendlinessIncrease = 15;

    public FixComputer(Character character, Character target) : base(character, target)
    {
        this.title = $"Fix {target.name}'s Computer";
    }
}