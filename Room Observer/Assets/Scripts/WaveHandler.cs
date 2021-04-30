using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHandler : MonoBehaviour, ISubject
{
    [SerializeField] private TimerView timerView;
    [SerializeField] private Transform parentOfEnemies;
    [SerializeField] private Transform parentOfSpawnPoints;
    [SerializeField] private WaveDataObject waveDataObject;

    private readonly List<IObserver> _observers = new List<IObserver>();
    private readonly List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();

    private int _waveCount;
    private int _waveLenght;
    private float _waveDuration;
    private IEnumerator _waveRoutine;
    
    public bool TimerIsRunning { get; private set; }
    
    // subject implementation
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
        _waveLenght = waveDataObject.wavesConfiguration.Length;
        _waveRoutine = WaveRunning();
        
        AddSpawnPointsToTheList();
        CreateNewWave();
    }
    
    private void AddSpawnPointsToTheList()
    {
        foreach (Transform point in parentOfSpawnPoints)
        {
            _spawnPoints.Add(new SpawnPoint(point, false));
        }
    }

    public void CreateNewWave()
    {
        if (_waveCount >= _waveLenght)
        {
            print("Finish!!!");
            return;
        }

        var wave = waveDataObject.wavesConfiguration[_waveCount];
        
        CreateEnemiesByWave(wave);
        
        _waveDuration = wave.duration;
        DisplayTime(_waveDuration);
        _waveCount++;
    }

    private void CreateEnemiesByWave(WaveConfiguration waveConfiguration)
    {
        print(waveConfiguration.name);

        for (var i = 0; i < waveConfiguration.numberOfEnemies; i++)
        {
            var lootObjects = waveConfiguration.enemiesLoot;
            var totalLoot = CalculateTotalLoot(lootObjects);
            var index = GetLootIndex(lootObjects, totalLoot);
            
            var obj = waveConfiguration.enemiesLoot[index];
            var prefab = obj.prefab;
            
            var point = GetRandomPoint();
            var position = point.Trans.position;
            var rotation = point.Trans.rotation;
            
            var enemy = Instantiate(prefab, position, rotation, parentOfEnemies);
            enemy.Initialization(this, obj.force);
            enemy.gameObject.SetActive(false);

            Subscribe(enemy);
        }
    }
    
    private int CalculateTotalLoot(EnemyLootObject[] objects)
    {
        var result = 0;

        foreach (var t in objects)
        {
            result += t.loot;
        }

        return result;
    }
    
    private int GetLootIndex(EnemyLootObject[] objects, int totalLoot)
    {
        var rand = Random.Range(0, totalLoot);

        var result = 0;
        var accumulate = 0;

        for (var i = 0; i < objects.Length; i++)
        {
            var part = objects[i];
            if (part.loot == 0)
            {
                continue;
            }
            
            accumulate += part.loot;

            if (rand > accumulate)
            {
                continue;
            }
            
            result = i;
            break;
        }

        return result;
    }
    
    private SpawnPoint GetRandomPoint()
    {
        var rand = Random.Range(0, _spawnPoints.Count);
        var point = _spawnPoints[rand];

        if (point.Taken)
        {
            GetRandomPoint();
        }

        point.Taken = true;
        return point;
    }

    private void EnableEnemies()
    {
        foreach (Transform o in parentOfEnemies)
        {
            o.gameObject.SetActive(true);
        }
    }

    private void CleanSpawnPoint()
    {
        foreach (var point in _spawnPoints)
        {
            point.Taken = false;
        }
    }

    public void StartWave()
    {
        EnableEnemies();
        CleanSpawnPoint();

        TimerIsRunning = true;
        
        StartCoroutine(_waveRoutine);
    }

    public void EndWave()
    {
        TimerIsRunning = false;
        
        NotifyObserver();
        StopCoroutine(_waveRoutine);
    }

    private IEnumerator WaveRunning()
    {
        while (_waveDuration > 0f)
        {
            _waveDuration -= Time.deltaTime;
            DisplayTime(_waveDuration);
            yield return null;
        }
        
        _waveDuration = 0f;
        DisplayTime(_waveDuration);
        CreateNewWave();
        EndWave();
    }

    private void DisplayTime(float time)
    {
        var minutes = Mathf.FloorToInt(time / 60);
        var seconds = Mathf.FloorToInt(time % 60);
        var milliSeconds = (time % 1) * 1000;

        var text = $"{minutes:0}:{seconds:00}:{milliSeconds:000}";
        
        timerView.SetTimeInText(text);
    }
}
