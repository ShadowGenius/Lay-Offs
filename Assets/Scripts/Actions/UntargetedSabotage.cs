using UnityEngine;

public class UntargetedSabotage : Action
{
    public static int friendlinessPenalty = 0;
    public UntargetedSabotage(Character character) : base(character, "Untargeted Sabotage", ActionStatus.NotStarted)
    {

    }
}

public class BreakPrinter : UntargetedSabotage
{
    public static int friendlinessPenalty = -25;
    public BreakPrinter(Character character) : base(character)
    {
        title = "Break the Printer";
    }
}