using UnityEngine;

public class Computer : ObjectInteraction
{
    public Character owner;
    Player player => playerObject.GetComponent<Player>();
    public override void OnPlayerUse()
    {

        Debug.Log("Computer doing computer things");

        if (player != owner)
        {
            Debug.Log("Player trying to use someone else's computer"); // don't let this happen
            return;
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

    public override void OnNPCUse()
    {
        // ...
    }

    public override void OnNPCSabotage()
    {
        // ...
    }
}
