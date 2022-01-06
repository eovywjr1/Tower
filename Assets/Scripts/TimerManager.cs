using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public Text timerText;
    public float timeMax, timeCurrent;
    public bool isEnd;

    void Start()
    {
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnd)
            return;
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
        isEnd = true;
    }
}
