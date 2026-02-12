using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public float interactionDistance = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Interact();
    }

    public bool IsWithinInteractionDistance()
    {
        return Vector2.Distance(transform.position, player.transform.position) <= interactionDistance;
    }

    public void Interact()
    {
        if (IsWithinInteractionDistance() && Input.GetKeyDown(KeyCode.E))
        {
            OnInteraction();
        }
    }

    public virtual void OnInteraction()
    {
        // override this

        Debug.Log("Default object interacted with");
    }
}
