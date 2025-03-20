using Pathfinding;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private AIPath _aiPath;

    private void Start()
    {
        if (_target == null)
        {
            _target = GameObject.Find("Player").GetComponent<Transform>();
        }
        _aiPath = GetComponent<AIPath>();
    }
    void Update()
    {
        if(_target != null)
        {
            _aiPath.destination = _target.position;
        }
    }
}
