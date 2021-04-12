using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint
{
    public readonly Transform Trans;
    public bool Taken;

    public SpawnPoint(Transform trans, bool taken)
    {
        Trans = trans;
        Taken = taken;
    }
}

public class WaveController : MonoBehaviour, ISubject
{
    [SerializeField] private Transform spawnPoints;
    [SerializeField] private WaveData waveData;
    [SerializeField] private GameObject puff;
    private readonly List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();
    private readonly List<GameObject> _colliders = new List<GameObject>();

    private int _wave;
    
    private readonly List<IObserver> _observers = new List<IObserver>();

    private void Start()
    {
        SetSpawnPointList();
        SetNewWave();
    }

    public void SetNewWave()
    {
        SpawnColliders(waveData.waves[_wave]);
        _wave++;
    }

    private void SetSpawnPointList()
    {
        foreach (Transform point in spawnPoints)
        {
            _spawnPoints.Add(new SpawnPoint(point, false));
        }
    }

    private void SpawnColliders(Wave wave)
    {
        Debug.Log(wave.name);

        for (var i = 0; i < wave.numberOfCollider; i++)
        {
            var point = SetRandomPoint();
            var rand = Random.Range(0, wave.colliders.Length);
            
            var position = point.Trans.position;
            var rotation = point.Trans.rotation;
            
            var col = Instantiate(wave.colliders[rand], position, rotation);
            var p = Instantiate(puff, position, rotation);
            
            col.SetActive(false);
            p.SetActive(false);
            
            _colliders.Add(col);
            _colliders.Add(p);
        }
    }

    private SpawnPoint SetRandomPoint()
    {
        Debug.Log("a");
        var rand = Random.Range(0, _spawnPoints.Count);
        var point = _spawnPoints[rand];

        if (point.Taken)
        {
            SetRandomPoint();
        }

        point.Taken = true;
        return point;
    }

    public void EnableColliders()
    {
        foreach (var o in _colliders)
        {
            o.SetActive(true);
        }

        _colliders.Clear();
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