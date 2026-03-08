using System.Collections;
using UnityEngine;

public class Printer : ObjectInteraction
{
    private bool isBroken = false;
    private bool isRunning = false;
    private int printingTime = 5;
    private int fixingTime = 5;
    [SerializeField] private AudioClip printingSFX = null;
    [SerializeField] private AudioClip explodeSFX = null;

    // communal printer, no one owns it (since it is related to untargeted sabotage)
    public override void OnPlayerUse()
    {

        Debug.Log("Printer doing printer things");

        Printing printingTask = player.playerActions.Find(action => action is Printing && action.IsNotFinished()) as Printing;

        if (canBeUsed() && player.isHandEmpty())
        {
            StartCoroutine(runPrinting(player, printingTask));
            //player.heldItem = Character.Item.Paper;
            //if (printingTask != null)
            //{
            //    printingTask.MakeProgress();

                Debug.Log($"Player made progress on printing ({printingTask.PercentComplete()}% complete)");
            //}
        }
        else if (isBroken)
        {
            StartCoroutine(runFixing());
        }
    }

    public override void OnNPCUse(NPC npc)
    {
        Debug.Log("NPC using printer");
        if (canBeUsed())
        {
            StartCoroutine(runPrinting(npc));
            Debug.Log("NPC used printer");
        }
    }

    public override void OnPlayerSabotage()
    {
        Debug.Log("Player sabotaging printer");
        SFXManager.instance.PlaySFX(explodeSFX, transform);
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

    private IEnumerator runPrinting(Character ch, Printing printingTask = null)
    {
        SFXManager.instance.PlaySFX(printingSFX, transform);
        isRunning = true;
        yield return new WaitForSeconds(printingTime);
        ch.heldItem = Character.Item.Paper;
        if (printingTask != null)
        {
            printingTask.MakeProgress();
        }
        isRunning = false;
    }

    private IEnumerator runFixing()
    {
        yield return new WaitForSeconds(fixingTime);
        isBroken = false;
    }
}
