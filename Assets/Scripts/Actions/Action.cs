using NUnit.Framework;
using UnityEngine;

public class Action
{
    public Character character;
    public string title;
    protected ActionStatus status = ActionStatus.NotStarted;

    public static List<Task> possibleTasks = new List<Task>()
    {
        new Printing(null, 5),
        new Delivery(null, 5)
    };

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
