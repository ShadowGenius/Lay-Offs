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
    [SerializeField] private GameObject choice3;
    [SerializeField] private Text choice3Text;
    [SerializeField] private Text nameText;
    private DialogueNode node;
    private Character currentNPC;
    private Character player;
    private int lineIndex;

    public bool prompt = true;
    

    void Start()
    {
        dialogueUI.SetActive(false);
        choice1.SetActive(false);
        choice2.SetActive(false);
        choice3.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
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
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Choose(2);
            }
        }
    }
    public void StartDialogue(DialogueNode dialogueNode, Character npc)
    {
        currentNPC = npc;
        nameText.text = dialogueNode.speakerName;
        
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
                StartDialogue(node.nextNode, currentNPC);
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
        choice3.SetActive(true);
        choice1Text.text = node.choices[0].label;
        choice2Text.text = node.choices[1].label;
        choice3Text.text = "Check friendliness";
    }


    public void Choose(int id)
    {
        Debug.Log($"press {id}");
        if (id == 0 || id == 1)
        {
            DialogueNode nextNode = node.choices[id].nextNode;
            if (nextNode!= null)
            {
                StartDialogue(nextNode, currentNPC);
            }
            else
            {
                dialogueUI.SetActive(false);
                prompt = true;
            }
            choice1.SetActive(false);
            choice2.SetActive(false);
            choice3.SetActive(false);
        }
        else if (id == 2)
        {   
            ShowFriendliness();
            choice1.SetActive(false);
            choice2.SetActive(false);
            choice3.SetActive(false);
        }
    }

    public void ShowFriendliness()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        double friendliness = currentNPC.FriendlinessTo(player);
        dialogueUI.SetActive(true);
        if (friendliness < 30)
        {
            lineText.text = $"{currentNPC.name} seems distant from you.";
        }
        else if (friendliness < 60)
        {
            lineText.text = $"{currentNPC.name} is neutral toward you.";
        }
        else if (friendliness < 80)
        {
            lineText.text = $"{currentNPC.name} seems friendly toward you.";
        }
        else
        {
            lineText.text = $"{currentNPC.name} trusts you a lot.";
        }
        
    }
}
