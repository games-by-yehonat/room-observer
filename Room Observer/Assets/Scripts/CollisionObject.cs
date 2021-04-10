using UnityEngine;

public class CollisionObject : MonoBehaviour, IObserver
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private GameObject puff;
    [SerializeField] [Range(0f, 10f)] private float force = 5f;

    private void Start()
    {
        GameController.Instance.Subscribe(this);
        Kick();
    }

    public void Kick()
    {
        var dir = new Vector2(RandomValue(), RandomValue());
        body.velocity = dir * force;
    }

    private float RandomValue()
    {
        return Random.value > .5f ? 1f : -1f;
    }

    public void Notify(ISubject subject)
    {
        Instantiate(puff, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void RemoveObserver()
    {
        GameController.Instance.Unsubscribe(this);
        Destroy(gameObject);
    }
}
