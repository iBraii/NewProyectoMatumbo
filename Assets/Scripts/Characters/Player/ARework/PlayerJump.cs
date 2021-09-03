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
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] float floorDistance;
    [SerializeField] Vector3 playerVelocity;
    float gravity = -9.81f;
    [SerializeField] float jumpForce;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        jumpAction = playerInput.actions["Jump"];
    }

    bool Grounded() => Physics.Raycast(transform.position, Vector3.down, floorDistance, GroundLayer);

    private void Update()
    {
        playerVelocity.y += gravity * Time.deltaTime;

        if (Grounded() && playerVelocity.y < 0)
            playerVelocity.y = 0f;

        if(playerVelocity.y > 0)
            playerVelocity += Vector3.up * gravity * Time.deltaTime;

        controller.Move(playerVelocity * Time.deltaTime);

        if (jumpAction.triggered && Grounded())
        {
            playerVelocity.y += Mathf.Sqrt(jumpForce * -3f * gravity);
        }
    }
}
