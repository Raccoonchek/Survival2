using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    public static PlayerAnimationManager Instance;

   [SerializeField] private Animator _animator;
   [SerializeField] private PlayerAttack _palyerAttack;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        if(_animator == null)
        {
            _animator = GetComponent<Animator>();
        }
    }


    public void AnimationMove(float count)
    {
        if (count > 0)
        {
            _animator.SetBool("Move", true);
        }
        else
        {
            _animator.SetBool("Move", false);
        }
    }
    public void AnimationAttack()
    {
        _animator.SetTrigger("Attack");
    }
}
