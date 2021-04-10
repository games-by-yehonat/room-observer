using UnityEngine;

public class BackgroundRepeat : MonoBehaviour
{
    [SerializeField] [Range(0f, 2f)] private float speed = 1f;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        Repeat();
    }

    private void Repeat()
    {
        var length = Mathf.Abs(minY) + maxY;
        var newPosition = Mathf.Repeat(Time.time * speed, length);
        transform.position = _startPosition + Vector3.left * newPosition;
    }
}