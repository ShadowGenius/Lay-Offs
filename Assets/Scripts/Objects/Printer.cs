using UnityEngine;

public class Printer : ObjectInteraction
{
    // communal printer, no one owns it (since it is related to untargeted sabotage)
    public override void OnPlayerUse()
    {

        Debug.Log("Printer doing printer things");

        Printing printingTask = player.playerActions.Find(action => action is Printing && action.IsNotFinished()) as Printing;
        
        if (printingTask != null)
        {
            printingTask.MakeProgress();

            Debug.Log($"Player made progress on printing ({printingTask.PercentComplete()}% complete)");
        }
    }

    public override void OnNPCUse(NPC npc)
    {
        Debug.Log("NPC using printer");
    }

    public override void OnPlayerSabotage()
    {
        Debug.Log("Player sabotaging printer");
    }

    public override void OnNPCSabotage(NPC npc)
    {
        Debug.Log("NPC sabotaging printer");
    }
}
