using UnityEngine;

public class Help : TargetedAction
{
    public Help(Character character, Character target) : base(character, target)
    {

    }
}

public class GiveCupOfWater : Help
{
    public GiveCupOfWater(Character character, Character target) : base(character, target)
    {
        this.title = $"Give a Cup of Water to {target.name}";
    }
}

public class PickUpItems : Help
{
    public PickUpItems(Character character, Character target) : base(character, target)
    {
        this.title = $"Pick Up {target.name}'s Dropped Items";
    }
}

public class FixComputer : Help
{
    public FixComputer(Character character, Character target) : base(character, target)
    {
        this.title = $"Fix {target.name}'s Computer";
    }
}