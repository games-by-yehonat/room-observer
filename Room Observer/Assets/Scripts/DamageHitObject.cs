using UnityEngine;
using UnityEngine.Events;

public class DamageHitObject : MonoBehaviour
{
    [SerializeField] private string hitTag;
    [SerializeField] private UnityEvent eventToToRaise;

    private void OnCollisionEnter2D(Collision2D hit)
    {
        if(!hit.gameObject.CompareTag(hitTag))
        {
            return;
        }

        var damageable = hit.gameObject.GetComponent<IDamageable>();
        damageable?.TakeDamage();

        eventToToRaise?.Invoke();
    }
}
