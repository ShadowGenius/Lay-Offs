using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class GameTimeManager : MonoBehaviour
{
    [SerializeField] public int earliestHour = 9;
    [SerializeField] public int latestHour = 17;
    [SerializeField] public GameObject VotingUI;
    [SerializeField] public float realSecondsPerMinute = 1f;
    [SerializeField] public TextMeshProUGUI timeText = null;

    private int currentDay = 1;
    private int currentHour; // goes from 9 to 17
    private int currentMinute = 0;

    private int minutesElapsedToday = 0;

    public string clockString => GetCurrentTimeString();

    private bool timePaused;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHour = earliestHour;

        StartCoroutine(DayLoop());
    }

    public String GetCurrentTimeString()
    {
        bool isPM = currentHour >= 12;

        if (isPM)
        {
            return $"{((currentHour > 12) ? (currentHour - 12) : 12)}:{currentMinute:00} PM";
        } else
        {
            return $"{currentHour}:{currentMinute:00} AM";
        }
    }

    public void AdvanceTime()
    {
        // should be called every second, advances one minute
        // Debug.Log($"Current minute: {currentMinute}");
        if (timePaused)
        {
            return;
        }

        ++minutesElapsedToday;
        ++currentMinute;

        if (currentMinute >= 60)
        {
            currentMinute = 0;
            currentHour += 1;
        }

        if (currentHour >= latestHour)
        {
            currentHour = latestHour;
            currentMinute = 0;
            PauseTime();
            StartVoting();
        }
        else
        {
            timeText.text = GetCurrentTimeString();
        }
    }

    public void AdvanceDay()
    {
        currentDay += 1;
        currentHour = earliestHour;
        currentMinute = 0;
        minutesElapsedToday = 0;

        Debug.Log($"Day {currentDay}, Clock: {clockString}");
    }

    public void PauseTime()
    {
        timePaused = true;
    }

    public void StartVoting()
    {
        // start voting logic
        VotingUI.SetActive(true);
    }

    public void ResumeTime()
    {
        timePaused = false;
        StartCoroutine(DayLoop());
    }

    // Update is called once per frame
    void Update()
    {


        if (timePaused)
        {
            // AdvanceDay();
            // ResumeTime();
        }
    }

    private IEnumerator DayLoop()
    {
        while (!timePaused)
        {
            yield return new WaitForSeconds(realSecondsPerMinute);
            AdvanceTime();
        }

        yield break;
    }
}
