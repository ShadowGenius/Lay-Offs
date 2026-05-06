using Unity.VisualScripting;
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
    public bool playerNear = false;


    void Start()
    {
        speed = nPCpathfinding.speed;
        interactUI.SetActive(false);
    }

    void Update()
    {
        if (interactUI)
        {
            Debug.Log("interact shown");
        }
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        if(distance <= interactionDistance)
        {
            playerNear = true;
            nPCpathfinding.speed = 0f;
            if (startDialogue == false)
            {
                startDialogue = true;
                if(uIController.prompt == true)
                {
                    interactUI.SetActive(true);
                }
                
                if(Input.GetKeyDown(KeyCode.E) && uIController.prompt == true)
                {
                    interactUI.SetActive(false);
                    uIController.StartDialogue(dialogueNode);
                    uIController.prompt = false;
                }
                
            }
        }
        else
        {
            playerNear = false;
            interactUI.SetActive(false);
            nPCpathfinding.speed = speed;
            uIController.prompt = true;
        }
    }
}
