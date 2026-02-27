using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : Character
{
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private TextMeshProUGUI taskListText = null;

    public List<Task> playerActions = new List<Task>();

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

    // Update is called once per frame
    void Update()
    {
        taskListText.text = "";
        bool allComplete = true;
        for (int i = 0; i < playerActions.Count; i++)
        {
            taskListText.text += $"{i + 1}. {playerActions[i].title} ({playerActions[i].PercentComplete()}%)\n";
            if (playerActions[i].PercentComplete() != 100)
            {
                allComplete = false;
            }
        }
        if (allComplete) {
            AssignRandomTasks();
        }
    }
}
