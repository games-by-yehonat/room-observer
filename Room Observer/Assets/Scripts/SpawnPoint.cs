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