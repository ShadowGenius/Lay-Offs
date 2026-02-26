using UnityEngine;

public class Watercooler : ObjectInteraction
{
    public override void OnPlayerUse()
    {
        player.hasWater = true;

        Debug.Log("Player now has water");
    }

    public override void OnNPCUse(NPC npc)
    {
        npc.hasWater = true;

        Debug.Log($"{npc.name} now has water");
    }

    // sabotages are not relevant (for now)
}
