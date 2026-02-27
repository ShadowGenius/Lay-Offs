using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private PlayerMovement movement;

    public List<Action> playerActions = new List<Action>();

    private void AssignRandomTasks(int count = 3)
    {
        for (int i = 0; i < count; i++)
        {
            playerActions.Add(Action.GenerateRandomTask(this));
        }
    }
    void Start()
    {
        AssignRandomTasks();

        for (int i = 0; i < playerActions.Count; i++)
        {
            Debug.Log($"Task {i + 1}: {playerActions[i].title}");
        }
    }
}
