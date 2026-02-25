using Unity.VisualScripting;
using UnityEngine;

public class TargetedAction : Action
{
    public Character target;

    public TargetedAction(Character character, Character target) : base(character, "Targeted Action", ActionStatus.NotStarted)
    {
        this.target = target;
    }
}
