using UnityEngine;

public class UntargetedSabotage : Action
{
    public UntargetedSabotage(Character character) : base(character, "Untargeted Sabotage", ActionStatus.NotStarted)
    {

    }
}

public class BreakPrinter : UntargetedSabotage
{
    public BreakPrinter(Character character) : base(character)
    {
        title = "Break the Printer";
    }
}