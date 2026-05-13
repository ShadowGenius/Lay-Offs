using TMPro;
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
        choice1.SetActive(false);
        choice2.SetActive(false);
    }

    void Update()
    {

        if(dialogueUI && dialogueUI.activeSelf && Input.GetKeyDown(KeyCode.F) && prompt == false)
        {
            NextSentence();
        }

        if (choice1.activeSelf && choice2.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Choose(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Choose(1);
            }
        }
    }
    public void StartDialogue(DialogueNode dialogueNode)
    {
        lineIndex = 0;
        node = dialogueNode;
        dialogueUI.SetActive(true);
        lineText.text = node.lines[0];
    }

    public void HideDialogue()
    {
        dialogueUI.SetActive(false);
    }

    public void NextSentence()
    {
        lineIndex += 1;
        if (lineIndex >= node.lines.Length)
        {   
            if (node.hasChoice == true)
            {
                ShowChoice();
                return;
            }

            if (node.nextNode != null)
            {
                StartDialogue(node.nextNode);
                return;
            }


            dialogueUI.SetActive(false);
            return;
            
        }
        lineText.text = node.lines[lineIndex];
    }

    public void ShowChoice()
    {
        choice1.SetActive(true);
        choice2.SetActive(true);
        choice1Text.text = node.choices[0].label;
        choice2Text.text = node.choices[1].label;
    }


    public void Choose(int id)
    {
        Debug.Log($"press {id}");
        
        DialogueNode nextNode = node.choices[id].nextNode;
        if (nextNode!= null)
        {
            StartDialogue(nextNode);
        }
        else
        {
            dialogueUI.SetActive(false);
            prompt = true;
        }
        choice1.SetActive(false);
        choice2.SetActive(false);
    }
}
