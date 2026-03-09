using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class VotingManager : MonoBehaviour
{
    public List<int> votes;
    public List<Button> buttons;
    public List<GameObject> employees;
    public Button endVoteButton;
    public List<bool> alive;
    public int employeesNum;
    private int votedOut;
    void Start()
    {
        endVoteButton.onClick.AddListener(() => CloseVote());
        for (int i = 0; i < buttons.Count; i++)
        {
            int buttonIndex = i;
            buttons[i].onClick.AddListener(() => OnVote(buttonIndex));
            alive[i] = true;
        }
    }
    private void OnEnable()
    {
        employeesNum = votes.Count;
        for (int i = 0; i < buttons.Count; i++)
        {
            votes[i] = 0;
            buttons[i].gameObject.SetActive(true);
            if (alive[i] == true)
            {
                buttons[i].transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = "Employee " + (i + 1);
            }
            else
            {
                buttons[i].transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = "Permanent vacation";
                Debug.Log("Permanent Vacation");
            }
        }
        endVoteButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void StartVoting()
    {
        employeesNum = votes.Count;
        endVoteButton.onClick.AddListener(() => CloseVote());
        for (int i = 0; i < buttons.Count; i++)
        {
            int buttonIndex = i;
            buttons[i].onClick.AddListener(() => OnVote(buttonIndex));
        }
    }
    void OnVote(int buttonIndex)
    {
        Debug.Log("Added vote for number " + (buttonIndex + 1));
        votes[buttonIndex] += 1;
        EndVote();
    }
    
    void EndVote()
    {
        //Player gets a vote so start at 1
        for(int i = 1; i < employeesNum; i++)
        {
            if(alive[i] == true)
                randomEmployeeVote();
        }
        int highestVoteIndex = 0;
        bool isTie = false;
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].gameObject.SetActive(false);
            buttons[i].transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = "Employee " + (i + 1) + "\nVotes: " + votes[i];
            
            if(votes[i] > votes[highestVoteIndex])
            {
                isTie = false;
                highestVoteIndex = i;
            }
            else if(votes[i] == votes[highestVoteIndex])
            {
                isTie = true;
            }
        }
        endVoteButton.gameObject.SetActive(true);
        if (!isTie)
        {
            Debug.Log("Employee " + (highestVoteIndex + 1) + " Voted out");
            votedOut = highestVoteIndex;
            employeesNum--;
            alive[votedOut] = false;
        }
        else
        {
            votedOut = -1;
            Debug.Log("Tie, nobody voted out");
        }
    }

    void CloseVote()
    {
        endVoteButton.gameObject.SetActive(false);
        transform.parent.gameObject.SetActive(false);
        if (votedOut != -1)
            employees[votedOut].gameObject.SetActive(false);
    }

    void randomEmployeeVote()
    {
        //They kept voting me out so make sure they don't vote the player.
        int voteNum;
        do
        {
            voteNum = Random.Range(1, employeesNum);
        } while (alive[voteNum] == false);
        Debug.Log("Random voted " + voteNum);
        votes[voteNum]+= 1;
    }
}
