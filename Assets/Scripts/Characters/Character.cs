using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] public string name;
    [SerializeField] public string gender;
    public Dictionary<Character, double> friendlinessValues = new Dictionary<Character, double>();

    private int taskPoints;

    public enum Item
    {
        None,
        Water,
        Paper
    }

    public Item heldItem = Item.None;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddTaskPoints(int points)
    {
        taskPoints += points;
        taskPoints = Math.Clamp(taskPoints, 0, 100);
    }

    public void UpdateFriendliness(Character character, double friendliness)
    {
        // not necessary for Player class

        if (friendlinessValues.ContainsKey(character))
        {
            friendlinessValues[character] = Math.Clamp(friendliness, 0, 100);

        } else
        {
            friendlinessValues.Add(character, Math.Clamp(friendliness, 0, 100));
        }
    }

    public double FriendlinessTo(Character target)
    {
        if (friendlinessValues.ContainsKey(target))
        {
            return friendlinessValues[target];
        }
        else
        {
            return 0.0; // this path should not happen
        }
    }

    public bool isHandEmpty()
    {
        if (heldItem == Item.None)
        {
            return true;
        }
        Debug.Log("Hands are full");
        return false;
    }
}
