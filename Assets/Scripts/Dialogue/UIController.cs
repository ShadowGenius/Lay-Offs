using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private Text lineText;
    [SerializeField] private GameObject interactUI;
    private DialogueNode node;
    private int lineIndex;
    public bool prompt = true;
    

    void Start()
    {
        dialogueUI.SetActive(false);
        interactUI.SetActive(false);
    }

    void Update()
    {

        if(dialogueUI && Input.GetKeyDown(KeyCode.F) && prompt == false)
        {
            NextSentence();
        }
    }

    public void StartPrompt(DialogueNode dialogueNode)
    {
        interactUI.SetActive(true);
        node = dialogueNode;
        prompt = true;
    }
    public void StartDialogue(DialogueNode dialogueNode)
    {
        lineIndex = 0;
        node = dialogueNode;
        dialogueUI.SetActive(true);
        lineText.text = node.lines[0];
    }

    public void NextSentence()
    {
        lineIndex += 1;
        if (lineIndex >= node.lines.Length)
        {
            dialogueUI.SetActive(false);
            return;
        }
        lineText.text = node.lines[lineIndex];
            
        
    }
}
