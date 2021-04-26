using TMPro;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer;

    public void SetTimeInText(string value)
    {
        timer.text = value;
    }
}
