using TMPro;
using UnityEngine;

public class DamagePanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
   public void DamageInfo(int damage)
    {
        _text.text = damage.ToString();
    }

    private void Update()
    {
        Destroy(gameObject,3);
        Move();
    }
    private void Move()
    {
        transform.position += Vector3.up * Time.deltaTime;
    }
}
