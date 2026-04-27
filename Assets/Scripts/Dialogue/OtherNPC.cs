using UnityEngine;

public class OtherNPC : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float interactionDistance;
    public DialogueNode dialogueNode;
    public UIController uIController;

    private bool startDialogue = false;

    void Update()
    {
        if(Vector3.Distance(playerTransform.position, transform.position) <= interactionDistance)
        {
            if (startDialogue == false)
            {
                startDialogue = true;
                uIController.StartDialogue(dialogueNode);
            }
        }
        else
        {
            startDialogue = false;
        }
    }
}
