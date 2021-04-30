using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private WaveHandler waveHandler;
    [SerializeField] private CameraShaker cameraShaker;
    [SerializeField] private HealthView healthView;
    
    [Space(10)]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject puffPrefab;
    
    public bool Playing { get; private set; }
    public bool IsDeath { get; private set;  }
    public static GameController Instance { get; private set; }

    private GameObject _player;
    private GameObject _puff;


    private void Awake()
    {
        Instance = this;
        
        CreatePlayer();
        CreatePuff();
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
        _player.SetActive(false);
        
        var health = _player.GetComponent<HealthHandler>();
        health.Subscribe(healthView);
        health.Subscribe(cameraShaker);
    }

    private void CreatePuff()
    {
        _puff = Instantiate(puffPrefab, Vector3.zero, Quaternion.identity);
        _puff.SetActive(false);
    }

    private void StartGame()
    {
        if (Playing)
        {
            return;
        }
        
        Playing = true;
        
        _player.SetActive(true);
        _puff.SetActive(true);
        
        waveHandler.StartWave();
    }

    public void GameOver()
    {
        Playing = false;
        IsDeath = true;
        
        waveHandler.EndWave();
    }
}