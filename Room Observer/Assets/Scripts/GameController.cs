using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour, ISubject
{
    [SerializeField] private CameraShaker cameraShaker;
    [SerializeField] private HealthView healthView;
    
    [SerializeField] private GameObject player;
    public static GameController Instance { get; private set; }

    public bool IsDeath { get; private set;  }

    private readonly List<IObserver> _observers = new List<IObserver>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        var playerObject = Instantiate(player, Vector3.zero, Quaternion.identity);
        var health = playerObject.GetComponent<HealthHandler>();
        health.Subscribe(healthView);
        health.Subscribe(cameraShaker);
    }

    public void GameOver()
    {
        IsDeath = true;
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