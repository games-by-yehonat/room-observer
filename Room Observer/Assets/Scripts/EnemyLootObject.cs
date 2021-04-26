using UnityEngine;

[System.Serializable]
public class EnemyLootObject : LootObject
{
    public ReboundHandler prefab;
    [Range(.1f, 20f)] public float force;
}
