using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro namespace

public class CountdownTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Reference to the TextMeshProUGUI component
    public float countdownTime = 30f; // Countdown time in seconds

    private float currentTime;

    void Start()
    {
        currentTime = countdownTime;
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            Debug.Log("Current Time: " + currentTime); // Debug log
            UpdateTimerDisplay(currentTime);
        }
        else
        {
            currentTime = 0;
            UpdateTimerDisplay(currentTime);
            // You can add any action here that should happen when the timer reaches 0
        }
    }

    void UpdateTimerDisplay(float time)
    {
        int seconds = Mathf.CeilToInt(time);
        timerText.text = seconds.ToString();
    }
}
