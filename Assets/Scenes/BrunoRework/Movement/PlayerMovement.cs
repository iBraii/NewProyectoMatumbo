
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _characterController;

    //Input vars
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    


    //Movement vars
    public float movementSpeed;
    private Vector3 direction;
    private Vector3 moveDirection; //Direction of camera
    public float turnSmoothTime = .1f;
    float turnSmoothvelocity;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
        _moveAction.ReadValue<float>();
    }
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Rotate();
    }

    public void Movement()
    {
        Vector2 input = _moveAction.ReadValue<Vector2>();
        direction = new Vector3(input.x, 0f, input.y).normalized;

        if(direction.magnitude >= .1f)
        {
            _characterController.Move(moveDirection.normalized * movementSpeed * Time.deltaTime);
        }
    }

    public void Rotate()
    {
        if (direction.magnitude >= .1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothvelocity,turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
    }

    
}
