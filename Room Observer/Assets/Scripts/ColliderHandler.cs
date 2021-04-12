using UnityEngine;

public class ColliderHandler : MonoBehaviour, IObserver
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private GameObject puff;
    
    [Space(10)]
    [SerializeField] [Range(0f, 10f)] private float force = 5f;

    private void Start()
    {
        GameController.Instance.Subscribe(this);
        Kick();
    }

    public void Kick()
    {
        var dir = new Vector2(DirectionValue(), DirectionValue());
        body.velocity = dir * force;
    }

    private float DirectionValue()
    {
        return Random.value > .5f ? 1f : -1f;
    }

    public void Notify(ISubject subject)
    {
        Disappear();
    }

    public void RemoveObserver()
    {
        GameController.Instance.Unsubscribe(this);
        Disappear();
    }

    private void Disappear()
    {
        body.velocity = Vector2.zero;
        Instantiate(puff, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
