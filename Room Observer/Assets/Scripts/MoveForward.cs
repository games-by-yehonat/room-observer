using System;
using UnityEngine;

public enum Directions
{
    Zero,
    Up,
    Right,
    Down,
    Left
}

public static class Use
{
    public static Vector2 GetVectorDirection(Directions dir)
    {
        switch (dir)
        {
            case Directions.Zero:
                return Vector2.zero;
            case Directions.Up:
                return Vector2.up;
            case Directions.Right:
                return Vector2.right;
            case Directions.Down:
                return Vector2.down;
            case Directions.Left:
                return Vector2.left;
            default:
                throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
        }
    }
}

public class MoveForward : MonoBehaviour
{
    [SerializeField] private Directions direction = Directions.Zero;
    [SerializeField] [Range(0f, 10f)] private float speed = 1.5f;

    public Vector2 Dir { get; set; }
    public float Speed => speed;

    private void Start()
    {
        Dir = Use.GetVectorDirection(direction);
    }

    private void Update()
    {
        DoMove();
    }

    private void DoMove()
    {
        var fixedSpeed = speed * Time.deltaTime;
        transform.Translate(Dir * fixedSpeed);
    }
}
