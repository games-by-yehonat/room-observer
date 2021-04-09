using UnityEngine;

public class CollisionObject : MonoBehaviour
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] [Range(0f, 10f)] private float speed = 5f;

    private void Start()
    {
        Kick();
    }

    public void Kick()
    {
        var dir = new Vector2(RandomValue(), RandomValue());
        body.velocity = dir * speed;
    }

    private float RandomValue()
    {
        return Random.value > .5f ? 1f : -1f;
    }
}
