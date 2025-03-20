using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speedAttack = 0.3f;
    [SerializeField] private int _damage = 5;

    private float _timeToAttack;
    private Animator _animator;

    private void Awake()
    {
        if(_target == null)
        {
            _target = GameObject.Find("Player").GetComponent<Transform>();
        }
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        CheckOnAttack();
    }

    private void CheckOnAttack()
    {

        if (Vector3.Distance(transform.position, _target.position) <= 2 && _timeToAttack <= 0)
        {
            _animator.SetTrigger("Attack");
            _target.GetComponent<PlayerHP>().DamagedPlayer(_damage);
            _timeToAttack = _speedAttack;
        }
        else
        {
            _timeToAttack -= Time.deltaTime;
        }
    }
}
