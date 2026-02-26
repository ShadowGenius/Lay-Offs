using UnityEngine;

public class TargetedSabotage : TargetedAction
{
    public TargetedSabotage(Character character, Character target) : base(character, target)
    {

    }
}

public class BumpIntoCharacter : TargetedSabotage
{
    public BumpIntoCharacter(Character character, Character target) : base(character, target)
    {
        title = $"Bump into {target.name}";
    }
}

public class BreakComputer : TargetedSabotage
{
    public BreakComputer(Character character, Character target) : base(character, target)
    {
        title = $"Break {target.name}'s Computer";
    }
}

public class HideFiles : TargetedSabotage
{
    public HideFiles(Character character, Character target) : base(character, target)
    {
        title = $"Hide {target.name}'s Files at {(target.gender.StartsWith("m") ? "His" : target.gender.StartsWith("f") ? "Her" : "Their")} Table";
    }
}
