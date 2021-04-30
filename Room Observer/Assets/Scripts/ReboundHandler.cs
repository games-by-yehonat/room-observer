using UnityEngine;

public class ReboundHandler : MonoBehaviour, IObserver
{
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private GameObject puff;

    private WaveHandler _handler;
    private float _force;

    public void Initialization(WaveHandler handler, float force)
    {
        _handler = handler;
        _force = force;
    }
    
    private void Start()
    {
        EnablePuff();
        ApplyForce();
    }

    private void EnablePuff()
    {
        Instantiate(puff, transform.position, Quaternion.identity);
    }

    public void ApplyForce()
    {
        var dir = new Vector2(DirectionValue(), DirectionValue());
        body.velocity = dir * _force;
    }

    private float DirectionValue()
    {
        return Random.value > .5f ? 1f : -1f;
    }

    public void Notify(ISubject subject)
    {
        DestroyObject();
    }

    public void UnsubscribeObserverAndDestroy()
    {
        _handler.Unsubscribe(this);
        DestroyObject();
    }

    private void DestroyObject()
    {
        body.velocity = Vector2.zero;
        EnablePuff();
        
        Destroy(gameObject);
    }
}
