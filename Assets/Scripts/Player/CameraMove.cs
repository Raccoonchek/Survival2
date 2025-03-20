using PhysicsCharacterController;
using UnityEngine;
using YG;

public class CameraMove : MonoBehaviour
{
    private float X, Y, Z;
    public int speeds;
    private float eulerX = 0, eulerY = 0;

    [SerializeField] private Transform _cameraRotation;
    [SerializeField] private Transform _playerRotation;
   private Transform _cameraTransform;
    // Use this for initialization

    public PlayerConroler _player;
    private MovementActions _movementActions;
    void Start()
    {
        _cameraTransform = this.transform;
        _movementActions = _player.input;
        if (YG2.envir.isDesktop)
        {
            speeds /= 15;
        }
    }

    // Update is called once per frame
    void Update()
    {

            MoveMobile();
        _playerRotation.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        _cameraTransform.position = _cameraRotation.position;
    }
    private Vector2 _axis;
    private void MoveMobile()
    {
        _axis = _movementActions.Gameplay.Camera.ReadValue<Vector2>();

        X = _axis.x * speeds * Time.deltaTime;
        Y = -_axis.y * speeds * Time.deltaTime;
        eulerX = (_cameraTransform.rotation.eulerAngles.x + Y) % 360;
        eulerY = (_cameraTransform.rotation.eulerAngles.y + X) % 360;

        _cameraTransform.rotation = Quaternion.Euler(eulerX, eulerY, 0);
    }  
}

