using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] private Transform _objectOpen;
    [SerializeField] private float _maxPosY = 2f;

    private Vector3 _startPos;
    private bool _isOpen;

    private void Update()
    {
        OpenDoor();
    }
    private void OnTriggerStay(Collider other)
    {
        _isOpen = true;
    }
    private void OnTriggerExit(Collider other)
    {
        _isOpen = false;
    }

    private void OpenDoor()
    {
        if (_isOpen && _objectOpen.position.y < _maxPosY)
        {
            _objectOpen.position += new Vector3(0,0.1f,0);
        }
        else if (!_isOpen && _objectOpen.position.y > _startPos.y)
        {
            _objectOpen.position -= new Vector3(0, 0.1f, 0);
        }
    }
}
