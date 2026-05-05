using UnityEngine;

public class OtherNPC : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float interactionDistance;
    public DialogueNode dialogueNode;
    public UIController uIController;
    [SerializeField] private GameObject interactUI;
    [SerializeField] private NPCpathfinding nPCpathfinding;

    public bool startDialogue = false;
    private float speed;


    void Start()
    {
        speed = nPCpathfinding.speed;
    }

    void Update()
    {
        if(Vector3.Distance(playerTransform.position, transform.position) <= interactionDistance)
        {
            if (startDialogue == false)
            {
                startDialogue = true;
                nPCpathfinding.speed = 0f;
                interactUI.SetActive(true);
                if(interactUI && Input.GetKeyDown(KeyCode.E) && uIController.prompt == true)
                {
                    uIController.StartDialogue(dialogueNode);
                    uIController.prompt = false;
                }
                
            }
        }
        else
        {
            startDialogue = false;
            interactUI.SetActive(false);
            nPCpathfinding.speed = speed;
        }
    }
}
