using UnityEngine;

public class TriggerOpenStore : MonoBehaviour
{
    [SerializeField] private WeaponManager _weaponManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerHP>())
        {
            _weaponManager.OpenStore(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerHP>())
        {
            _weaponManager.OpenStore(false);
        }
    }
}
