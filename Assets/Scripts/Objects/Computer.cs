using System.Collections;
using UnityEngine;

public class Computer : ObjectInteraction
{
    public Character owner;
    private bool isBroken = false;
    private bool isRunning = false;
    private int usingTime = 5;
    [SerializeField] private AudioClip[] usingSFX;

    [SerializeField] GameObject smokePrefab;
    [SerializeField] Transform smokePoint;
    private GameObject smokeInstance;
    [SerializeField] private AudioClip explodeSFX = null;

    public override void OnPlayerUse()
    {
        ComputerUse computerTask = player.playerActions.Find(action => action is ComputerUse && action.IsNotFinished()) as ComputerUse;
        Use(player, computerTask);
    }

    public override void OnNPCUse(NPC npc)
    {
        Use(npc);
    }

    private void Use(Character user, ComputerUse computerTask = null)
    {
        Debug.Log($"{user.name} using computer {gameObject.transform.parent.name}");

        if (user != owner)
        {
            // don't let this happen
            return;
        }

        if (canBeUsed())
        {
            SFXManager.instance.PlayRandomSFX(usingSFX, transform);
            StartCoroutine(runUsing());
            if (computerTask != null)
            {
                computerTask.MakeProgress();
            }
            Debug.Log($"{user.name} made progress on computer use");
        }

        if (isBroken)
        {
            Debug.Log($"{user.name} fixing computer");
            StartCoroutine(runUsing());
            isBroken = false;

            if (smokeInstance != null)
            {
                Destroy(smokeInstance);
                smokeInstance = null;
            }
            owner.IncreaseFriendliness(user, FixComputer.friendlinessIncrease);
        }
    }

    public override void OnPlayerSabotage()
    {
        Sabotage(player);
    }

    public override void OnNPCSabotage(NPC npc)
    {
        Sabotage(npc);
    }

    private void Sabotage(Character traitor)
    {
        Debug.Log($"{traitor.name} trying to sabotage {owner}'s computer");

        if (traitor == owner)
        {
            // don't let this happen
            return;
        }

        if (!isBroken)
        {
            SFXManager.instance.PlaySFX(explodeSFX, transform);
            isBroken = true;

            if (smokePrefab != null && smokePoint != null && smokeInstance == null)
            {
                smokeInstance = Instantiate(smokePrefab, smokePoint.position, Quaternion.identity);
            }
            owner.IncreaseFriendliness(traitor, BreakComputer.friendlinessPenalty);
        }
        else
        {
            isBroken = false;

            if (smokeInstance != null)
            {
                Destroy(smokeInstance);
                smokeInstance = null;
            }
            owner.IncreaseFriendliness(traitor, FixComputer.friendlinessIncrease);
        }
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