using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPC : Character
{
    private Queue<Action> actionQueue = new Queue<Action>();
    private Action currentAction = null;
    
    [SerializeField] public NPCPersonality personality;

    // movement script is a different component

    void Start()
    {
        personality = NPCPersonality.generic_model; // everyone is a generic colleague for now
    }

    public void AddAction(Action action)
    {
        actionQueue.Enqueue(action);
    }

    public void AddRandomAction()
    {
        // ...
        int index = (int)Random.Range(0f, friendlinessValues.Count);
        Character target = friendlinessValues.ElementAt(index).Key;
        double friendlinessToTarget = friendlinessValues[target];

        Action action = personality.NextAction(this, target);

        AddAction(action);
    }

    public void StartNewAction()
    {
        if (currentAction == null)
        {
            currentAction = actionQueue.Dequeue();
            currentAction.BeginAction();
        }
    }
}
