using Unity.VisualScripting;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField] public GameObject playerObject;
    [SerializeField] public float interactionDistance = 2.2f;

    protected Player player => playerObject.GetComponent<Player>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Interact();
    }

    public bool PlayerWithinInteractionDistance()
    {
        return Vector2.Distance(transform.position, playerObject.transform.position) <= interactionDistance;
    }

    public void Interact()
    {
        if (PlayerWithinInteractionDistance() && Input.GetKeyDown(KeyCode.E))
        {
            OnPlayerUse();

            Debug.Log($"Player interaction at distance {Vector2.Distance(transform.position, playerObject.transform.position)}");

            // handle sabotaging later
        } else if (Input.GetKeyDown(KeyCode.E))
        {
            // Debug.Log($"Player likely too far (distance = {Vector2.Distance(transform.position, playerObject.transform.position)}) from this {GetType()}");
        }
    }

    public virtual void OnPlayerUse()
    {
        // override this

        Debug.Log("Default object interacted with by player");
    }

    public virtual void OnPlayerSabotage()
    {
        Debug.Log("Default object sabotaged by player");
    }

    public virtual void OnNPCUse(NPC npc)
    {
        Debug.Log("Default object interacted with by NPC");
    }

    public virtual void OnNPCSabotage(NPC npc)
    {
        Debug.Log("Default object sabotaged by NPC");
    }
}
