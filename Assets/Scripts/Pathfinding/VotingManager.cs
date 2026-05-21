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
        GameObject[] employeesArray = GameObject.FindGameObjectsWithTag("NPC");
        employees = new List<GameObject>(employeesArray);
        employees.Insert(0, GameObject.FindGameObjectWithTag("Player"));
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
        for(int i = 1; i < employees.Count; i++)
        {
            if (alive[i] == true)
                employeeVote(i);
                //randomEmployeeVote();
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
            votedOut = bossVote();
            //Debug.Log("Tie, nobody voted out");
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
    void employeeVote(int employeeNum)
    {
        //Debug.Log("EMPLOYEE VOTING TEIPOT GIJPOERJHPOIUHPERP");
        Dictionary<Character, double> employeeRelations = new Dictionary<Character, double>();
        //Debug.Log("Employee apriufjesiougero: " + employeeNum);
        employeeRelations = employees[employeeNum].GetComponent<NPC>().friendlinessValues;
        Debug.Log(employeeRelations.Keys);
        var enumerator = employeeRelations.Keys.GetEnumerator();
        if (!enumerator.MoveNext())
        {
            Debug.Log("NOTVOTING");
            return;

        }
        Character leastChar = enumerator.Current;
        List<Character> tieChars = new List<Character>();
        int voteNum = 0;
        int i = 0;
        do
        {
            //Debug.Log("COMPARING: " + enumerator.Current + " (" + employeeRelations[enumerator.Current] + ") with " + leastChar + " (" + employeeRelations[leastChar] + ")");
            if (employeeRelations[enumerator.Current] < employeeRelations[leastChar] && alive[i] == true)
            {
                leastChar = enumerator.Current;
                voteNum = i;
                tieChars.Clear();
                tieChars.Add(leastChar);
            }
            else if(employeeRelations[enumerator.Current] == employeeRelations[leastChar])
            {
                tieChars.Add(enumerator.Current);
            }
            i++;
        } while (enumerator.MoveNext());
        Debug.Log("Employee " + employeeNum + " voted for " + voteNum);
        if (tieChars.Count > 1)
        {
            votes[Random.Range(0, tieChars.Count)] += 1;
        }
        else
        {
            votes[voteNum] += 1;
        }
    }

    int bossVote()
    {
        List<Character> characters = new List<Character>();
        for(int i = 0; i < employees.Count; i++)
        {
            characters.Add(employees[i].GetComponent<Character>());
        }
        int leastProductiveIndex = 0;
        for(int i = 1; i < characters.Count; i++)
        {
            if(leastProductiveIndex < characters[i].tasksCompleted)
            {
                leastProductiveIndex = i;
            }
        }
        Debug.Log("Boss voting for (SLACKER) " + leastProductiveIndex);
        return leastProductiveIndex;
    }
}
