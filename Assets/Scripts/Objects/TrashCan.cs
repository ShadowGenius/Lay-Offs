using UnityEngine;

public class TrashCan : ObjectInteraction
{
    // communal printer, no one owns it (since it is related to untargeted sabotage)
    public override void OnPlayerUse()
    {

        Debug.Log("Player using trash can");

        if (!player.isHandEmpty())
        {
            player.heldItem = Character.Item.None;

            Debug.Log($"Player threw something into the trash");
        }
    }

    public override void OnNPCUse(NPC npc)
    {
        Debug.Log("NPC using trash can");
    }

    public override void OnPlayerSabotage()
    {
        // can't sabotage the trash can
    }

    public override void OnNPCSabotage(NPC npc)
    {
        // can't sabotage the trash can
    }
}