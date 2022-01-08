using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public Text timerText;
    public PlayerScript playerScript;
    public UiManager uiManager;
    public float timeMax, timeCurrent;
    public bool isStart, isEnd;

    void Start()
    {
        ResetTimer();
    }

    void Update()
    {
        if (isEnd)
            return;

        if (isStart)
            UpdateTimer();
    }

    void ResetTimer()
    {
        timeCurrent = timeMax;
        timerText.text = $"{timeCurrent:N1}";
        isEnd = false;
    }

    void UpdateTimer()
    {
        if (timeCurrent > 0)
        {
            timeCurrent -= Time.deltaTime;
            timerText.text = $"{timeCurrent:N1}";
        }
        else
            EndTimer();
    }

    void EndTimer()
    {
        timeCurrent = 0;
        timerText.text = $"{timeCurrent:N1}";
        playerScript.isDie = true;
        uiManager.PlayerDiedShowText();
        isEnd = true;
    }
}