using UnityEngine;
using UnityEngine.Events;

public class EventsAnimations : MonoBehaviour
{
    [SerializeField] private UnityEvent[] eventToRaise;
    
    public void RunEventRaise(int eventIndex)
    {
        eventToRaise[eventIndex]?.Invoke();
    }
}
