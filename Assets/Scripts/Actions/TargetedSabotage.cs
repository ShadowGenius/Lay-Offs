using UnityEngine;

public class TargetedSabotage : TargetedAction
{
    public static int friendlinessPenalty = 0;
    public TargetedSabotage(Character character, Character target) : base(character, target)
    {

    }
}

public class BumpIntoCharacter : TargetedSabotage
{
    public static int friendlinessPenalty = -5;
    public BumpIntoCharacter(Character character, Character target) : base(character, target)
    {
        title = $"Bump into {target.name}";
    }
}

public class BreakComputer : TargetedSabotage
{
    public static int friendlinessPenalty = -20;
    public BreakComputer(Character character, Character target) : base(character, target)
    {
        title = $"Break {target.name}'s Computer";
    }
}

public class HideFiles : TargetedSabotage
{
    public static int friendlinessPenalty = -10;
    public HideFiles(Character character, Character target) : base(character, target)
    {
        title = $"Hide {target.name}'s Files at {(target.gender.StartsWith("m") ? "His" : target.gender.StartsWith("f") ? "Her" : "Their")} Table";
    }
}
