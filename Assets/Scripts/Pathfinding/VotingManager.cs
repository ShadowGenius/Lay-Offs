using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class VotingManager : MonoBehaviour
{
    public List<int> votes;
    public List<Button> buttons;
    public Button endVoteButton;
    public int employeesNum;
    void Start()
    {
        employeesNum = votes.Count;
        endVoteButton.onClick.AddListener(() => CloseVote());
        for (int i = 0; i < buttons.Count; i++)
        {
            int buttonIndex = i;
            buttons[i].onClick.AddListener(() => OnVote(buttonIndex));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnVote(int buttonIndex)
    {
        Debug.Log("Added vote for number " + (buttonIndex + 1));
        votes[buttonIndex] += 1;
        EndVote();
    }
    
    void EndVote()
    {
        for(int i = 0; i < employeesNum; i++)
        {
            randomEmployeeVote();
        }
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].gameObject.SetActive(false);
            buttons[i].transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = "Employee " + (i + 1) + "\nVotes: " + votes[i];
        }
        int highestVoteIndex = 0;
        bool isTie = false;
        for(int i = 1; i < votes.Count; i++)
        {
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
        }
        else
        {
            Debug.Log("Tie, nobody voted out");
        }
    }

    void CloseVote()
    {
        endVoteButton.gameObject.SetActive(false);
        transform.parent.gameObject.SetActive(false);
    }

    void randomEmployeeVote()
    {
        int voteNum = Random.Range(0, employeesNum);
        Debug.Log("Random voted " + voteNum);
        votes[voteNum]+= 1;
    }
}
