using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _characterController;

    //Input vars
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction jumpAction;

    private Vector2 currentInputVector;
    public Vector2 currentInputVectorRaw;
    private Vector2 smoothInputVel;
    [SerializeField] float smoothInputSpd;


    //Movement vars
    public float movementSpeed;
    [SerializeField] Vector3 direction;
    /*[HideInInspector]*/ public Vector3 moveDirection; //Direction of camera
    float turnSmoothTime = .1f;
    float turnSmoothvelocity;

    //jump vars
    float gravity = -4.5f;
    [SerializeField] float jumpForce;
     Vector3 playerVelocity;


    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
        jumpAction = _playerInput.actions["Jump"];
    }
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        PlayerSingleton.Instance.canMove = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(PlayerSingleton.Instance.canMove)
        {
            Rotate();
            Movement();
        }
        Gravity();
    }
    private void Update()
    {
        if (PlayerSingleton.Instance.canMove)
            Jumping();
    }
    public void Movement()
    {
        Vector2 input = _moveAction.ReadValue<Vector2>();
        currentInputVectorRaw = _moveAction.ReadValue<Vector2>();
        currentInputVector = Vector2.SmoothDamp(currentInputVector, input, ref smoothInputVel, smoothInputSpd);
       

        direction = new Vector3(currentInputVector.x, 0f, currentInputVector.y).normalized;           
        if (direction.magnitude >= .1f && !PlayerSingleton.Instance.isHiding)
        {
            PlayerSingleton.Instance.isMoving = true;
            _characterController.Move(moveDirection.normalized * movementSpeed * Time.deltaTime);
        }
        else
            PlayerSingleton.Instance.isMoving = false;
    }

    public void Rotate()
    {
        if (direction.magnitude >= .1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothvelocity, turnSmoothTime);
            if (PlayerSingleton.Instance.canRotate&&!PlayerSingleton.Instance.isHiding)
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
        else
            moveDirection = Vector3.zero;
    }
    private void Gravity()
    {
        playerVelocity.y += gravity * Time.deltaTime;
        _characterController.Move(playerVelocity * Time.deltaTime);

        if (PlayerSingleton.Instance.isGrounded && playerVelocity.y < 0)
            playerVelocity.y = 0f;

        if (playerVelocity.y > 0)
            playerVelocity += Vector3.up * gravity * Time.deltaTime;
    }
    private void Jumping()
    {
        if (jumpAction.triggered && PlayerSingleton.Instance.isGrounded&&!PlayerSingleton.Instance.isHiding&&PlayerSingleton.Instance.canJump)
        {
            playerVelocity.y += Mathf.Sqrt(jumpForce * -3f * gravity);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
