using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage();
}

public class DamageHitObject : MonoBehaviour
{
    [SerializeField] private string hitTag;
    public event Action HitByObject;

    private void OnCollisionEnter2D(Collision2D hit)
    {
        if(!hit.gameObject.CompareTag(hitTag))
        {
            return;
        }

        var damageable = hit.gameObject.GetComponent<IDamageable>();
        damageable?.TakeDamage();
        
        HitByObject?.Invoke();
    }
}
