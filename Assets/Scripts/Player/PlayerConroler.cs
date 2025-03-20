using PhysicsCharacterController;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using YG;

public class PlayerConroler : MonoBehaviour
{
    [Tooltip("Inventory")]
    [SerializeField] private GameObject _inventory;
    private bool _isInventoryOpen = false;
    [Tooltip("Input reference")]
    public MovementActions input;

    [Range(0.01f, 0.99f)]
    [Tooltip("Minimum input value to trigger movement")]
    public float movementThrashold = 0.01f;
    [Space(10)]

    public float dampSpeedDown = 0.1f;
    public float dampSpeedUp = 0.1f;
    [HideInInspector]
    public float targetAngle;
    public float sprintSpeed = 14f;
    public float movementSpeed = 14f;
    public float jumpVelocity = 20f;
    public float _gravity = 1f;
    private Rigidbody rigidbody;

            [Tooltip("Character camera")]
    public GameObject characterCamera;

    private Vector2 axisInput;
    private bool jump;
    private bool sprint;

    private bool isGrounded;
    private bool isJumping;
    private Vector3 currVelocity = Vector3.zero;
    private Vector3 forward;
    private Transform _player;
    
    public float slopeCheckerThrashold = 0.51f;
    public LayerMask groundMask;
    private float originalColliderHeight;
    private void Awake()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        _player = this.transform;

        input = new MovementActions();
    }
    private void OnEnable()
    {
        input.Enable();
        input.Gameplay.Jump.performed += MoveJump;
        input.Gameplay.Inventory.performed += OpenInventory;
    }
    private void OnDisable()
    {
        input.Disable();
        input.Gameplay.Jump.performed -= MoveJump;
        input.Gameplay.Inventory.performed -= OpenInventory;
    }
    private void Update()
    {
    }
    private void FixedUpdate()
    {
        forward = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        CheckGround();
        PlayerMove();
        Gravity();
    }    
    private void PlayerMove()
    {
        float crouchMultiplier = 1f;
        Vector2 side = input.Gameplay.Movement.ReadValue<Vector2>();
        axisInput = side;
        if (axisInput.magnitude > movementThrashold)
        {
            targetAngle = Mathf.Atan2(axisInput.x, axisInput.y) * Mathf.Rad2Deg + characterCamera.transform.eulerAngles.y;

            if (!sprint) rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, forward * movementSpeed * crouchMultiplier, ref currVelocity, dampSpeedUp);
            else rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, forward * sprintSpeed * crouchMultiplier, ref currVelocity, dampSpeedUp);
        }
        else rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, Vector3.zero * crouchMultiplier, ref currVelocity, dampSpeedDown);
        PlayerAnimationManager.Instance.AnimationMove(axisInput.magnitude);
    }
    private void CheckGround()
    {
        RaycastHit slopeHit;
        if (Physics.SphereCast(_player.position, slopeCheckerThrashold, Vector3.down, out slopeHit, originalColliderHeight / 2f + 0.5f, groundMask))
        {
            isGrounded = true;
            isJumping = false;
        }
        if (Physics.Raycast(_player.position,Vector3.down,out RaycastHit ray,groundMask))
        {
            if(Vector3.Distance(_player.position,ray.point) > 1)
            {
                isGrounded = false; 
            }
        }
    }
    private void MoveJump(InputAction.CallbackContext obj)
    {
        jump = true;
        //jumped
        if (jump && isGrounded)
        {
            rigidbody.velocity += Vector3.up * jumpVelocity;
            isJumping = true;
            isGrounded = false;
        }
    }
    private void OpenInventory(InputAction.CallbackContext obj)
    {
        if(_isInventoryOpen)
        {
            _inventory.SetActive(false);
            if (YG2.envir.isDesktop)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            _isInventoryOpen = false;
        }
        else
        {
            _inventory.SetActive(true);
            if (YG2.envir.isDesktop)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            _isInventoryOpen = true;
        }
    }
    [SerializeField] private int _speedMoveInventoryPanel = 100;
   
    private void Gravity()
    {
        if(isGrounded == false)
        {
            rigidbody.AddForce(Vector3.down * _gravity);
        }
    }

  }
