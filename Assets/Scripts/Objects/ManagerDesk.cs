using UnityEngine;

public class ManagerDesk : ObjectInteraction
{
    // communal printer, no one owns it (since it is related to untargeted sabotage)
    public override void OnPlayerUse()
    {

        Debug.Log("Messing with the manager's desk");

        Delivery deliveryTask = player.playerActions.Find(action => action is Delivery && action.IsNotFinished()) as Delivery;
        
        if (deliveryTask != null && player.heldItem == Character.Item.Paper)
        {
            deliveryTask.MakeProgress();
            player.heldItem = Character.Item.None;

            Debug.Log($"Player made progress on delivery ({deliveryTask.PercentComplete()}% complete)");
        }
    }

    public override void OnNPCUse(NPC npc)
    {
        Debug.Log("NPC using manager's desk");
    }

    public override void OnPlayerSabotage()
    {
        // can't sabotage the manager's desk
    }

    public override void OnNPCSabotage(NPC npc)
    {
        // can't sabotage the manager's desk
    }
}
