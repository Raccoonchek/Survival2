using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private Transform _player;
    private Rigidbody rb;
    [SerializeField] private float _speedMove = 5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _player = GameObject.FindObjectOfType<PlayerHP>().transform;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerHP>() != null)
        {
            Debug.Log("Item pick Up");
            CoinCounter.Instance.AddCoin(2);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Item Drop");
        }
    }

    private void Update()
    {
        MoveToPlayer();
    }

    private Vector2 _movement;
    private void MoveToPlayer()
    {
        if (Vector3.Distance(transform.position, _player.position) < 5)
        {
            Vector3 direction = _player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            direction.Normalize();

            rb.MovePosition(transform.position + (direction * _speedMove * Time.deltaTime));
        }
    }

}