using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class VotingManager : MonoBehaviour
{
    public List<int> votes;
    public List<Button> buttons;
    public Button endVoteButton;
    void Start()
    {
        endVoteButton.onClick.AddListener(() => EndVote());
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
    }
    
    void EndVote()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].gameObject.SetActive(false);
            buttons[i].transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = "Employee " + (i + 1) + "\nVotes: " + votes[i];
        }
        int highestVoteIndex = 0;
        for(int i = 1; i < votes.Count; i++)
        {
            if(votes[i] > votes[highestVoteIndex])
            {
                highestVoteIndex = i;
            }
        }
        endVoteButton.gameObject.SetActive(false);
        Debug.Log("Employee " + (highestVoteIndex + 1) + " Voted out");
    }
}
