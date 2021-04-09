using UnityEngine;

public class RotateObj : MonoBehaviour
{
    [SerializeField] [Range(-10f, 10f)] private float speed;

    private void Update()
    {
        var angle = speed * Time.deltaTime;
        transform.Rotate(0f, 0f, angle * 50f, Space.Self);
    }
}
