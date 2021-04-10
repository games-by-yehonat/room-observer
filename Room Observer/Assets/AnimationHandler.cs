using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private MovementHandler movement;
    
    private bool _isDamage;
    private bool _isDeath;

    private void LateUpdate()
    {
        if (GameController.Instance.IsDeath)
        {
            return;
        }
        
        animator.SetBool($"Damage", _isDamage);
        animator.SetFloat($"Speed", movement.Move > Mathf.Epsilon ? 1 : 0);
    }

    public void SetDamageState(bool state)
    {
        movement.SetCoolDown();
        _isDamage = state;
    }

    public void SetDeathState()
    {
        _isDeath = true;
        animator.SetBool($"Death", _isDeath);
    }
}
