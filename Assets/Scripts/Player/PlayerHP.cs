using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private float _HitPoint = 100;
    [SerializeField] private int _MaxHitPoint = 100;

    private int OldHitPoint;


    private void Awake()
    {
        OldHitPoint = _MaxHitPoint;
        StatsManager.OnUpStatsEvent.AddListener(UpHitPoint);
    }
    private void Start()
    {
        HitPointManager.Instance.StartHitPointSlider(_HitPoint, _MaxHitPoint);
    }
    private void UpHitPoint()
    {
        _MaxHitPoint = Mathf.RoundToInt(StatsManager.Instance.ReturnUpStats(OldHitPoint, TypeStats.STR));
        _HitPoint = _MaxHitPoint;
    }
    public void DamagedPlayer(int damage)
    {
        _HitPoint -= damage;
        HitPointManager.Instance.DamagePlayerUI(_HitPoint, damage);
        if (_HitPoint <= 0)
        {
            Debug.Log("Death");
        }
    }
    public void Hilling(float hilling)
    {

        if (_HitPoint < _MaxHitPoint)
        {
            _HitPoint += hilling;
            HitPointManager.Instance.UpdateSliderOfHilling(_HitPoint);
        }
    }


}
