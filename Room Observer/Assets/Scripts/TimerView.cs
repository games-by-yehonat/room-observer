using TMPro;
using UnityEngine;

public class TimerView : GUITween
{
    [SerializeField] private TextMeshProUGUI timer;
    
    private void Start()
    {
        EnterFadeInScene();
    }

    public void SetTimeInText(string value)
    {
        timer.text = value;
    }
}
