using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private int _Hp = 100;
    [SerializeField] private int _XP = 50;
    [SerializeField] private GameObject _dropItem;
    [SerializeField] private GameObject _damagePanel;


    public void DamagedEnemy(int damage)
    {
        _Hp -= damage;
        GameObject pan = Instantiate(_damagePanel, transform.position, Quaternion.identity) as GameObject;
        pan.GetComponent<DamagePanel>().DamageInfo(damage);
        if (_Hp <= 0)
        {
            LevelManager.Instance.AddXP(Mathf.RoundToInt(StatsManager.Instance.ReturnUpStats(_XP,TypeStats.INT)));
            DropItem(3);
            SeekerEnemyToLocation.Instanse.SeekerEnemy();
            Destroy(gameObject);
        }
    }

    private void DropItem(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(_dropItem, transform.position, Quaternion.identity);
        }
    }
}
