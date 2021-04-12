using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour, IDamageable, ISubject
{
    [SerializeField] private AnimationHandler anim;
    [SerializeField] private GameObject puff;

    public int MaxHealth { get; private set; }
    public int Health { get; private set; }

    private readonly List<IObserver> _observers = new List<IObserver>();
    
    private void Start()
    {
        Health = 2;
        MaxHealth = Health;
    }

    public void TakeDamage()
    {
        if (GameController.Instance.CoolDown)
        {
            return;
        }
        
        Health--;

        if (Health > 0)
        {
            anim.SetDamageState(true);
        }
        else
        {
            Health = 0;
            
            GameController.Instance.GameOver();
            
            puff.SetActive(true);
        }
        
        anim.SetDeathState();

        NotifyObserver();
    }

    public void Subscribe(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void Unsubscribe(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObserver()
    {
        foreach (var observer in _observers)
        {
            observer.Notify(this);
        }
    }
}
