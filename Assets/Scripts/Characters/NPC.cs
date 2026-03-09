using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPC : Character
{
    private Queue<Action> actionQueue = new Queue<Action>();
    private Action currentAction = null;
    
    [SerializeField] public NPCPersonality personality;

    private bool givenWater = false; // whether or not this NPC has been given water by the player

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
        if (friendlinessValues.Count == 0)
        {
            Debug.Log($"Cannot add random action");
            return;
        }

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

    public bool ReceiveWater()
    {
        if (!givenWater)
        {
            double friendlinessToPlayer = FriendlinessTo(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>());

            Debug.Log($"{gameObject.name} received water from player");
            givenWater = true;
            
            IncreaseFriendliness(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(), GiveCupOfWater.friendlinessIncrease);
            Debug.Log($"{gameObject.name} friendliness to player increased from {friendlinessToPlayer} to {FriendlinessTo(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>())}");

            return true;
        }

        return false;
    }
}
