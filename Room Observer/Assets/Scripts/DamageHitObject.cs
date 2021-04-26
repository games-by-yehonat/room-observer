using UnityEngine;

public class DamageHitObject : MonoBehaviour
{
    [SerializeField] private ReboundHandler collision;
    [SerializeField] private string hitTag;

    private void OnCollisionEnter2D(Collision2D hit)
    {
        if(!hit.gameObject.CompareTag(hitTag) || GameController.Instance.CoolDown)
        {
            return;
        }

        var damageable = hit.gameObject.GetComponent<IDamageable>();
        damageable?.TakeDamage();

        collision.RemoveObserver();
    }
}
