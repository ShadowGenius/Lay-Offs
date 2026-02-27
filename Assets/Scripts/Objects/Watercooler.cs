using UnityEngine;

public class Watercooler : ObjectInteraction
{
    public override void OnPlayerUse()
    {
        if (!player.isHandEmpty())
        {
            Debug.Log("Player is holding too much for water");
            return;
        }
        player.hasWater = true;
        player.heldItem = Character.Item.Water;

        Debug.Log("Player now has water");
    }

    public override void OnNPCUse(NPC npc)
    {
        if (!npc.isHandEmpty())
        {
            Debug.Log("NPC is holding too much for water");
            return;
        }
        npc.hasWater = true;
        npc.heldItem = Character.Item.Water;

        Debug.Log($"{npc.name} now has water");
    }

    // sabotages are not relevant (for now)
}
