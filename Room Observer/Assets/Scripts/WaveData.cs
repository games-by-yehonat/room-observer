using UnityEngine;

[CreateAssetMenu(menuName = "Wave Data", fileName = "WaveData", order = 0)]
public class WaveData : ScriptableObject
{
    public Wave[] waves;
}

[System.Serializable]
public class Wave
{
    public string name;
    [Range(0, 20)] public int numberOfCollider;
    public GameObject[] colliders;
    public bool hasPowerUps;
    public GameObject[] powerUps;
}