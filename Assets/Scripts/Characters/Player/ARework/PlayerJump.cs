using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerJump : MonoBehaviour
{
    //Input
    PlayerInput playerInput;
    InputAction jumpAction;
    CharacterController controller;

    //ActionVars
    [SerializeField] Vector3 playerVelocity;
    float gravity = -4.5f;
    [SerializeField] float jumpForce;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        jumpAction = playerInput.actions["Jump"];
        
    }

    private void Update()
    {
        playerVelocity.y += gravity * Time.deltaTime;

        if (PlayerSingleton.Instance.isGrounded && playerVelocity.y < 0)
            playerVelocity.y = 0f;

        if(playerVelocity.y > 0)
            playerVelocity += Vector3.up * gravity * Time.deltaTime;

        controller.Move(playerVelocity * Time.deltaTime);

        if (jumpAction.triggered && PlayerSingleton.Instance.isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpForce * -3f * gravity);
        }
    }
}
