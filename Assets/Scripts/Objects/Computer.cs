using UnityEngine;

public class Computer : ObjectInteraction
{
    public Character owner;
    public override void OnPlayerUse()
    {
        if (player != owner)
        {
            Debug.Log($"Player trying to use someone else's computer at desk {gameObject.transform.parent.name}"); // don't let this happen
            
        } else
        {
            Debug.Log($"Player using their computer {gameObject.transform.parent.name}");
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
    }

    public override void OnNPCUse(NPC npc)
    {
        // ...
    }

    public override void OnNPCSabotage(NPC npc)
    {
        // ...
    }
}
