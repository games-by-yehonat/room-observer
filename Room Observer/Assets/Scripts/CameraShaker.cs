using UnityEngine;

public class CameraShaker : MonoBehaviour, IObserver
{
    [SerializeField] private float magnitude;
    [SerializeField] private float duration;
    
    private float _duration;
 
    private bool _shake;

    private void BeginShake()
    {
        _duration = duration;
        
        _shake = true;
    }

    private void StopShake()
    {
        transform.localPosition =  Vector3.zero;
        _shake = false;
    }

    private void Update()
    {
        if (!_shake)
        {
            return;
        }
        
        if (_duration > 0)
        {
            
            SetShakePosition();
            DiscountDuration();
        }
        else
        {
            StopShake();
        }

    }

    private void SetShakePosition()
    {
        transform.localPosition = Random.insideUnitSphere * magnitude;
    }
        
    private void DiscountDuration()
    {
        _duration -= Time.deltaTime;
    }

    public void Notify(ISubject subject)
    {
        BeginShake();
    }
}
