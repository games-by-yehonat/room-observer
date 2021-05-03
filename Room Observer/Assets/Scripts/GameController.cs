using System;
using UnityEngine;

public enum GameState
{
    None,
    Menu,
    Playing,
    Pause,
    Death
}

public class GameController : MonoBehaviour
{
    [SerializeField] private WaveHandler waveHandler;
    [SerializeField] private CameraShaker cameraShaker;
    [SerializeField] private HealthView healthView;
    
    [Space(10)]
    [SerializeField] private GameObject introCanvas;
    [SerializeField] private GameObject guiCanvas;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject puffPrefab;

    private GameState _state = GameState.None;
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
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ExecuteMethodByState();
        }
    }

    private void ExecuteMethodByState()
    {
        switch (_state)
        {
            case GameState.None:
                break;
            case GameState.Menu:
                EnableGUI();
                break;
            case GameState.Playing:
                Pause();
                break;
            case GameState.Pause:
                Resume();
                break;
            case GameState.Death:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void EnableGUI()
    {
        introCanvas.SetActive(false);
        guiCanvas.SetActive(true);
    }

    private void Pause()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        _state = GameState.Pause;
    }

    private void Resume()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        _state = GameState.Playing;
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

    public void StartGame()
    {
        waveHandler.StartWave();
        
        if (_state == GameState.Playing)
        {
            return;
        }

        _state = GameState.Playing;
        
        _player.SetActive(true);
        _puff.SetActive(true);
    }

    public void GameOver()
    {
        _state = GameState.Death;
        waveHandler.EndWave();
    }

    public void SetGameState(GameState state)
    {
        _state = state;
    }

    public void SetMenuState()
    {
        SetGameState(GameState.Menu);
    }

    public GameState GetGameState()
    {
        return _state;
    }
}