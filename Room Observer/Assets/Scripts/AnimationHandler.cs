using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private MovementHandler movement;
    
    private bool _isDamage;

    private void Update()
    {
        if (GameController.Instance.GetGameState() == GameState.Death)
        {
            return;
        }
        
        animator.SetBool($"Damage", _isDamage);
        animator.SetFloat($"Speed", movement.Move > Mathf.Epsilon ? 1f : 0f);
    }

    public void SetDamageState(bool state)
    {
        _isDamage = state;
    }

    public void SetDeathState()
    {
        animator.SetBool($"Death", true);
    }
}
