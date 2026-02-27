using System.Collections;
using UnityEngine;

public class Printer : ObjectInteraction
{
    private bool isBroken = false;
    private bool isRunning = false;
    private int printingTime = 5;

    // communal printer, no one owns it (since it is related to untargeted sabotage)
    public override void OnPlayerUse()
    {

        Debug.Log("Printer doing printer things");

        Printing printingTask = player.playerActions.Find(action => action is Printing && action.IsNotFinished()) as Printing;

        if (canBeUsed() && player.isHandEmpty())
        {
            StartCoroutine(runPrinting());
            player.heldItem = Character.Item.Paper;
            if (printingTask != null)
            {
                printingTask.MakeProgress();

                Debug.Log($"Player made progress on printing ({printingTask.PercentComplete()}% complete)");
            }
        }
    }

    public override void OnNPCUse(NPC npc)
    {
        Debug.Log("NPC using printer");
        if (canBeUsed())
        {
            StartCoroutine(runPrinting());
            Debug.Log("NPC used printer");
        }
    }

    public override void OnPlayerSabotage()
    {
        Debug.Log("Player sabotaging printer");
        isBroken = true;
    }

    public override void OnNPCSabotage(NPC npc)
    {
        Debug.Log("NPC sabotaging printer");
        isBroken = true;
    }

    public bool canBeUsed()
    {
        if (isRunning)
        {
            Debug.Log("Printer is running");
        }
        else if (isBroken)
        {
            Debug.Log("Printer is broken");
        }
        return !isRunning && !isBroken;
    }

    private IEnumerator runPrinting()
    {
        isRunning = true;
        yield return new WaitForSeconds(printingTime);
        isRunning = false;
    }
}
