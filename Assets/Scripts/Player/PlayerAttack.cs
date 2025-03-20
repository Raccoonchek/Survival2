using System.Collections;
using UnityEngine;
using YG;
using static UnityEngine.UI.Image;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int _damagePlayer;
    [SerializeField] private float _range;
    [SerializeField] private float _attackSpeed = 1.7f;
    [SerializeField] private LayerMask enemyMask;
    
    private int _damagePlayerOld;
    private float _attackSpeedOld = 1.7f;

    private Camera fpsCam;

    private bool _isAttack;
    private float _timerAttack = 1f;

    private void Awake()
    {
        _attackSpeedOld = _attackSpeed;
        _damagePlayerOld = _damagePlayer;
        fpsCam = Camera.main;
        StatsManager.OnUpStatsEvent.AddListener(UpDamage);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            AttackPlayer();

        }
        AttackTimer();
    }
    private void AttackTimer()
    {
        _timerAttack -= Time.deltaTime;
    }
    private void UpDamage()
    {
        _damagePlayer = Mathf.RoundToInt(StatsManager.Instance.ReturnUpStats(_damagePlayerOld, TypeStats.STR));
        _attackSpeed = _attackSpeedOld - (StatsManager.Instance.ReturnUpStats(_attackSpeedOld, TypeStats.AGL) / 25);
        _timerAttack = _attackSpeed;
    }
    private void AttackPlayer()
    {

        RaycastHit hitInfo;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward,out hitInfo, _range, enemyMask))
        {
            if(hitInfo.collider.GetComponent<EnemyHP>() != null && _timerAttack <= 0)
            {
                hitInfo.collider.GetComponent<EnemyHP>().DamagedEnemy(_damagePlayer);
                _timerAttack = _attackSpeed;
                PlayerAnimationManager.Instance.AnimationAttack();
            }
        }

    }
}
