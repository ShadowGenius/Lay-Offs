using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : Character
{
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private TextMeshProUGUI taskListText = null;
    [SerializeField] private TextMeshProUGUI heldItemText = null;

    [SerializeField] public float npcInteractionDistance = 2.2f;

    public List<Task> playerActions = new List<Task>();

    private List<NPC> npcs = new List<NPC>();

    private void AssignRandomTasks(int count = 3)
    {
        for (int i = 0; i < count; i++)
        {
            playerActions.Add(Action.GenerateRandomTask(this));
        }
    }

    private void GetNPCS()
    {
        foreach (GameObject gameObj in GameObject.FindGameObjectsWithTag("NPC"))
        {
            npcs.Add(gameObj.GetComponent<NPC>());
        }
    }
    void Start()
    {
        // AssignRandomTasks();
        playerActions.Add(new Printing(this));
        playerActions.Add(new Delivery(this));
        playerActions.Add(new ComputerUse(this));

        for (int i = 0; i < playerActions.Count; i++)
        {
            Debug.Log($"Task {i + 1}: {playerActions[i].title}");
        }

        GetNPCS();
    }

    // Update is called once per frame
    void Update()
    {
        taskListText.text = "TASKS\n";
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

        heldItemText.text = "Current held item: " + heldItem.ToString();

        NPCInteraction();
    }

    NPC NPCInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            NPC nearestNPC = FindNearestNPC();

            if (nearestNPC != null && Vector2.Distance(transform.position, nearestNPC.transform.position) <= npcInteractionDistance)
            {
                GiveWater(nearestNPC);

                Debug.Log($"Interacted with {nearestNPC.gameObject.name}");
                return nearestNPC;

            } else
            {
                Debug.Log($"No NPC within interaction distance (nearest NPC is {Vector2.Distance(transform.position, nearestNPC.transform.position)} units away)");
            }
        }

        return null;
    }

    bool GiveWater(NPC npc)
    {
        if (heldItem == Item.Water)
        {
            if (npc.ReceiveWater())
            {
                heldItem = Item.None;
                return true;

            } else
            {
                Debug.Log($"{npc.gameObject.name} is not thirsty right now (already given water)");
            }
        }

        Debug.Log("No water to give");
        return false;
    }

    NPC FindNearestNPC()
    {
        NPC nearestNPC = null;
        float nearestDistanceSq = Mathf.Infinity;

        foreach (NPC npc in npcs)
        {
            float distanceSq = Mathf.Pow(Vector2.Distance(transform.position, npc.transform.position), 2);

            if (distanceSq < nearestDistanceSq)
            {
                nearestDistanceSq = distanceSq;
                nearestNPC = npc;
            }
        }

        return nearestNPC;
        heldItemText.text = "Current held item:\n" + heldItem.ToString();
    }
}
