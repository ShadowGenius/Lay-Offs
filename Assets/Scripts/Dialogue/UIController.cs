using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private Text lineText;
    [SerializeField] private GameObject choice1;
    [SerializeField] private Text choice1Text;
    [SerializeField] private GameObject choice2;
    [SerializeField] private Text choice2Text;
    private DialogueNode node;
    private int lineIndex;

    public bool prompt = true;
    

    void Start()
    {
        dialogueUI.SetActive(false);
    }

    void Update()
    {

        if(dialogueUI && dialogueUI.activeSelf && Input.GetKeyDown(KeyCode.F) && prompt == false)
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

    public void HideDialogue()
    {
        dialogueUI.SetActive(false);
    }

    public void ShowChoice(DialogueNode dialogueNode)
    {
        choice1.SetActive(true);
        choice2.SetActive(true);
        choice1Text.text = dialogueNode.choices[0].label;
        choice2Text.text = dialogueNode.choices[1].label;
        NextSentence();
    }

/*
    public void Chosen(DialogueNode dialogueNode ,int id)
    {
        choice1.SetActive(false);
        choice2.SetActive(false);
        if (id == 0)
        {
            StartDialogue(dialogueNode);
        }
        if (id == 1)
        {
            StartDialogue(dialogueNode);
        }
    }*/
}
