using UnityEngine;

public class Printer : ObjectInteraction
{
    // communal printer, no one owns it (since it is related to untargeted sabotage)
    public override void OnPlayerUse()
    {

        Debug.Log("Printer doing printer things");
    }
}
