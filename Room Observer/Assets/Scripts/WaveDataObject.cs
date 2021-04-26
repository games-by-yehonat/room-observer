using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Wave Data", fileName = "WaveData", order = 0)]
public class WaveDataObject : ScriptableObject
{
    public WaveConfiguration[] wavesConfiguration;
}

[System.Serializable]
public class WaveConfiguration
{
    public string name;
    [Range(0, 20)] public float duration;
    [Range(0, 20)] public int numberOfEnemies;
    public EnemyLootObject[] enemiesLoot;
    public LootObject[] powerUpsPrefab;
}