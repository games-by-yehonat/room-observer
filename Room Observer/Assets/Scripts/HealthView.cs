using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : GUITween, IObserver
{
    [SerializeField] private TextMeshProUGUI amount;
    [SerializeField] private Image[] thumbs;

    private void Start()
    {
        EnterFadeInScene();
    }

    public void Notify(ISubject subject)
    {
        if (!(subject is HealthHandler handler))
        {
            return;
        }

        SetHealthInUI(handler);
    }

    private void SetHealthInUI(HealthHandler handler)
    {
        for (var i = 0; i < handler.MaxHealth; i++)
        {
            var healthState = i < handler.Health;
            thumbs[i].gameObject.SetActive(healthState);
        }

        amount.text = $"x{handler.Health}";
    }
}