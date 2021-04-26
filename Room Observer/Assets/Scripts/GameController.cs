using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour, ISubject
{
    [SerializeField] private WaveController _waveController;
    [SerializeField] private CameraShaker cameraShaker;
    [SerializeField] private HealthView healthView;
    
    [Space(10)]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject puffPrefab;
    
    public static GameController Instance { get; private set; }
    public bool Playing { get; private set; }
    public bool IsDeath { get; private set;  }
    public bool CoolDown { get; private set; }
    public bool TimerIsRunning { get; private set; }

    private WaitForSeconds _delay;

    private readonly List<IObserver> _observers = new List<IObserver>();

    private GameObject _player;
    private GameObject _puff;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _delay = new WaitForSeconds(.5f);
        CreatePlayer();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !IsDeath)
        {
            StartGame();
        }
    }

    private void CreatePlayer()
    {
        _player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        
        var health = _player.GetComponent<HealthHandler>();
        health.Subscribe(healthView);
        health.Subscribe(cameraShaker);
        
        _player.SetActive(false);
        
        _puff = Instantiate(puffPrefab, Vector3.zero, Quaternion.identity);
        _puff.SetActive(false);
    }
    
    private void StartGame()
    {
        _waveController.StartWave();
        TimerIsRunning = true;
        
        if (Playing)
        {
            return;
        }
        
        Playing = true;
        
        
        _player.SetActive(true);
        _puff.SetActive(true);
    }

    public void TimeOut()
    {
        TimerIsRunning = false;
        
        NotifyObserver();
        _waveController.CreateNewWave();
    }

    public void GameOver()
    {
        Playing = false;
        IsDeath = true;
        TimerIsRunning = false;
        
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
            Debug.Log(observer);
            observer.Notify(this);
        }
        
        _observers.Clear();
    }
    
    public void SetCoolDown()
    {
        CoolDown = true;
        StartCoroutine(DisableCoolDown());
    }

    private IEnumerator DisableCoolDown()
    {
        yield return _delay;
        CoolDown = false;
    }
}