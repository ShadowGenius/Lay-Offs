using UnityEngine;
using System.Collections.Generic;

public class NPCPersonality
{
    public static NPCPersonality generic_model = new NPCPersonality("Generic Model", 50, 10, 40, 80, -0.6f, 20, 0.6f);
    public static NPCPersonality evil_mastermind = new NPCPersonality("Evil Mastermind", 
                                                               basicTaskChance: 25, 
                                                               untargetedSabotageChance: 25, 
                                                               targetedActionChance: 50, 
                                                               baseTargetedSabotageChance: 100, 
                                                               targetedSabotageFriendlinessMult: -0.4f, 
                                                               baseHelpChance: 0, 
                                                               helpFriendlinessMult: 0.4f);
    public static NPCPersonality saint = new NPCPersonality("The Saint",
                                                     basicTaskChance: 70,
                                                     untargetedSabotageChance: 0,
                                                     targetedActionChance: 30,
                                                     baseTargetedSabotageChance: 60,
                                                     targetedSabotageFriendlinessMult: -0.6f,
                                                     baseHelpChance: 40,
                                                     helpFriendlinessMult: 0.6f);
    public static NPCPersonality retaliator = new NPCPersonality("The Retaliator",
                                                     basicTaskChance: 40,
                                                     untargetedSabotageChance: 10,
                                                     targetedActionChance: 50,
                                                     baseTargetedSabotageChance: 100,
                                                     targetedSabotageFriendlinessMult: -0.8f,
                                                     baseHelpChance: 0,
                                                     helpFriendlinessMult: 0.8f);
    public static NPCPersonality madman = new NPCPersonality("The Madman",
                                                     basicTaskChance: 20,
                                                     untargetedSabotageChance: 30,
                                                     targetedActionChance: 50,
                                                     baseTargetedSabotageChance: 60,
                                                     targetedSabotageFriendlinessMult: -0.2f,
                                                     baseHelpChance: 40,
                                                     helpFriendlinessMult: 0.2f);

    // this class should be used as an attribute for a broader NPC class
    public string name = "Generic Model";
    
    private int basicTaskChance = 50;
    private int untargetedSabotageChance = 10;
    private int targetedActionChance = 40;

    private int baseTargetedSabotageChance = 80;
    private double targetedSabotageFriendlinessMult = -0.6f;

    private int baseHelpChance = 20;
    private double helpFriendlinessMult = 0.6f;

    public NPCPersonality(string name, int basicTaskChance, int untargetedSabotageChance, int targetedActionChance,
                          int baseTargetedSabotageChance, double targetedSabotageFriendlinessMult, int baseHelpChance,
                          double helpFriendlinessMult)
    {
        this.name = name;
        this.basicTaskChance = basicTaskChance;
        this.untargetedSabotageChance = untargetedSabotageChance;
        this.targetedActionChance = targetedActionChance;
        this.baseTargetedSabotageChance = baseTargetedSabotageChance;
        this.targetedSabotageFriendlinessMult = targetedSabotageFriendlinessMult;
        this.baseHelpChance = baseHelpChance;
        this.helpFriendlinessMult = helpFriendlinessMult;
    }

    double TargetedSabotageChance(double friendlinessToTarget)
    {
        // the broader NPC class should have an array of friendliness values for each other NPC + player, which this uses
        return baseTargetedSabotageChance + (friendlinessToTarget * targetedSabotageFriendlinessMult);
    }

    double HelpChance(double friendlinessToTarget)
    {
        return baseHelpChance + (friendlinessToTarget * helpFriendlinessMult);
    }

    public Action NextAction(Character character, Character target) 
    {
        // strings are just placeholders for the actual actions
        // return the Action object instead

        double friendlinessToTarget = character.FriendlinessTo(target);

        double actionRoll = Random.Range(0f, 100f);
        string actionString = "";
        Action action = null;

        if (actionRoll < basicTaskChance)
        {
            actionString = "basic task";

        } else if (actionRoll < basicTaskChance + untargetedSabotageChance)
        {
            actionString = "untargeted sabotage";

        } else if (actionRoll < basicTaskChance + untargetedSabotageChance + targetedActionChance) // should just be 100 
        {
            double targetedActionRoll = Random.Range(0f, 100f);
            string targetedAction = "";

            if (targetedActionRoll < TargetedSabotageChance(friendlinessToTarget))
            {
                targetedAction = "targeted sabotage";
            }
            else 
            {
                targetedAction = "help";
            }

            actionString = targetedAction;
        }

        if (actionString == "basic task")
        {
            // select a random basic task from a list of basic tasks

        } else if (actionString == "untargeted sabotage")
        {
            // select random untargeted sabotage

        } else if (actionString == "targeted sabotage")
        {
            // ...

        } else if (actionString == "help")
        {
            // ...

        }

        return action;
    }

}
