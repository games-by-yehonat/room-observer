using System.Collections;
using TMPro;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private float timeRemaining = 60f;
    
    private void Update()
    {
        if (!GameController.Instance.TimerIsRunning)
        {
            timeRemaining = 10f;
            return;
        }
        
        if (timeRemaining > 0f)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        else
        {
            timeRemaining = 0f;
            DisplayTime(timeRemaining);
            
            GameController.Instance.TimeOut();
        }
    }

    private void DisplayTime(float time)
    {
        var minutes = Mathf.FloorToInt(time / 60);
        var seconds = Mathf.FloorToInt(time % 60);
        var milliSeconds = (time % 1) * 1000;

        timer.text = $"{minutes:0}:{seconds:00}:{milliSeconds:000}";
    }
}
