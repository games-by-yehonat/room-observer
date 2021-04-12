﻿using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private MovementHandler movement;
    
    private bool _isDamage;

    private void Update()
    {
        if (GameController.Instance.IsDeath)
        {
            return;
        }
        
        animator.SetBool($"Damage", _isDamage);
        animator.SetFloat($"Speed", movement.Move > Mathf.Epsilon ? 1f : 0f);
    }

    public void SetDamageState(bool state)
    {
        GameController.Instance.SetCoolDown();
        _isDamage = state;
    }

    public void SetDeathState()
    {
        if (GameController.Instance.IsDeath)
        {
            animator.SetBool($"Death", true);
        }
    }
}