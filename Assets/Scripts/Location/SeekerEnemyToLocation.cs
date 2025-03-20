using UnityEngine;

public class SeekerEnemyToLocation : MonoBehaviour
{
    [SerializeField] private GameObject _portal;
    [SerializeField] private float _timer = 1f;
    public static SeekerEnemyToLocation Instanse;

    private float time;
    private void Start()
    {
        if(Instanse == null)
        {
            Instanse = this;
        }    
        else
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        time -= Time.deltaTime;
        if(time < 0f)
        {
            SeekerEnemy();
            time = _timer;
        }
    }
    public void SeekerEnemy()
    {
        EnemyHP[] enemys = Resources.FindObjectsOfTypeAll(typeof(EnemyHP)) as EnemyHP[];

        if(enemys.Length <= 0)
        {
            _portal.SetActive(true);
        }
    }
}
