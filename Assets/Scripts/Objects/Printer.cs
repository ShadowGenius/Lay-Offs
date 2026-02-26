using UnityEngine;

public class Printer : ObjectInteraction
{
    // communal printer, no one owns it (since it is related to untargeted sabotage)
    public override void OnPlayerUse()
    {

        Debug.Log("Printer doing printer things");
    }

    public override void OnNPCUse()
    {
        Debug.Log("NPC using printer");
    }

    public override void OnPlayerSabotage()
    {
        Debug.Log("Player sabotaging printer");
    }

    public override void OnNPCSabotage()
    {
        Debug.Log("NPC sabotaging printer");
    }
}
