using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class NPCPersonalityTest : MonoBehaviour
{
    private NPCPersonality[] personalities = {NPCPersonality.generic_model, NPCPersonality.evil_mastermind,
    NPCPersonality.saint, NPCPersonality.retaliator, NPCPersonality.madman};

    public int testsPerPersonality = 50;
    public int friendliness = 50;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log($"Friendliness: {friendliness}");

        for (int j = 0; j < personalities.Length; j++)
        {
            List<Action> actions = new List<Action>();

            Debug.Log($"{personalities[j].name}");
            for (int i = 1; i <= testsPerPersonality; ++i)
            {
                //Action action = 

                //actions.Add(action);
                //Debug.Log($"Test {i}/{testsPerPersonality}: {action}");
            }

            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
