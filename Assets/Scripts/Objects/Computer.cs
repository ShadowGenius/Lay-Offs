using System.Collections;
using UnityEngine;

public class Computer : ObjectInteraction
{
    public Character owner;
    private bool isBroken = false;
    private bool isRunning = false;
    private int usingTime = 5;

    public override void OnPlayerUse()
    {
        if (player != owner)
        {
            Debug.Log($"Player trying to use someone else's computer at desk {gameObject.transform.parent.name}"); // don't let this happen
            
        } else
        {
            Debug.Log($"Player using their computer {gameObject.transform.parent.name}");

            ComputerUse computerTask = player.playerActions.Find(action => action is ComputerUse && action.IsNotFinished()) as ComputerUse;

            if (computerTask != null && canBeUsed())
            {
                StartCoroutine(runUsing());
                computerTask.MakeProgress();
                Debug.Log($"Player made progress on computer use ({computerTask.PercentComplete()}% complete)");
            }
        }

        if (isBroken)
        {
            Debug.Log("Player fixing computer");
            StartCoroutine(runUsing());
            isBroken = false;
        }
    }

    public override void OnPlayerSabotage()
    {
        Debug.Log("Player trying to sabotage a computer");

        if (player == owner)
        {
            Debug.Log("Player trying to sabotage own computer"); // don't let this happen
            return;
        }
        isBroken = true;
    }

    public override void OnNPCUse(NPC npc)
    {
        
        Debug.Log($"Player using their computer {gameObject.transform.parent.name}");

        if (canBeUsed())
        {
            StartCoroutine(runUsing());
            //computerTask.MakeProgress();
            Debug.Log($"NPC made progress on computer use");
        }

        if (isBroken)
        {
            Debug.Log("NPC fixing computer");
            StartCoroutine(runUsing());
            isBroken = false;
        }
    }

    public override void OnNPCSabotage(NPC npc)
    {
        Debug.Log("NPC trying to sabotage a computer");
        isBroken = true;
    }

    public bool canBeUsed()
    {
        if (isRunning)
        {
            Debug.Log("Computer is running");
        }
        else if (isBroken)
        {
            Debug.Log("Computer is broken");
        }
        return !isRunning && !isBroken;
    }

    private IEnumerator runUsing()
    {
        isRunning = true;
        yield return new WaitForSeconds(usingTime);
        isRunning = false;
    }
}
