using UnityEngine;

public class Watercooler : ObjectInteraction
{
    public override void OnPlayerUse()
    {
        use(player);
    }

    public override void OnNPCUse(NPC npc)
    {
        use(npc);
    }

    public void use(Character ch)
    {
        if (!ch.isHandEmpty())
        {
            Debug.Log($"{ch.name} is holding too much for water");
            return;
        }
        ch.heldItem = Character.Item.Water;

        Debug.Log($"{ch.name} now has water");
    }

    // sabotages are not relevant (for now)
}
