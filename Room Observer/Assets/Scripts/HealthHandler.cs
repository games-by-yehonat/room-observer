using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour, IDamageable, ISubject
{
    [SerializeField] private AnimationHandler anim;
    [SerializeField] private GameObject puff;
    [SerializeField] [Range(.1f, 1f)] private float coolDownDelay = .5f;

    public int MaxHealth { get; private set; }
    public int Health { get; private set; }

    private bool _coolDown;
    private WaitForSeconds _delay;

    private readonly List<IObserver> _observers = new List<IObserver>();
    
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
    
    private void Start()
    {
        _delay = new WaitForSeconds(coolDownDelay);
        
        Health = 2;
        MaxHealth = Health;
    }
    
    private void SetCoolDown()
    {
        _coolDown = true;
        StartCoroutine(DisableCoolDown());
    }

    private IEnumerator DisableCoolDown()
    {
        yield return _delay;
        _coolDown = false;
    }

    public void TakeDamage()
    {
        if (_coolDown)
        {
            return;
        }
        
        Health--;

        if (Health > 0)
        {
            anim.SetDamageState(true);
            SetCoolDown();
        }
        else
        {
            Health = 0;
            puff.SetActive(true);
            GameController.Instance.GameOver();
        }
        
        anim.SetDeathState();

        NotifyObserver();
    }

    public bool GetCoolDown()
    {
        return _coolDown;
    }
}
