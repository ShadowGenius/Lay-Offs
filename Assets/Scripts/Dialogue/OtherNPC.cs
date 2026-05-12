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
        //if (interactUI && interactUI.activeSelf)
        //{
        //    Debug.Log("interact shown");
        //}
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        if (distance <= interactionDistance)
        {
            playerNear = true;
        }
        else if (playerNear)
        {
            Debug.Log("no longer near NPC");
            playerNear = false;
            interactUI.SetActive(false);
            uIController.prompt = true;
            uIController.HideDialogue();
            nPCpathfinding.speed = speed;
            startDialogue = false;
        }

        if (playerNear && !interactUI.activeSelf && uIController.prompt)
        {
            nPCpathfinding.speed = 0f;
            startDialogue = true;
            interactUI.SetActive(true);
        }

        if (startDialogue && Input.GetKeyDown(KeyCode.E))
        {
            interactUI.SetActive(false);
            Debug.Log("pressed interact");
            uIController.StartDialogue(dialogueNode);
            uIController.prompt = false;
        }
    }
}
