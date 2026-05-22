using System.Collections;
using UnityEngine;

public class Printer : ObjectInteraction
{
    [SerializeField] GameObject paperPrefab;
    [SerializeField] Transform holdPoint;

    [SerializeField] GameObject smokePrefab;
    [SerializeField] Transform smokePoint;

    private GameObject smokeInstance;

    private bool isBroken = false;
    private bool isRunning = false;
    private int printingTime = 5;
    private int fixingTime = 5;
    [SerializeField] private AudioClip printingSFX = null;
    [SerializeField] private AudioClip explodeSFX = null;

    // communal printer, no one owns it (since it is related to untargeted sabotage)
    public override void OnPlayerUse()
    {
        Printing printingTask = player.playerActions.Find(action => action is Printing && action.IsNotFinished()) as Printing;
        Use(player, printingTask);
    }

    public override void OnNPCUse(NPC npc)
    {
        Use(npc);
    }

    public void Use(Character user, Printing printingTask = null)
    {
        Debug.Log("Printer doing printer things");

        if (canBeUsed() && user.isHandEmpty())
        {
            StartCoroutine(runPrinting(user, printingTask));
            Debug.Log($"{user.name} used printer");
        }
        //else if (isBroken)
        //{
        //    StartCoroutine(runFixing());
        //}
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
        Debug.Log($"{traitor.name} sabotaging printer");

        if (!isBroken)
        {
            SFXManager.instance.PlaySFX(explodeSFX, transform);
            isBroken = true;

            if (smokePrefab != null && smokePoint != null && smokeInstance == null)
            {
                smokeInstance = Instantiate(smokePrefab, smokePoint);
                smokeInstance.transform.localPosition = Vector3.zero;
                smokeInstance.transform.localScale = new Vector3(4, 4, 1);
            }

            spellFieldInstance = Instantiate(spellFieldPrefab, transform);
            FriendlinessEvent detector = spellFieldInstance.GetComponent<FriendlinessEvent>();
            detector.ApplyToAll(traitor, BreakPrinter.friendlinessPenalty);
            spellFieldInstance = null;
        }
        else
        {
            StartCoroutine(runFixing());
        }
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

        //GameObject paper = Instantiate(paperPrefab, holdPoint);
        //paper.transform.localPosition = new Vector3(0, 1, 0);
        //paper.transform.localScale = new Vector3((float)1.2, (float)1.2, 1);
        //SFXManager.instance.PlaySFX(printingSFX, transform);
        isRunning = true;
        SFXManager.instance.PlaySFX(printingSFX, transform);
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

        if (smokeInstance != null)
        {
            Destroy(smokeInstance);
            smokeInstance = null;
        }
    }
}
