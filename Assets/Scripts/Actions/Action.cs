using NUnit.Framework;
using UnityEngine;

public class Action
{
    public Character character;
    public string title;
    protected ActionStatus status = ActionStatus.NotStarted;

    public static Task GenerateRandomTask(Character character)
    {
        int randomTaskType = Random.Range(0, 3);

        switch (randomTaskType)
        {
            case 0:
                return new Printing(character);

            case 1:
                return new Delivery(character);

            case 2:
                return new ComputerUse(character);

        }

        return null; // shouldn't happen
    }

    public static Help GenerateRandomHelp(Character character, Character target)
    {
        int randomTaskType = Random.Range(0, 3);

        switch (randomTaskType)
        {
            case 0:
                return new GiveCupOfWater(character, target);

            case 1:
                return new PickUpItems(character, target);

            case 2:
                return new FixComputer(character, target);

        }

        return null; // shouldn't happen
    }

    public static TargetedSabotage GenerateRandomTargetedSabotage(Character character, Character target)
    {
        int randomTaskType = Random.Range(0, 3);

        switch (randomTaskType)
        {
            case 0:
                return new HideFiles(character, target);

            case 1:
                return new BreakComputer(character, target);

            case 2:
                return new BumpIntoCharacter(character, target);

        }

        return null; // shouldn't happen
    }

    public static UntargetedSabotage GenerateRandomUntargetedSabotage(Character character)
    {
        int randomTaskType = Random.Range(0, 1);

        switch (randomTaskType)
        {
            case 0:
                return new BreakPrinter(character);

        }

        return null; // shouldn't happen
    }

    public enum ActionStatus
    {
        NotStarted,
        InProgress,
        Completed,
        Failed // if for example there is a time limit and the player runs out of time, only applicable to certain types of actions
    }

    public Action(Character character, string title, ActionStatus status)
    {
        this.character = character;
        this.title = title;
        this.status = status;
    }

    public virtual void BeginAction()
    {
        if (status != ActionStatus.NotStarted)
        {
            return;
        }

        status = ActionStatus.InProgress;
    }
}
