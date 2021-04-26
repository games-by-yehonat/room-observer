using UnityEngine;

public class ReboundHandler : MonoBehaviour, IObserver
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private GameObject puff;
    
    [Space(10)]
    [SerializeField] [Range(0f, 20f)] private float force = 5f;
    public float Force { set => force = value; }
    
    private void Start()
    {
        Instantiate(puff, transform.position, Quaternion.identity);
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
        Instantiate(puff, transform.position, Quaternion.identity); // TODO: Object pooling
        
        Destroy(gameObject);
    }
}
