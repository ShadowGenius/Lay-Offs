using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private Text lineText;
    private DialogueNode node;
    private int lineIndex;

    void Start()
    {
        dialogueUI.SetActive(false);
    }

    void Update()
    {
        if(dialogueUI && Input.GetKeyDown(KeyCode.F))
        {
            NextSentence();
        }
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
